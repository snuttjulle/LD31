using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public class IngredientCollections : ScriptableObject
{
	public List<Ingredient> IngredientCollection;

	public IngredientCollections()
	{
		IngredientCollection = new List<Ingredient>();
	}

	[MenuItem("Assets/Create/IngredientCollections")]
	public static void CreateAsset()
	{
		ScriptableObjectUtility.CreateAsset<IngredientCollections>();
	}
}
