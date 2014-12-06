using UnityEngine;
using System.Collections;
using System;

public enum AnimationStates { NotCooking, Cooking };

public class AnimationStateMachine
{
	readonly GameController _controller;
	private AnimationStates _currentState;

	public AnimationStateMachine(GameController controller)
	{
		if (controller == null)
			throw new NullReferenceException();

		_controller = controller;
	}

	public void SetAnimationState(AnimationStates state)
	{
		if (state == AnimationStates.Cooking)
		{
			_controller.Burner.TurnOn();
			_controller.CookingDevices[0].gameObject.SetActive(true);
			_controller.Fumes.gameObject.SetActive(true);
		}
		else if (state == AnimationStates.NotCooking)
		{
			_controller.Burner.TurnOff();

			foreach (var device in _controller.CookingDevices)
				device.gameObject.SetActive(false);

			_controller.Fumes.gameObject.SetActive(false);
		}

		_currentState = state;
	}
}
