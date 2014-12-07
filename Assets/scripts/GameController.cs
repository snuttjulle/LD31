﻿using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{
	public Chef Chef;
	public CookingDevice[] CookingDevices;
	public Burner Burner;
	public Fumes Fumes;
	public OverlayHandler OverlayHandler;

	AnimationStateMachine _animationStateMachine;

	void Start()
	{
		_animationStateMachine = new AnimationStateMachine(this);
	}

	void Update()
	{
		if (Input.GetKeyUp(KeyCode.W))
			_animationStateMachine.SetAnimationState(AnimationStates.Cooking);
		else if (Input.GetKeyUp(KeyCode.Q))
			_animationStateMachine.SetAnimationState(AnimationStates.NotCooking);
		else if (Input.GetKeyUp(KeyCode.D))
			OverlayHandler.Show();
		else if (Input.GetKeyUp(KeyCode.F))
			OverlayHandler.Hide();
		else if (Input.GetKeyUp(KeyCode.T))
			Chef.GiveThumbsUp();
	}
}
