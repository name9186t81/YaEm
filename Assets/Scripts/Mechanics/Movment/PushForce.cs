using System;
using System.Collections.Generic;
using UnityEngine;

namespace YaEm
{
	public class ParameterizedForce : IForce
	{
		private Dictionary<ParameterizedForceKey, object> _parameters = new Dictionary<ParameterizedForceKey, object>();
		private Func<Vector2, Vector2> _forceFunc;
		public Func<Vector2, Vector2> ForceFunc => _forceFunc;

		public ParameterizedForce() { }

		public ParameterizedForce SetForce(Func<Vector2, Vector2> force)
		{
			_forceFunc = force;
			return this;
		}

		public ParameterizedForce AddParameter(ParameterizedForceKey key, object value)
		{
			_parameters.Add(key, value);
			return this;
		}

		public ParameterizedForce SetParameter(ParameterizedForceKey key, object value)
		{
			_parameters[key] = value;
			return this;
		}

		public object GetParameter(ParameterizedForceKey key)
		{
			return _parameters[key];
		}
	}

	public enum ParameterizedForceKey
	{
		PushPosition,
		PushDirection,
		ElapsedTime,
		MaxTime,
		Motor
	}
}