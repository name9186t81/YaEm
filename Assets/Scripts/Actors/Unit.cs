using UnityEngine;

namespace YaEm {
	[RequireComponent(typeof(Collider2D)), RequireComponent(typeof(Rigidbody2D))]
	public class Unit : Actor, IProjectileReactable, IProvider<IHealth>, IProvider<Motor>
	{
		[SerializeField] private int _maxHealth;
		[SerializeField] private float _speed;
		[SerializeField] private float _rotationSpeed;
		private IHealth _health;
		private Motor _motor;

		public IHealth Value => _health;
		Motor IProvider<Motor>.Value => _motor;

		protected override void Init()
		{
			//todo implement health factory
			_health = new Health(_maxHealth);
			_motor = new Motor(_speed, _rotationSpeed, this);
		}

		private void OnValidate()
		{
			_maxHealth = Mathf.Max(_maxHealth, 0);
		}

		public void OnHit(Projectile projectile)
		{
			_health.TakeDamage(projectile.DamageArgs);
			projectile.RemoveProjectile();
		}
	}
}