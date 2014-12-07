using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;

public class KitchenDatabase : ScriptableObject
{
	public List<Food> Dishes;
	public List<IngredientCollections> IngredientCollection;
	//public List<Ingredient> Ingredients;

	public Food GetDish(IngredientCollections ingredients)
	{
		return Dishes[0];
	}

	[MenuItem("Assets/Create/KitchenDatabase")]
	public static void CreateAsset()
	{
		ScriptableObjectUtility.CreateAsset<KitchenDatabase>();
	}
}
