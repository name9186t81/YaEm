using System;
using UnityEngine;

namespace YaEm
{
	public sealed class BaseForce : IForce
    {
        private readonly Func<Vector2, Vector2> _force;

		public BaseForce(Func<Vector2, Vector2> force)
		{
			_force = force;
		}

		public Func<Vector2, Vector2> ForceFunc => _force;
    }
}