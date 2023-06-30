using System;
using UnityEngine;

namespace YaEm
{
	public class DamageArgs : EventArgs
	{
		public Actor Attacker;
		public int Damage;
		public Vector2 AttackPoint;
	}
}