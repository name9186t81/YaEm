using System;
using UnityEngine;

namespace YaEm
{
	public interface IController
	{
		Vector2 DesiredMoveDirection { get; }
		float DesiredRotation { get; }
		event Action <ControllerAction> OnAction;
	}
}