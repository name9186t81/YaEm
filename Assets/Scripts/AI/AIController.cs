using System;
using UnityEngine;

namespace YaEm.AI
{
	[RequireComponent(typeof(AIVision))]
	public class AIController : MonoBehaviour, IController
	{
		[SerializeField] private Actor _controlled;
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
		}

		private void Update()
		{
			Debug.Log(Vision.AliesInRange.Count);
			if (Vision.EnemiesInRange.Count > 0)
			{
				Actor enemy = Vision.EnemiesInRange[0];

				LookAtPoint(enemy.Position);
				InvokeAction(ControllerAction.Fire);
				if (Vector2.Distance(enemy.Position, Position) > 5f)
					MoveToThePoint(enemy.Position);
				else
					StopMoving();
			}
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

		public AIVision Vision => _vision;
		public IHealth Health => _health;
		public Motor Motor => _motor;
	}
}