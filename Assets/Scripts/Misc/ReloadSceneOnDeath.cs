using UnityEngine;
using UnityEngine.SceneManagement;

namespace YaEm
{
    //used in debug builds
    public class ReloadSceneOnDeath : MonoBehaviour
    {
        [SerializeField] private Actor _tracked;

		private void Start()
		{
			if (_tracked is IProvider<IHealth> prov)
			{
				prov.Value.OnDeath += (_) => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
			}
		}
	}
}