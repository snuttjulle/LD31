using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;
using System;

public class KitchenDatabase : ScriptableObject
{
	public List<Food> Dishes;
	public List<IngredientCollections> IngredientCollection;
	public List<Level> Levels;
	//public List<Ingredient> Ingredients;

	public Level GetLevelData(int level)
	{
		if (level < 0 || level > Levels.Count)
			throw new ArgumentOutOfRangeException("Invalid Level");

		return Levels[level];
	}

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
