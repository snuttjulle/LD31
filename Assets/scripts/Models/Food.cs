using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;

public class Food : ScriptableObject
{
	public string Name;
	public List<Ingredient> Ingredients;

	[MenuItem("Assets/Create/Food")]
	public static void CreateAsset()
	{
		ScriptableObjectUtility.CreateAsset<Food>();
	}
}
