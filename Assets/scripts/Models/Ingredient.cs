using UnityEngine;
using System.Collections;
using UnityEditor;

public class Ingredient : ScriptableObject
{
	public string Name;

	[MenuItem("Assets/Create/Ingredient")]
	public static void CreateAsset()
	{
		ScriptableObjectUtility.CreateAsset<Ingredient>();
	}
}
