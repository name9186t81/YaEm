using UnityEngine;

namespace YaEm
{
	public abstract class ActorSO : ScriptableObject
	{
		[SerializeField] protected Actor prefab;
		public abstract Actor CreateActor();
	}
}