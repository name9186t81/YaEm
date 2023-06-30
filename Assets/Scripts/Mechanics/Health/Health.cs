using System;

namespace YaEm
{
	public class Health : IHealth
	{
		private int _max;
		private int _current;

		public Health(int max)
		{
			_max = max;
		}

		public Health(int max, int current)
		{
			_max = max;
			_current = current;
		}

		public int Max => _max;

		public int Current => _current;

		public event Action<DamageArgs> OnDamage;
		public event Action<DamageArgs> OnDeath;

		public int TakeDamage(DamageArgs args)
		{
			_current -= args.Damage;

			if (args.Damage > 0)
			{
				OnDamage?.Invoke(args);
			}

			if (_current < 0)
			{
				OnDeath?.Invoke(args);
			}

			return args.Damage;
		}

		public int TryDamage(DamageArgs args)
		{
			return args.Damage;
		}
	}
}