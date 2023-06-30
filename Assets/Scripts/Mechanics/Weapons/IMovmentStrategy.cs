using UnityEngine;

namespace YaEm
{
    public interface IMovmentStrategy
    {
		Vector2 Move(Vector2 direction);
    }
}