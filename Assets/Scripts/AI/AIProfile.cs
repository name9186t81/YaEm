using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace YaEm.AI
{
	[Serializable]
	public struct AIProfile
	{
		[Range(-1f, 1f)]
		public float Aggresivness;
		[Range(-1f, 1f)]
		public float TeamWork;

		public static AIProfile Mix(AIProfile first, AIProfile second)
		{
			return new AIProfile()
			{
				Aggresivness = Random.Range(first.Aggresivness, second.Aggresivness),
				TeamWork = Random.Range(first.TeamWork, second.TeamWork)
			};
		}
	}
}
