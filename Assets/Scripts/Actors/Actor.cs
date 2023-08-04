using System;
using UnityEngine;

namespace YaEm {
	[DisallowMultipleComponent]
	public abstract class Actor : MonoBehaviour
	{
		[SerializeField] private int _teamNumber;
		[SerializeField] private string _name;
		[SerializeField] private float _size;
		[SerializeField] protected bool _debug;
		private IController _controller;
		private Transform _cached;

		/// <summary>
		/// first argument is old team, second is new
		/// </summary>
		public event Action<int, int> OnTeamNumberChange;
		/// <summary>
		/// first controller is an old one, second is new
		/// </summary>
		public event Action<IController, IController> OnControllerChange;
		private void Awake()
		{
			_cached = transform;
			Init();
		}

		protected virtual void Init() { }

		public bool TryChangeController(IController newController)
		{
			//todo implement prevention mechanism to avoid mindless controller changes
			OnControllerChange?.Invoke(_controller, newController);
			_controller = newController;
			return true;
		}

		public bool TryChangeTeamNumber(int newTeamNumber)
		{
			OnTeamNumberChange?.Invoke(_teamNumber, newTeamNumber);
			_teamNumber = newTeamNumber;
			return true;	
		}

		protected virtual void OnDrawGizmos()
		{
			if (!_debug) return;

			Gizmos.color = Color.green;
			Gizmos.DrawWireSphere(transform.position, _size);
		}

		public Transform Transform { get { return _cached; } }
		public Vector2 Position { get { return _cached.position; } set { _cached.position = value; } }
		public string Name { get { return _name; } }
		public float Size { get { return _size; } }
		public int TeamNumber => _teamNumber;
		public IController CurrentController{ get { return _controller; } }	
	}
}