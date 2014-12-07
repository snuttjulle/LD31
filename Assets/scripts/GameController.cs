using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class GameController : MonoBehaviour
{
	public Chef Chef;
	public CookingDevice[] CookingDevices;
	public Burner Burner;
	public Fumes Fumes;
	public OverlayHandler OverlayHandler;
	public ProgressBar ProgressBarPrefab;
	public List<Table> Tables;
	public Kitchen Kitchen;

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

	public void SetupLevel(int level)
	{
		FoodRequest request = new FoodRequest();

		List<Food> foods = new List<Food>();
		
		//foods.Add(new Food(
		//Tables[0].InitTable(
	}

	public void StartProgressBar(float time, Vector3 position, Action<object> callback)
	{
		var progressBar = (ProgressBar)UnityEngine.Object.Instantiate(ProgressBarPrefab, position, new Quaternion());
		Vector3 pos = progressBar.transform.position;
		pos.z = -15;
		progressBar.transform.position = pos;
		
		progressBar.ProgressTime = time;
		progressBar.SetCallback(callback);
		progressBar.Start();
	}

	public void Cook()
	{
		_animationStateMachine.SetAnimationState(AnimationStates.Cooking);
	}

	public void UnCook()
	{
		_animationStateMachine.SetAnimationState(AnimationStates.NotCooking);
	}
}
