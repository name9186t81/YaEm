using UnityEngine;

namespace YaEm
{
	//todo: make deflector able to deflect only different team's bullets
	public class BulletDeflector : MonoBehaviour, IProjectileReactable
	{
		public bool IsHiddenHit => false;

		public bool CanReact(Projectile projectile)
		{
			return true;
		}

		public void OnHit(Projectile projectile, Vector2 normal)
		{
			projectile.Reflect(normal);
			if (TryGetComponent<IProvider<IHealth>>(out var health))
			{
				health.Value.TakeDamage(projectile.DamageArgs);
			}
		}
	}
}