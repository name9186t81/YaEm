using UnityEngine;
using UnityEngine.UI;

namespace YaEm.GUI
{
	public sealed class GUIRecolorer : GUIElement
	{
		[SerializeField] private Image[] _images;

		protected override void Init()
		{
			if (Player != null)
			{
				Recolor(Player.TeamNumber);
				Player.OnTeamNumberChange += UpdateTeam;
			}
		}

		protected override void PlayerChanged(Actor player)
		{
			if(Player != null) Player.OnTeamNumberChange -= UpdateTeam;
			Recolor(player.TeamNumber);
			player.OnTeamNumberChange += UpdateTeam;
		}

		private void UpdateTeam(int arg1, int arg2)
		{
			Recolor(arg2);
		}

		private void Recolor(int teamNum)
		{
			for (int i = 0; i < _images.Length; i++)
			{
				_images[i].color = ColorTable.GetColor(teamNum);
			}
		}
	}
}