namespace YaEm
{
	public interface IProvider<T>
	{
		T Value { get; }
	}
}