using System;

namespace YaEm
{
	public static class MovmentStrategyFactory
	{
		public static IMovmentStrategy GetStrategy(float speed, MovmentStrategy type)
		{
			switch (type)
			{
				case MovmentStrategy.Linear:
					{
						return new LinearMovment(speed);
					}
				case MovmentStrategy.Sin:
					{
						return new SinusMovment(speed);
					}
				default:
					{
						throw new NotImplementedException();
					}
			}
		}
	}
}