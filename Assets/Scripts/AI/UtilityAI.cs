using System;

namespace YaEm.AI
{
	public sealed class UtilityAI
	{
		private readonly IUtility[] _utilities;
		private IUtility _currentBest;

		public UtilityAI(AIController controller, IUtility[] utilities)
		{
			_utilities = utilities;

			if (_utilities == null || _utilities.Length == 0)
			{
				throw new ArgumentException();
			}

			for (int i = 0; i < _utilities.Length; i++)
			{
				_utilities[i].Init(controller);
			}
		}

		public void Update()
		{
			IUtility best = UpdateCurrentUtility();

			if (_currentBest != best)
			{
				_currentBest.Undo();
				_currentBest = best;
				_currentBest.PreExecute();
			}

			_currentBest.Execute();
		}

		private IUtility UpdateCurrentUtility()
		{
			IUtility best = null;
			float max = float.MinValue;

			for (int i = 0, length = _utilities.Length; i < length; i++)
			{
				float val = _utilities[i].GetEffectivness();
				if (val > max)
				{
					max = val;
					best = _utilities[i];
				}
			}

			return best;
		}
	}
}
