using System.Collections.Generic;
using UnityEngine;

namespace YaEm.AI {
	public class AIVision : MonoBehaviour
	{
		[SerializeField] private float _range;
		[SerializeField] private float _angle = 360;
		[SerializeField] private float _scanFrequancy;
		[SerializeField] private LayerMask _scanMask;
		[SerializeField] private bool _debug;
		[SerializeField] private bool _canSeeThroughWalls;
		private float _delay;
		private AIController _controller;
		private IList<Actor> _cachedScan;
		private IReadOnlyList<Actor> _enemies;
		private IReadOnlyList<Actor> _alies;

		public IReadOnlyList<Actor> ActorsInRange => (IReadOnlyList<Actor>)_cachedScan;
		public IReadOnlyList<Actor> EnemiesInRange => _enemies;
		public IReadOnlyList<Actor> AliesInRange => _alies;


		private void Awake()
		{
			enabled = false;
		}

		public void Init(AIController controller)
		{
			enabled = true;
			_controller = controller;
		}

		private void Update()
		{
			_delay += Time.deltaTime;
			if (_delay > _scanFrequancy)
			{
				ForceScan();
			}
		}

		public void ForceScan()
		{
			_delay = 0;

			IList<Collider2D> hits = Physics2D.OverlapCircleAll(_controller.Position, _range, _scanMask);

			IList<Actor> listed = ClearForActors(hits);
			if (!_canSeeThroughWalls) listed = ClearForWalls(listed);
			if (_angle < 360) listed = ClearForAngle(listed);

			_cachedScan = listed;
			_enemies = SortForTeamNumber(_controller.Actor.TeamNumber, true);
			_alies = SortForTeamNumber(_controller.Actor.TeamNumber, false);
		}

		public IReadOnlyList<Actor> SortForTeamNumber(int teamNumber, bool excluded)
		{
			IList<Actor> list = new List<Actor>();
			foreach (var el in _cachedScan)
			{
				if (el.TeamNumber == teamNumber && !excluded || excluded && el.TeamNumber != teamNumber)
				{
					list.Add(el);
				}
			}
			return (IReadOnlyList<Actor>)list;
		}

		public IReadOnlyList<T> SortForT<T>() where T : class
		{
			IList<T> list = new List<T>();
			foreach (var el in _cachedScan)
			{
				if (el is T t)
				{
					list.Add(t);
				}
			}
			return (IReadOnlyList<T>)list;
		}

		private IList<Actor> ClearForActors(IList<Collider2D> hits)
		{
			IList<Actor> newList = new List<Actor>();

			foreach (var col in hits)
			{
				if (col.transform.TryGetComponent<Actor>(out var act))
				{
					newList.Add(act);
				}
			}
			return newList;
		}

		private IList<Actor> ClearForWalls(IList<Actor> hits)
		{
			IList<Actor> newList = new List<Actor>();
			bool haveCollider = Physics2D.OverlapPoint(_controller.Position);
			foreach (var col in hits)
			{
				if ((!haveCollider && !Physics2D.Linecast(_controller.Position, col.transform.position, _scanMask)) ||
					(haveCollider && Physics2D.LinecastAll(_controller.Position, col.transform.position, _scanMask).Length == 2))
				{
					newList.Add(col);
				}
			}
			return newList;
		}


		private IList<Actor> ClearForAngle(IList<Actor> hits)
		{
			IList<Actor> newList = new List<Actor>();
			float angle1 = -_angle / 2 + _controller.Actor.Transform.up.AngleFromVector();
			float angle2 = _angle / 2 + _controller.Actor.Transform.up.AngleFromVector();
			foreach (var col in hits)
			{
				Vector2 dir = (Vector2)col.transform.position - _controller.Position;
				float angle = dir.AngleFromVector();
				if (angle > angle1 && angle < angle2)
				{
					newList.Add(col);
				}
			}
			return newList;
		}

		private void OnValidate()
		{
			_angle = Mathf.Clamp(_angle, 0, 360);
			_range = Mathf.Max(_range, 0);
			_scanFrequancy = Mathf.Max(_scanFrequancy, 0);
		}

		private void OnDrawGizmos()
		{
			if (!_debug) return;

			Gizmos.color = Color.yellow;
			Gizmos.DrawWireSphere(transform.position, _range);

			Gizmos.color = Color.green;
			float radAngle = (_angle) * Mathf.Deg2Rad;
			Vector3 dir = Vector2Utils.VectorFromAngle(radAngle / 2 + transform.up.AngleFromVector() * Mathf.Deg2Rad);
			Vector3 dir2 = Vector2Utils.VectorFromAngle(-radAngle / 2 + transform.up.AngleFromVector() * Mathf.Deg2Rad);
			Gizmos.DrawLine(transform.position, dir * _range + transform.position);
			Gizmos.DrawLine(transform.position, dir2 * _range + transform.position);
		}
	}
}