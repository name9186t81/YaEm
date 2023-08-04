using UnityEngine;

namespace YaEm
{
	[DisallowMultipleComponent]
	public class TeamSync : MonoBehaviour
	{
		[SerializeField] private Actor _tracked;
		[SerializeField] private Actor[] _toChange;

		private void Awake()
		{
			_tracked.OnTeamNumberChange += Change;
			Change(_tracked.TeamNumber, 0);
		}

		private void Change(int arg1, int arg2)
		{
			for (int i = 0; i < _toChange.Length; i++)
			{
				_toChange[i].TryChangeTeamNumber(arg1);
			}
		}
	}
}