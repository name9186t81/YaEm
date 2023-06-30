using System;
using UnityEngine;

namespace YaEm
{
	public interface IForce
	{
		/// <summary>
		/// Return force, takes worldPosition as a paramater
		/// </summary>
		public Func<Vector2, Vector2> ForceFunc { get; }
	}
}