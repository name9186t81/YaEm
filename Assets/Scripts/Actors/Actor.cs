using UnityEngine;

public abstract class Actor : MonoBehaviour
{
	[SerializeField] private int _teamNumber;
	[SerializeField] private string _name;
	private Transform _cached;

	private void Awake()
	{
		_cached = transform;
		Init();
	}

	protected virtual void Init() { }

	public Transform Transform{ get { return _cached; } }
	public Vector2 Position { get { return _cached.position; } set { _cached.position = value; } }
	public string Name { get { return _name; } }
}
