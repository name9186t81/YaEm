namespace YaEm
{
	public static class SpreadFactory
	{
		public static ISpread GetSpread() { return new FixedSpread(); }
		public static ISpread GetSpread(float angle, int iterations, Actor owner) { return new AngleSpread(owner, angle, iterations); }
	}
}