using System;
using UnityEngine;

namespace YaEm.AI
{
	//todo: ai calculations are pretty heave so implemented global ai updater and set its tick rate to like 0.1 sec
	[RequireComponent(typeof(AIVision)), DisallowMultipleComponent]
	public class AIController : MonoBehaviour, IController
	{
		//todo: write custom editor for all of this
		[SerializeField] private Actor _controlled;
		[SerializeField] private float _fireThreshold;
		[SerializeField] private AIProfile _min;
		[SerializeField] private AIProfile _max;
		private AIProfile _mixed;
		private AIVision _vision;
		private IHealth _health;
		private Motor _motor;
		private Vector2 _direction;
		private float _rotation;

		private void Awake()
		{
			if (!_controlled.TryChangeController(this))
			{
				Debug.LogWarning("Cant change controller on " + _controlled);
				enabled = false;
				return;
			}

			_vision = GetComponent<AIVision>();
			_vision.Init(this);

			if (_controlled is IProvider<IHealth> provider)
			{
				_health = provider.Value;
			}
			if (_controlled is IProvider<Motor> motor)
			{
				_motor = motor.Value;
			}

			_mixed = AIProfile.Mix(_min, _max);
		}

		private void Update()
		{
			if (Vision.EnemiesInRangeCount > 0)
			{
				Actor enemy = Vision.EnemiesInRange[0];

				LookAtPoint(enemy.Position);
				if (IsEffectiveToFire(enemy.Position))
					InvokeAction(ControllerAction.Fire);
				if (!enemy.Position.FastDistanceCheck(Position, 25))
					MoveToThePoint(enemy.Position);
				else
					StopMoving();
			}
		}

		public bool IsEffectiveToFire(Vector2 point)
		{
			return Mathf.Abs(Vector2.Angle(Actor.Transform.up, point - Position)) < _fireThreshold;
		}

		public void LookAtPoint(Vector2 point)
		{
			Vector2 dir = Position - point;
			_rotation = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + 90;
		}

		public void MoveToThePoint(Vector2 point)
		{
			MoveDirection = point - Position;
		}

		public void InvokeAction(ControllerAction action)
		{
			OnAction?.Invoke(action);
		}

		public void StopMoving()
		{
			MoveDirection = Vector2.zero;
		}

		public Vector2 MoveDirection {get { return _direction; } set { _direction = value.normalized; } }
		public Vector2 DesiredMoveDirection => _direction;
		public Vector2 Position => _controlled.Position;
		public Actor Actor => _controlled;

		public float DesiredRotation => _rotation;

		public event Action<ControllerAction> OnAction;

		public float Aggresivness => _mixed.Aggresivness;
		public float TeamWork => _mixed.TeamWork;
		public AIVision Vision => _vision;
		public IHealth Health => _health;
		public Motor Motor => _motor;
	}
}