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
			_projectile.OnInit += () => GetComponent<ParticleSystem>().maxParticles = 1;
		}

		private void Hit(RaycastHit2D obj)
		{
			//todo separate damage operation
			_projectile.DamageArgs.Damage = (int)(_projectile.DamageArgs.Damage * 1.5f);
			GetComponent<ParticleSystem>().maxParticles++;
		}
	}
}