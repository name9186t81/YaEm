using UnityEngine;
using UnityEngine.SceneManagement;

public class BootsTrap : MonoBehaviour
{
	[SerializeField] private string _loadSceneName;

	private void Start()
	{
		
		SceneManager.LoadScene(_loadSceneName);
	}
}
