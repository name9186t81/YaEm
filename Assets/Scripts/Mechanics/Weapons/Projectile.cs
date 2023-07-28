using System;
using System.Collections;
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

	//please god do not inherit from projectile
	public sealed class Projectile : Actor
	{
		[SerializeField] private float _speed;
		[SerializeField] private MovmentStrategy _movmentType;
		[SerializeField] private LayerMask _hitMask;
		[SerializeField] private StandartHitReact _react;
		[SerializeField] private int _maxBounceTimes;
		[SerializeField] private float _deactivationTime;

		private Actor _owner;
		private WaitForSeconds _delay;
		private int _bounceTimes;
		private Vector2 _prevPosition;
		private Vector2 _direction;
		private IMovmentStrategy _strategy;
		private bool _active = true;
		private Pool<Projectile> _storedPool;
		private float _speedMod = 1;

		public event Action OnInit;
		public DamageArgs DamageArgs;
		public event Action<RaycastHit2D> OnHit;
		public event Action OnDeactivate;

		private void Update()
		{
			_prevPosition = Position;
		}

		protected override void Init()
		{
			_strategy = MovmentStrategyFactory.GetStrategy(_speed, _movmentType);
			_prevPosition = Position;
			_delay = new WaitForSeconds(_deactivationTime);
		}

		public void Init(Pool<Projectile> pool, DamageArgs args, int team, Vector2 position, Vector2 direction, Actor owner = null, float speedModifier = 1)
		{
			_prevPosition = Position = position;
			_active = enabled = true;
			gameObject.SetActive(true);
			_direction = direction;
			DamageArgs = args;

			_owner = owner;
			_bounceTimes = 0;
			_storedPool = pool;
			_speedMod = speedModifier;
			TryChangeTeamNumber(team);
			OnInit?.Invoke();
		}

		private void LateUpdate()
		{
			if (!_active) return;

			Position += _strategy.Move(_direction) * Time.deltaTime * _speedMod;

			Vector2 currentPosition = Position;
			float distance = (currentPosition - _prevPosition).magnitude;
			Vector2 direction = (currentPosition - _prevPosition) / distance;

			var raycast = Physics2D.Raycast(_prevPosition, direction, distance, _hitMask);
			if (raycast)
			{
				if (raycast.transform.TryGetComponent(out IProjectileReactable reactable))
				{
					if (!reactable.CanReact(this)) return;

					Position = raycast.point;
					reactable.OnHit(this, raycast.normal);
					if(!reactable.IsHiddenHit) OnHit?.Invoke(raycast);
				}
				else
				{
					//todo make standart projectile collision interaction
					//and leave special interactions for IProjectileReactable
					Position = raycast.point;
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
			if (_storedPool != null) _storedPool.ReturnToPool(this);
			else DisposeProjectile(); //todo maybe track active projectiles in weapon class...
			enabled = _active = false;
		}

		public void ChangeDirection(Vector2 direction, bool interpMove = true)
		{
			_direction = direction.normalized;

			if(interpMove) Position += _direction * _speed * Time.deltaTime;
		}

		public void DisposeProjectile()
		{
			Destroy(gameObject);
		}

		public void Deactivate()
		{
			if(!_active) return;
			StartCoroutine(DelayRoutine());
		}

		private IEnumerator DelayRoutine()
		{
			_active = false;
			yield return _delay;
			OnDeactivate?.Invoke();
			gameObject.SetActive(false);
			_storedPool.ReturnToPool(this);
		}

		public Actor Owner => _owner;
		public int BounceTimes => _bounceTimes;
		public int MaxBounceTimes => _maxBounceTimes;
		public Vector2 Direction{ get { return _direction; } }
	}
}