using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;

public class Food : ScriptableObject
{
	public string Name;
	public List<Ingredient> Ingredients;

	public bool HasIngredient(Ingredient ingredient)
	{
		return Ingredients.Contains(ingredient);
	}


	[MenuItem("Assets/Create/Food")]
	public static void CreateAsset()
	{
		ScriptableObjectUtility.CreateAsset<Food>();
	}
}
