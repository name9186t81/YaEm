using UnityEngine;

namespace YaEm
{
	public abstract class ActorSO : ScriptableObject
	{
		public abstract Actor CreateActor();
	}
}