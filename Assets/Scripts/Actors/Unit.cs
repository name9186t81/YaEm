using UnityEngine;

namespace YaEm {
	[RequireComponent(typeof(Collider2D)), RequireComponent(typeof(Rigidbody2D))]
	public class Unit : Actor, IProjectileReactable, IProvider<IHealth>, IProvider<Motor>
	{
		[SerializeField] private int _maxHealth;
		[SerializeField] private float _speed;
		[SerializeField] private float _rotationSpeed;
		[SerializeField] private bool _reactToHit = true;
		private IHealth _health;
		private Motor _motor;

		public IHealth Value => _health;
		Motor IProvider<Motor>.Value => _motor;

		public bool IsHiddenHit => false;

		protected override void Init()
		{
			//todo implement health factory
			_health = new Health(_maxHealth);
			_motor = new Motor(_speed, _rotationSpeed, this);
			_health.OnDeath += Die;
		}

		private void Die(DamageArgs obj)
		{
			_health.OnDeath -= Die;
			Destroy(gameObject); //todo use pool perhaps
		}

		private void Update()
		{
			_motor.Update(Time.deltaTime);
		}

		private void OnValidate()
		{
			_maxHealth = Mathf.Max(_maxHealth, 0);
		}

		public void OnHit(Projectile projectile, Vector2 normal)
		{
			if (!_reactToHit) return;

			_health.TakeDamage(projectile.DamageArgs);
			projectile.RemoveProjectile();
		}

		//todo: perhaps implement friendly fire lock?
		public bool CanReact(Projectile projectile)
		{
			return true;
		}
	}
}