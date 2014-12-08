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
		if(ingredients.IngredientCollection.Count <= 0)
			return null;

		foreach (Food food in Dishes)
		{
			int numIng = food.Ingredients.Count;
			if (numIng != ingredients.IngredientCollection.Count)
				continue;

			if (food.HasIngredient(ingredients.IngredientCollection[0]))
			{
				bool contains = true;

				foreach (Ingredient ing in ingredients.IngredientCollection)
				{
					contains = food.HasIngredient(ing);

					if (!contains)
						break;
				}

				if (contains)
					return food;
			}
		}

		foreach (Food food in Dishes)
		{
			if (food.Name == "Junk")
				return food;
		}
		
		return null;
	}

	[MenuItem("Assets/Create/KitchenDatabase")]
	public static void CreateAsset()
	{
		ScriptableObjectUtility.CreateAsset<KitchenDatabase>();
	}
}
