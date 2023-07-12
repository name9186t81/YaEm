using UnityEngine;

namespace YaEm {
	public interface IProjectileReactable
	{
		void OnHit(Projectile projectile, Vector2 normal);
	}
}