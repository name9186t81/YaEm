using UnityEngine;

namespace YaEm
{
	public static class ColorTable
	{
		public static Color GetColor(int teamNumber)
		{
			switch (teamNumber)
			{
				case 1: return Color.red;
				case 2: return Color.green;
				case 3: return Color.blue;
				case 4: return Color.yellow;
				case 5: return Color.cyan;
				case 6: return Color.magenta;
				default: return Color.white;
			}
		}
	}
}