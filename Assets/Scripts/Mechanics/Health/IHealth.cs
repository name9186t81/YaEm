using System;

namespace YaEm
{
	public interface IHealth
	{
		/// <summary>
		/// returns taken damage
		/// </summary>
		/// <param name="args"></param>
		/// <returns></returns>
		int TakeDamage(DamageArgs args);
		/// <summary>
		/// returns taken damage
		/// </summary>
		/// <param name="args"></param>
		/// <returns></returns>
		int TryDamage(DamageArgs args);
		int Max { get; }
		int Current { get; }
		event Action<DamageArgs> OnDamage;
		event Action<DamageArgs> OnDeath;
	}
}