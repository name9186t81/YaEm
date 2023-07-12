using UnityEngine;

namespace YaEm
{
	public class BulletDeflector : MonoBehaviour, IProjectileReactable
	{
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