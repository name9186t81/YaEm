namespace YaEm
{
	public static class HealthExtensions
	{
		public static float Delta(this IHealth health) => (float)health.Current / health.Max;
	}
}
