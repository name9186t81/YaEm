using UnityEngine;

namespace YaEm.Effects
{
	public class DeathObject : MonoBehaviour
	{
		[SerializeField] private Actor _tracked;
		[SerializeField] private GameObject _object;
		[SerializeField] private float _lifeTime;

		private void Start()
		{
			_object.SetActive(false);
			if (_tracked is IProvider<IHealth> prov)
			{
				prov.Value.OnDeath += (_) =>
				{
					_object.transform.parent = null;
					_object.SetActive(true);
					Destroy(_object, _lifeTime);
				};
			}
		}
	}
}