using System;
using System.Reflection;
using UnityEngine;

namespace YaEm
{
	public enum MovmentStrategy
	{
		Linear,
		Sin
	}

	public enum StandartHitReact
	{
		Bounce,
		Destroy
	}
	public class Projectile : Actor
	{
		[SerializeField] private float _speed;
		[SerializeField] private MovmentStrategy _movmentType;
		[SerializeField] private LayerMask _hitMask;
		[SerializeField] private StandartHitReact _react;
		[SerializeField] private int _maxBounceTimes;
		private int _bounceTimes;
		private Vector2 _prevPosition;
		private Vector2 _direction;
		private IMovmentStrategy _strategy;
		private bool _active = true;

		public DamageArgs DamageArgs;
		public event Action<RaycastHit2D> OnHit;

		private void Update()
		{
			_prevPosition = Position;
		}

		protected override void Init()
		{
			_strategy = MovmentStrategyFactory.GetStrategy(_speed, _movmentType);
			_prevPosition = Position;
		}

		private void LateUpdate()
		{
			if (!_active) return;

			Position += _strategy.Move(_direction) * Time.deltaTime;
			Vector2 currentPosition = Position;
			float distance = (currentPosition - _prevPosition).magnitude;
			Vector2 direction = (currentPosition - _prevPosition) / distance;

			var raycast = Physics2D.Raycast(_prevPosition, direction, distance, _hitMask);
			if (raycast)
			{
				Position = raycast.point;
				if (raycast.transform.TryGetComponent<IProjectileReactable>(out IProjectileReactable reactable))
				{
					reactable.OnHit(this, raycast.normal);
					OnHit?.Invoke(raycast);
				}
				else
				{
					//todo make standart projectile collision interaction
					//and leave special interactions for IProjectileReactable
					if (_react == StandartHitReact.Bounce && _bounceTimes++ < _maxBounceTimes)
					{
						Reflect(raycast.normal);
						OnHit?.Invoke(raycast);
						return;
					}
					RemoveProjectile();
				}
			}
		}

		public void Reflect(Vector2 normal)
		{
			_direction = Vector2.Reflect(_direction, normal);
			Position += _direction * _speed * Time.deltaTime;
			_prevPosition = Position;
			transform.rotation = Quaternion.AngleAxis(_direction.AngleFromVector(), Vector3.forward);
		}

		public void RemoveProjectile()
		{
			//todo make a projectile pool
			Destroy(gameObject, 2);
			enabled = _active = false;
		}

		public void ChangeDirection(Vector2 direction)
		{
			_direction = direction.normalized;
		}

		public int BounceTimes => _bounceTimes;
		public int MaxBounceTimes => _maxBounceTimes;
		public Vector2 Direction{ get { return _direction; } }
	}
}