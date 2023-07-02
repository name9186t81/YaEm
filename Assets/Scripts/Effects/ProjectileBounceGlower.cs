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
			//todo separate damage operation
			_projectile.DamageArgs.Damage = (int)(_projectile.DamageArgs.Damage * 1.5f);
			GetComponent<ParticleSystem>().maxParticles++;
		}
	}
}