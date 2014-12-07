using UnityEngine;
using System.Collections;
using UnityEditor;

public class TimelineEvent : ScriptableObject
{
	public int MinNothingTime;
	public int MaxNothingTime;

	public int MinOrderTime;
	public int MaxOrderTime;

	public int MinOrderTimeDelay;
	public int MaxOrderTimeDelay;

	public int MinPrepareFoodTime;
	public int MaxPrepareFoodTime;

	public int MinWaitToLeaveTime;
	public int MaxWaitToLeaveTime;

	[MenuItem("Assets/Create/TimelineSettings")]
	public static void CreateAsset()
	{
		ScriptableObjectUtility.CreateAsset<TimelineEvent>();
	}
}
