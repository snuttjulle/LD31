using UnityEngine;
using System.Collections;
#if UNITY_EDITOR
using UnityEditor;
#endif

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

#if UNITY_EDITOR
	[MenuItem("Assets/Create/TimelineSettings")]
	public static void CreateAsset()
	{
		ScriptableObjectUtility.CreateAsset<TimelineEvent>();
	}
#endif
}
