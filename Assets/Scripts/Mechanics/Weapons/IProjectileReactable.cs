using UnityEngine;

namespace YaEm {
	public interface IProjectileReactable
	{
		void OnHit(Projectile projectile, Vector2 normal);
		bool IsHiddenHit { get; }
		bool CanReact(Projectile projectile);
	}
}