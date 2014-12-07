using UnityEngine;
using System.Collections;

public class IngredientsInventory
{
	private readonly IngredientCollections _collection;
	public IngredientCollections Collection { get { return _collection; } }

	public IngredientsInventory(IngredientCollections collection)
	{
		_collection = collection;
	}
}
