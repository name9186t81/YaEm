using UnityEngine;

namespace YaEm.Effects
{
    [RequireComponent(typeof(Projectile))]
    public sealed class ProjectileTrailCleaner : MonoBehaviour
    {
        [SerializeField] private TrailRenderer _renderer;

		private void Awake()
		{
			GetComponent<Projectile>().OnDeactivate += () => _renderer.Clear();
		}
	}
}