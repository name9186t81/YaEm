using UnityEngine;

namespace YaEm
{
	//todo: this class is not used so remove it perhaps
	public abstract class ActorSO : ScriptableObject
	{
		[SerializeField] protected Actor prefab;
		public abstract Actor CreateActor();
	}
}