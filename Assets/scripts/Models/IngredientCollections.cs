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

	public void Shuffle()
	{
		//_ingredientCollection = (List<Ingredient>)RandomUtils.Shuffle(IngredientCollection);
		//_ingredientCollection = _ingredientCollection.OrderBy(item => RandomUtils.GetRandom.Next());

		for (int i = 0; i < _ingredientCollection.Count; i++)
		{
			var temp = _ingredientCollection[i];
			int randomIndex = Random.Range(i, _ingredientCollection.Count);
			_ingredientCollection[i] = _ingredientCollection[randomIndex];
			_ingredientCollection[randomIndex] = temp;
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
