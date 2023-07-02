using UnityEngine;

namespace YaEm
{
	public class TeamColorAdapter : MonoBehaviour
	{
		[SerializeField] private Actor _attached;
		[SerializeField] private Renderer[] _spriteRenderers;
		[SerializeField] private TrailRenderer[] _trailRenderers;
		[SerializeField] private LineRenderer[] _lineRenderers;
		[SerializeField] private ParticleSystem[] _particleSystems;
		[SerializeField] private bool _alphaUnneffected = true;

		private void Start()
		{
			ChangeColors(_attached.TeamNumber);

			_attached.OnTeamNumberChange += UpdateColor;
		}

		private void UpdateColor(int arg1, int arg2)
		{
			ChangeColors(arg2);
		}

		private void ChangeColors(int num)
		{
			Color color = ColorTable.GetColor(num);
			for (int i = 0; i < _spriteRenderers.Length; i++)
			{
				_spriteRenderers[i].material.color = _alphaUnneffected ? new Color(color.r, color.g, color.b, _spriteRenderers[i].material.color.a) : color;
			}

			for (int i = 0; i < _trailRenderers.Length; i++)
			{
				_trailRenderers[i].startColor = _alphaUnneffected ? new Color(color.r, color.g, color.b, _trailRenderers[i].startColor.a) : color;
			}

			for (int i = 0; i < _lineRenderers.Length; i++)
			{
				_lineRenderers[i].startColor = _alphaUnneffected ? new Color(color.r, color.g, color.b, _lineRenderers[i].startColor.a) : color;
			}

			for (int i = 0; i < _particleSystems.Length; i++)
			{
				_particleSystems[i].startColor = _alphaUnneffected ? new Color(color.r, color.g, color.b, _particleSystems[i].startColor.a) : color;
			}
		}
	}
}