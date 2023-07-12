using UnityEngine;

namespace YaEm
{
	public interface ISpread
	{
		Vector2 GetSpreadedDirection(Vector2 origDirection);
		Vector2 PeekSpreadedDirection(Vector2 origDirection);
	}
}