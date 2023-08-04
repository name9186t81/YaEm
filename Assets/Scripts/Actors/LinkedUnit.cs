using UnityEngine;

namespace YaEm
{
	//this script exists solidly because for some reason unity will summarize all colliders
	//on gameobject into one when calculating rigidbody stuff which i dont want to happen
	[DisallowMultipleComponent]
	public class LinkedUnit : MonoBehaviour
	{
		[SerializeField] private Unit _tracked;
		[SerializeField] private Transform _applied;
		private float _radius;
		private float _angle;
		private float _selfAngle;

		private void Start()
		{
			_radius = _applied.localPosition.magnitude;
			_selfAngle = _applied.localRotation.eulerAngles.z;
			_angle = _applied.localPosition.AngleFromVector() * Mathf.Deg2Rad;
			_applied.SetParent(null);
			_tracked.Value.OnDeath += (_) => Destroy(gameObject);

			if (TryGetComponent<Actor>(out var act))
			{
				_tracked.OnTeamNumberChange += (int old, int newT) => act.TryChangeTeamNumber(newT);
				act.TryChangeTeamNumber(_tracked.TeamNumber);
			}
		}

		private void Update()
		{
			_applied.position = _tracked.Position + (_angle + _tracked.Transform.eulerAngles.z * Mathf.Deg2Rad).VectorFromAngle() * _radius;
			_applied.rotation = Quaternion.Euler(0, 0, _tracked.Transform.eulerAngles.z + _selfAngle);
		}
	}
}