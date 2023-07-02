using UnityEngine;

public class CameraFollow : MonoBehaviour
{
	[SerializeField] private Transform _tracked;

	private void Update()
	{
		if (_tracked != null)
		{
			transform.position = new Vector3(_tracked.position.x, _tracked.position.y, transform.position.z);
		}
	}
}
