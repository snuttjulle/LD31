using UnityEngine;
using System.Collections;

public enum TableEventType { Order, HasOrderedYet, HasFoodYet, Pay, HasYetToPay, Leave }

public class TableEvent
{
	public float Time; //Time when the event will occur
	public TableEventType EventType; //what type of event that will occur at the table

	public TableEvent(float time, TableEventType type)
	{
		Time = time;
		EventType = type;
	}
}
