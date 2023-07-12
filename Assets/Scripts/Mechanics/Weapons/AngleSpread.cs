using UnityEngine;

namespace YaEm
{
	public class AngleSpread : ISpread
	{
		private readonly Actor _tracked;
		private readonly float _angle;
		private readonly int _iterations;
		private int _currentIteration;

		public AngleSpread(Actor tracked, float angle, int iterations)
		{
			_tracked = tracked;
			_angle = angle;
			_iterations = iterations;
		}

		public Vector2 GetSpreadedDirection(Vector2 origDirection)
		{
			if (_currentIteration > _iterations) _currentIteration = 0;

			float angle = Mathf.Atan2(origDirection.y, origDirection.x) * Mathf.Rad2Deg;
			angle = angle - _angle / 2 + _angle / _iterations * _currentIteration;

			angle *= Mathf.Deg2Rad;
			_currentIteration++;
			return new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
		}

		public Vector2 PeekSpreadedDirection(Vector2 origDirection)
		{
			float angle = Mathf.Atan2(origDirection.y, origDirection.x) * Mathf.Rad2Deg;
			angle = angle - _angle / 2 + _angle / _iterations * _currentIteration;

			angle *= Mathf.Deg2Rad;
			return new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
		}
	}
}