namespace YaEm.AI
{
	public interface IUtility
	{
		void Init(AIController controller);
		float GetEffectivness();
		void PreExecute();
		void Execute();
		void Undo();
	}
}
