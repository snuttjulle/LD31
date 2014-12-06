using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

public class FoodRequest : ScriptableObject
{
	public List<Food> RequestedFood;

	[MenuItem("Assets/Create/FoodRequest")]
	public static void CreateAsset()
	{
		ScriptableObjectUtility.CreateAsset<FoodRequest>();
	}
}
