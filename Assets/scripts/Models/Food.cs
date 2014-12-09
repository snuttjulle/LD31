using UnityEngine;
using System.Collections;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System.Collections.Generic;

public class Food : ScriptableObject
{
	public string Name;
	public List<Ingredient> Ingredients;

	public bool HasIngredient(Ingredient ingredient)
	{
		return Ingredients.Contains(ingredient);
	}

#if UNITY_EDITOR
	[MenuItem("Assets/Create/Food")]
	public static void CreateAsset()
	{
		ScriptableObjectUtility.CreateAsset<Food>();
	}
#endif
}
