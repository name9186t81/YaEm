using UnityEngine;

namespace YaEm {
	public class Destructable : Actor, IProjectileReactable, IProvider<IHealth>
	{
		[SerializeField] private int _maxHealth;
		private IHealth _health;

		public IHealth Value => _health;

		public bool IsHiddenHit => false;

		protected override void Init()
		{
			_health = new Health(_maxHealth);

			_health.OnDeath += (_) => Destroy(gameObject);
		}

		public void OnHit(Projectile projectile, Vector2 normal)
		{
			_health.TakeDamage(projectile.DamageArgs);
		}

		//todo: perhaps implement friendly fire lock?
		public bool CanReact(Projectile projectile)
		{
			return true;
		}
	}
}