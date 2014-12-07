using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public class IngredientCollections : ScriptableObject
{
	public List<Ingredient> IngredientCollection;

	[MenuItem("Assets/Create/IngredientCollections")]
	public static void CreateAsset()
	{
		ScriptableObjectUtility.CreateAsset<IngredientCollections>();
	}
}
