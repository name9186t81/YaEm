using UnityEngine;

namespace YaEm
{
	public class ProjectileReflectZone : MonoBehaviour, IProjectileReactable
	{
		[SerializeField, Tooltip("if non null will change team number to owner team number")] 
		private Actor _owner;
		[SerializeField, Tooltip("Set to -2 to disable team change, has lower priority over owner")] 
		private int _forcedTeam = -2;
		[SerializeField] private bool _deflectAtSender;
		[SerializeField] private bool _ignoreSameTeam;

		public bool IsHiddenHit => true;

		public bool CanReact(Projectile projectile)
		{
			return !(_ignoreSameTeam && (_owner != null && _owner.TeamNumber == projectile.TeamNumber || projectile.TeamNumber == _forcedTeam));
		}

		public void OnHit(Projectile projectile, Vector2 normal)
		{
			if (_deflectAtSender && projectile.Owner != null)
			{
				projectile.ChangeDirection(projectile.Owner.Position - projectile.Position);
			}

			if (_forcedTeam != -2) projectile.TryChangeTeamNumber(_forcedTeam);
			if(_owner != null) projectile.TryChangeTeamNumber(_owner.TeamNumber);
		}
	}
}