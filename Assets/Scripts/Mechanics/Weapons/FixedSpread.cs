using UnityEngine;

namespace YaEm {
	public class FixedSpread : ISpread
	{
		public Vector2 GetSpreadedDirection(Vector2 origDirection)
		{
			return origDirection;
		}

		public Vector2 PeekSpreadedDirection(Vector2 origDirection)
		{
			return origDirection;
		}
	}
}