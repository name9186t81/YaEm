using UnityEngine;

namespace YaEm
{
	[RequireComponent(typeof(ParticleSystem))]
    public class ProjectileBounceGlower : MonoBehaviour
    {
        [SerializeField] private Projectile _projectile;

		private void Awake()
		{
			_projectile.OnHit += Hit;
		}

		private void Hit(RaycastHit2D obj)
		{
			_projectile.DamageArgs.Damage *= 2;
			GetComponent<ParticleSystem>().maxParticles++;
		}
	}
}