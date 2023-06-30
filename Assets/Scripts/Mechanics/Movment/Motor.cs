using System;
using System.Collections.Generic;
using UnityEngine;

namespace YaEm
{
	public sealed class Motor
	{
		private readonly List<IForce> _forces = new List<IForce>(1);
		private readonly float _speed;
		private readonly float _rotationSpeed;
		private readonly Actor _actor;
		private readonly Rigidbody2D _rigidbody;
		private BaseForce _movmentForce;
		private IController _controller;
		public event Action<float> _updateFunc;

		public Motor(float speed, float rotationSpeed, Actor controller)
		{
			_speed = speed;
			_rotationSpeed = rotationSpeed;
			_actor = controller;
			_controller = _actor.CurrentController;
			_actor.OnControllerChange += (IController oldC, IController newC) =>
			{
				_controller = newC;
				_movmentForce = new BaseForce((_) => _controller.DesiredMoveDirection * _speed);
			};
			_rigidbody = _actor.GetComponent<Rigidbody2D>();

			if (_controller == null)
			{
				Debug.LogWarning($"Actor: {controller.gameObject.name} does not have controller");
				return;
			}
			//movment force
			_movmentForce = new BaseForce((_) => _controller.DesiredMoveDirection * _speed);
		}

		public void Update(float deltaTime)
		{
			Vector2 velocity = SummarizeForces();
			if (_rigidbody != null)
			{
				_rigidbody.velocity = velocity;
			}
			else
			{
				_actor.Transform.position += (Vector3)velocity * deltaTime;
			}
			_updateFunc?.Invoke(deltaTime);
			if (_controller == null) return;

			_actor.Transform.rotation = Quaternion.Lerp(_actor.Transform.rotation, Quaternion.Euler(0, 0, _controller.DesiredRotation), _rotationSpeed * deltaTime);
			if (_rigidbody != null)
			{
				_rigidbody.velocity = velocity + _movmentForce.ForceFunc(_actor.Position);
			}
			else
			{
				_actor.Transform.position += (Vector3)_movmentForce.ForceFunc(_actor.Position) * deltaTime;
			}
		}

		public void AddForce(IForce force)
		{
			_forces.Add(force);
		}

		public bool RemoveForce(IForce force)
		{
			return _forces.Remove(force);
		}

		private Vector2 SummarizeForces()
		{
			Vector2 result = new Vector2();
			for (int i = 0, length = _forces.Count; i < length; i++)
			{
				result += _forces[i].ForceFunc(_actor.Position);
			}
			return result;
		}

		public float Speed => _speed;
		public Vector2 MoveDirection => _controller.DesiredMoveDirection;
	}
}