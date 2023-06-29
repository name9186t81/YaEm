using System;
using System.Collections.Generic;

namespace YaEm
{
	public static class ServiceLocator
	{
		private static Dictionary<Type, object> _services = new Dictionary<Type, object>();

		public static void Register<T>(T instance) where T : class
		{
			_services.Add(typeof(T), instance);
		}

		public static bool TryGet<T>(out T instance) where T : class
		{
			if (_services.TryGetValue(typeof(T), out object obj))
			{
				instance = obj as T;
				return true;
			}
			instance = default;
			return false;
		}

		public static T Get<T>() where T : class
		{
			if (_services.TryGetValue(typeof(T), out object obj))
			{
				return (T)obj;
			}
			return null;
		}
	}
}