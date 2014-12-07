using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class TableTimeline : MonoBehaviour
{
	public Queue<TableEvent> TableEvents;

	private float? _eventTriggerTime;
	private float _timer;

	private Action<TableEventType> _eventTriggerCallback;

	void Awake()
	{
		TableEvents = new Queue<TableEvent>();

		//TableEvents.Enqueue(new TableEvent(5, TableEventType.Order));
		//TableEvents.Enqueue(new TableEvent(10, TableEventType.HasOrderedYet));
		//TableEvents.Enqueue(new TableEvent(15, TableEventType.HasFoodYet));
		//TableEvents.Enqueue(new TableEvent(20, TableEventType.Pay));
		//TableEvents.Enqueue(new TableEvent(25, TableEventType.HasYetToPay));
	}

	void Update()
	{
		if (TableEvents.Count <= 0)
			return;

		if (!_eventTriggerTime.HasValue)
			_eventTriggerTime = TableEvents.Peek().Time;

		_timer += Time.deltaTime;

		if (_timer > _eventTriggerTime.Value)
		{
			_eventTriggerTime = null;
			TableEvent evt = TableEvents.Dequeue();

			if (_eventTriggerCallback != null)
				_eventTriggerCallback(evt.EventType);
		}
	}

	public void SetEventTriggerCallback(Action<TableEventType> callback)
	{
		_eventTriggerCallback = callback;
	}

	public void Reset()
	{
		_timer = 0.0f;
	}
}
