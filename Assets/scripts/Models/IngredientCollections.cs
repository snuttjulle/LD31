using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public class IngredientCollections
{
	private List<Ingredient> _ingredientCollection;
	public List<Ingredient> IngredientCollection { get { return _ingredientCollection; } }

	public IngredientCollections(List<Food> dishes)
	{
		_ingredientCollection = new List<Ingredient>();

		foreach (Food food in dishes)
		{
			foreach (Ingredient ing in food.Ingredients)
			{
				if (!_ingredientCollection.Contains(ing))
					_ingredientCollection.Add(ing);
			}
		}
	}

	public IngredientCollections()
	{
		_ingredientCollection = new List<Ingredient>();
	}

	public void AddIngredient(Ingredient ingredient)
	{
		if (!_ingredientCollection.Contains(ingredient))
			_ingredientCollection.Add(ingredient);
	}
}
