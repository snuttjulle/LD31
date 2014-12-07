﻿using UnityEngine;
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
		SetupLevel(0);
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
		Level data = Kitchen.LoadLevelData(level);

		//disable hitboxes for tables
		foreach (Table table in Tables)
		{
			table.GetComponent<BoxCollider2D>().enabled = false;
		}

		//randomize table
		for (int i = 0; i < data.ActiveTables.Count; i++)
		{
			//Tables[i].GetComponent<BoxCollider2D>().enabled = true;
			Tables[i].Requests = new FoodRequest(data.ActiveTables[i], data.Dishes);
			
			//setup timeline
			Queue<TableEvent> events = new Queue<TableEvent>();

			float timestamp = 0.0f;
			int moment;

			//nothing
			moment = RandomUtils.GetRandom.Next(data.TimelineSettings.MinNothingTime, data.TimelineSettings.MaxNothingTime);
			timestamp += moment;
			events.Enqueue(new TableEvent(timestamp, TableEventType.Order));

			//order
			moment= RandomUtils.GetRandom.Next(data.TimelineSettings.MinOrderTime, data.TimelineSettings.MaxOrderTime);
			timestamp += moment;
			events.Enqueue(new TableEvent(timestamp, TableEventType.HasOrderedYet));

			//order delay
			moment = RandomUtils.GetRandom.Next(data.TimelineSettings.MinOrderTimeDelay, data.TimelineSettings.MinOrderTimeDelay);
			timestamp += moment;
			events.Enqueue(new TableEvent(timestamp, TableEventType.HasFoodYet));

			//food
			moment = RandomUtils.GetRandom.Next(data.TimelineSettings.MinPrepareFoodTime, data.TimelineSettings.MaxPrepareFoodTime);
			timestamp += moment;
			events.Enqueue(new TableEvent(timestamp, TableEventType.Pay));

			//pay
			moment = RandomUtils.GetRandom.Next(data.TimelineSettings.MinWaitToLeaveTime, data.TimelineSettings.MaxWaitToLeaveTime);
			timestamp += moment;
			events.Enqueue(new TableEvent(timestamp, TableEventType.Leave));

			//leave

			Tables[i].InitTable(new FoodRequest(data.ActiveTables[i], data.Dishes), events);
		}
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
