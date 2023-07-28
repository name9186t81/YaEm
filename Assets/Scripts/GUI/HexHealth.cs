using UnityEngine;
using UnityEngine.UI;

namespace YaEm.GUI
{
    public sealed class HexHealth : GUIElement
	{
		//todo make one array out of 2 arrays and work with it
        [SerializeField] private Image[] _upperHexes;
        [SerializeField] private Image[] _lowerHexes;
		[SerializeField] private bool _startFromUpper;
		protected override void Init()
		{
			for (int i = 0; i < _upperHexes.Length; i++)
			{
				_upperHexes[i].fillMethod = Image.FillMethod.Horizontal;
				_upperHexes[i].fillAmount = 1;
			}

			for (int i = 0; i < _lowerHexes.Length; i++)
			{
				_lowerHexes[i].fillMethod = Image.FillMethod.Horizontal;
				_lowerHexes[i].fillAmount = 1;
			}

			if (Player != null) PlayerChanged(Player);
		}

		protected override void PlayerChanged(Actor player)
		{
			if (!(player is IProvider<IHealth> prov))
			{
				UpdateHexes(0);
				return;
			}

			if (Player != null && Player is IProvider<IHealth> prov2)
			{
				prov2.Value.OnDamage -= RegisterDamage;
				prov2.Value.OnDeath -= RegisterDeath;
			}

			prov.Value.OnDamage += RegisterDamage;
			prov.Value.OnDeath += RegisterDeath;
		}

		private void RegisterDeath(DamageArgs args)
		{
			UpdateHexes(0);
		}

		private void RegisterDamage(DamageArgs args) 
		{
			UpdateHexes((Player as IProvider<IHealth>).Value.Delta());
		}

		private void UpdateHexes(float deltaHealth)
		{
			int toRemove = (int)(TotalHexes * (1 - deltaHealth));
			if (toRemove == 0) return;

			Debug.Log(toRemove);
			bool isEven = toRemove % 2 == 0;
			int halfed = toRemove / 2;
			int lowersToDisable = 0, uppersToDisable = 0;
			if (_startFromUpper)
			{
				lowersToDisable = halfed;
				uppersToDisable = halfed + (isEven ? 0 : 1);
			}
			else
			{
				uppersToDisable = halfed;
				lowersToDisable = halfed + (isEven ? 0 : 1);
			}

			for (int i = 0; i < _upperHexes.Length; i++)
			{
				if (i < uppersToDisable)
					_upperHexes[_upperHexes.Length - 1 - i].fillAmount = 0;
				else
					_upperHexes[_upperHexes.Length - 1 - i].fillAmount = 1;
			}

			for (int i = 0; i < _lowerHexes.Length; i++)
			{
				if (i < lowersToDisable)
					_lowerHexes[_lowerHexes.Length - 1 - i].fillAmount = 0;
				else
					_lowerHexes[_lowerHexes.Length - 1 - i].fillAmount = 1;
			}
		}

		private int TotalHexes => _upperHexes.Length + _lowerHexes.Length;
	}
}