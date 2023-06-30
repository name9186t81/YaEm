using System;
using UnityEngine;

namespace YaEm
{
	public class PCController : MonoBehaviour, IController
	{
		[SerializeField] private Actor _controlled;
		private Vector2 _direction;
		private float _rotation;

		public Vector2 DesiredMoveDirection => _direction;

		public float DesiredRotation => _rotation;

		public event Action<ControllerAction> OnAction;

		private void Awake()
		{
			_controlled.TryChangeController(this);
		}

		private void Update()
		{
			_direction = Vector2.zero;

			if (Input.GetKey(KeyCode.W)) _direction += Vector2.up;
			if (Input.GetKey(KeyCode.A)) _direction += Vector2.left;
			if (Input.GetKey(KeyCode.D)) _direction += Vector2.right;
			if (Input.GetKey(KeyCode.S)) _direction += Vector2.down;
			_direction.Normalize();

			Vector2 dir = _controlled.Position - (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
			_rotation = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + 90;

			if (Input.GetKey(KeyCode.Mouse0)) OnAction?.Invoke(ControllerAction.Fire);
		}
	}
}