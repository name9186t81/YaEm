using UnityEngine;

namespace YaEm
{
	public enum MovmentStrategy
	{
		Linear,
		Sin
	}

	public class Projectile : Actor
	{
		[SerializeField] private float _speed;
		[SerializeField] private MovmentStrategy _movmentType;
		[SerializeField] private LayerMask _hitMask;
		private Vector2 _prevPosition;
		private Vector2 _direction;
		private IMovmentStrategy _strategy;

		public DamageArgs DamageArgs;

		private void Update()
		{
			_prevPosition = Position;
		}

		protected override void Init()
		{
			_strategy = MovmentStrategyFactory.GetStrategy(_speed, _movmentType);
		}

		private void LateUpdate()
		{
			Position += _strategy.Move(_direction) * Time.deltaTime;
			Vector2 currentPosition = Position;
			float distance = (_prevPosition - currentPosition).magnitude;
			Vector2 direction = (_prevPosition - currentPosition) / distance;

			var raycast = Physics2D.Raycast(currentPosition, direction, distance, _hitMask);
			if (raycast)
			{
				if (raycast.transform is IProjectileReactable reactable)
				{
					reactable.OnHit(this);
				}
				else
				{
					//todo make standart projectile collision interaction
					//and leave special interactions for IProjectileReactable
					RemoveProjectile();
				}
			}
		}

		public void RemoveProjectile()
		{
			//todo make a projectile pool
			Destroy(gameObject);
		}

		public void ChangeDirection(Vector2 direction)
		{
			_direction = direction.normalized;
		}

		public Vector2 Direction{ get { return _direction; } }
	}
}