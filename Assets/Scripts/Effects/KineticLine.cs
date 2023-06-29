using UnityEngine;

namespace YaEm.Effects
{
	[RequireComponent(typeof(LineRenderer))]
	public class KineticLine : MonoBehaviour
	{
		[SerializeField] private float _maxVelocity;
		[SerializeField] private float _velocityLostPerSecond;
		[SerializeField] private float _maxRange;
		[SerializeField] private bool _debug;
		private LineRenderer _renderer;
		private float _distance;
		private Vector2 _velocity;
		private Vector2 _direction;
		private Vector2 _prevPosition;

		private void Awake()
		{
			_renderer = GetComponent<LineRenderer>();
			_renderer.enabled = true;
			_renderer.positionCount = 2;
			_distance = _maxRange * _maxRange;
			_prevPosition = transform.position;
		}

		private void Update()
		{
			Vector2 secondPos = _renderer.GetPosition(1) + transform.position;
			_velocity += (secondPos - _prevPosition) * Time.deltaTime;
			_direction = _velocity.normalized;
			_velocity -= _direction * _velocityLostPerSecond * Time.deltaTime;

			if (_velocity.sqrMagnitude > _maxVelocity * _maxVelocity)
			{
				_velocity = _direction * _maxVelocity;
			}

			if ((secondPos - (Vector2)_renderer.GetPosition(1)).sqrMagnitude > _distance)
			{
				_velocity = Vector2.Reflect(_direction, (Vector2)_renderer.GetPosition(1) - secondPos) * _velocity.magnitude;
			}
			_prevPosition = secondPos;
			_renderer.SetPosition(1, secondPos - (Vector2)transform.position + _velocity);
		}

		private void OnValidate()
		{
			_maxRange = Mathf.Max(0, _maxRange);
			_velocityLostPerSecond = Mathf.Max(0, _velocityLostPerSecond);
			_maxVelocity = Mathf.Max(0, _maxVelocity);
		}

		private void OnDrawGizmos()
		{
			if(!_debug) return;

			Gizmos.color = Color.yellow;
			Gizmos.DrawWireSphere(GetComponent<LineRenderer>().GetPosition(0) + transform.position, _maxRange);
		}
	}
}