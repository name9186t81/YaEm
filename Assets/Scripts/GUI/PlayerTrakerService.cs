using System;
using UnityEngine;

namespace YaEm.GUI
{
	public class PlayerTrakerService : MonoBehaviour, IService
	{
		[SerializeField] private Actor _player;
		[SerializeField] private PCController _playerController; //todo: implement interface to work with differents controllers
		/// <summary>
		/// called before Player is changed making the paramether as an new player
		/// </summary>
		public Action<Actor> OnPlayerChange;

		private void Awake()
		{
			if (!ServiceLocator.TrySet(this))
			{
				ServiceLocator.Register(this);
			}
		}

		public bool TryChangePlayer(Actor player)
		{
			if (player.TryChangeController(_playerController))
			{
				OnPlayerChange?.Invoke(player);
				_player = player;
				return true;
			}
			return false;
		}

		public Actor Player => _player;
	}
}