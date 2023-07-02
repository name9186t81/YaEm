using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace YaEm
{
	//todo make separated weapons classes for both ranged and melee variants
	public class Weapon : MonoBehaviour
	{
		[SerializeField] private Actor _owner;
		[SerializeField] private Projectile _projectilePrefab;
		[SerializeField] private float _offset;
		[SerializeField] private float _originAngle;
		[SerializeField] private Transform _shootPoint;
		[SerializeField] private bool _debug;
		[SerializeField] private float _preFireDelay;
		[SerializeField] private float _delay;
		[SerializeField] private float _delayPerProjectile;
		[SerializeField] private int _projectilesCount;
		[SerializeField] private int _damage;
		private bool _isFiring = false;

		public event Action OnPreFireStart;
		public event Action OnPreFireEnd;
		public event Action OnFire;
		public event Action OnEndFire;

		public UnityEvent PreFireStart;
		public UnityEvent PreFireEnd;
		public UnityEvent Fire;
		public UnityEvent EndFire;

		private void Start()
		{
			OnPreFireStart += () => PreFireStart?.Invoke();
			OnPreFireEnd += () => PreFireEnd?.Invoke();
			OnFire += () => Fire?.Invoke();
			OnEndFire += () => EndFire?.Invoke();

			Vector2 originPoint = (Vector2)_owner.transform.position + Vector2Utils.VectorFromAngle(_originAngle) * _owner.Size + Vector2Utils.VectorFromAngle(_originAngle) * _offset * _owner.Size;
			transform.position = originPoint;
			if (_owner.CurrentController != null)
			{
				_owner.CurrentController.OnAction += ReadAction;
			}

			_owner.OnControllerChange += UpdateSubs;
		}

		private void UpdateSubs(IController arg1, IController arg2)
		{
			if(arg1 != null)
				arg1.OnAction -= ReadAction;
			if (arg2 != null)
				arg2.OnAction += ReadAction;
		}

		private void ReadAction(ControllerAction obj)
		{
			if (obj == ControllerAction.Fire && !_isFiring)
			{
				StartCoroutine(FiringRoutine());
			}
		}

		private IEnumerator FiringRoutine()
		{
			_isFiring = true;
			OnPreFireStart?.Invoke();
			yield return new WaitForSeconds(_preFireDelay);
			OnPreFireEnd?.Invoke();

			for (int i = 0; i < _projectilesCount; i++)
			{
				var obj = Instantiate<Projectile>(_projectilePrefab,_shootPoint.position ,default ,null);
				obj.DamageArgs = new DamageArgs(_owner, _damage, Vector2.zero);
				obj.ChangeDirection(_shootPoint.up);
				obj.TryChangeTeamNumber(_owner.TeamNumber);

				OnFire?.Invoke();
				yield return new WaitForSeconds(_delayPerProjectile);
			}

			OnEndFire?.Invoke();
			yield return new WaitForSeconds(_delay);
			_isFiring = false;
		}

		private void OnDrawGizmos()
		{
			if(!_debug) return;

			Gizmos.color = Color.red;
			Vector2 originPoint = (Vector2)_owner.transform.position + Vector2Utils.VectorFromAngle(_originAngle) * _owner.Size + Vector2Utils.VectorFromAngle(_originAngle) * _offset * _owner.Size;
			Gizmos.DrawWireSphere(originPoint, _owner.Size / 4);
			Gizmos.DrawWireSphere(_shootPoint.position, _owner.Size / 4);
		}
	}
}