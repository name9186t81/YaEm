using UnityEngine;

namespace YaEm
{
	//todo make sinus working with different sinuses parameters
	public class SinusMovment : IMovmentStrategy
	{
		private float _elaspedTime;
		private float _speed;

		public SinusMovment(float speed)
		{
			_speed = speed;
		}

		public Vector2 Move(Vector2 direction)
		{
			_elaspedTime += Time.deltaTime;

			float sin1 = Mathf.Cos(_elaspedTime);
			Debug.Log(sin1);
			return new Vector2(sin1 / _speed, 1).Rotate(direction.AngleFromVector() * Mathf.Deg2Rad) * _speed;
		}
	}
}