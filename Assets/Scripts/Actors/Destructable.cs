using UnityEngine;

namespace YaEm {
	public class Destructable : Actor, IProjectileReactable, IProvider<IHealth>
	{
		[SerializeField] private int _maxHealth;
		private IHealth _health;

		public IHealth Value => _health;

		protected override void Init()
		{
			_health = new Health(_maxHealth);
		}

		public void OnHit(Projectile projectile, Vector2 normal)
		{
			_health.TakeDamage(projectile.DamageArgs);
		}
	}
}