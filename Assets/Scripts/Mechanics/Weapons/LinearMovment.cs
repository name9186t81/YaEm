using UnityEngine;

namespace YaEm
{
	public class LinearMovment : IMovmentStrategy
	{
		private float _speed;

		public LinearMovment(float speed)
		{
			_speed = speed;
		}

		public Vector2 Move(Vector2 direction)
		{
			return direction * _speed;
		}
	}
}