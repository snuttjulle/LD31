using UnityEngine;
using System.Collections;

public class IngredientsInventory
{
	private readonly IngredientCollections _collection;
	public IngredientCollections Collection { get { return _collection; } }

	private IngredientCollections _ingredientsInPot;
	public IngredientCollections IngredientsInPot { get { return _ingredientsInPot; } }

	public int NumIngredientsInPot { get { return _ingredientsInPot.IngredientCollection.Count; } }

	private ContentHandler _cookDialogContentHandler;
	private GameObject _labelPrefab;

	const float default_pos_y = 50;
	private float _listPosY = default_pos_y;

	public IngredientsInventory(IngredientCollections collection)
	{
		_collection = collection;
		_ingredientsInPot = new IngredientCollections();
	}

	public void SetDialogContentHandler(ContentHandler handler, GameObject labelPrefab)
	{
		_cookDialogContentHandler = handler;
		_labelPrefab = labelPrefab;
	}

	public void NewPot()
	{
		_cookDialogContentHandler.CleanContent();
		_ingredientsInPot.IngredientCollection.Clear();
		_listPosY = default_pos_y;
	}

	public void AddIngredientToPot(Ingredient ingredient)
	{
		if (_ingredientsInPot.IngredientCollection.Contains(ingredient))
		{
			Debug.Log("that ingredient already exists");
			return;
		}

		_ingredientsInPot.IngredientCollection.Add(ingredient);
		var label = (GameObject)Object.Instantiate(_labelPrefab, _cookDialogContentHandler.transform.position, new Quaternion());
		label.transform.parent = _cookDialogContentHandler.transform;
		Vector3 pos = label.transform.localPosition;
		pos.x = -45;
		pos.y = _listPosY;
		pos.z = -20;
		label.transform.localPosition = pos;
		_listPosY -= 15;

		label.GetComponent<TextMesh>().text = ingredient.Name;

		Debug.Log("Ingredients in pot:");
		foreach (Ingredient ing in _ingredientsInPot.IngredientCollection)
			Debug.Log(ing.Name);
	}

	//Let's reset everything instead, much easier for the GUI (for me)
	//public void RemoveIngredientFromPot(Ingredient ingredient)
	//{
	//	if (_ingredientsInPot.IngredientCollection.Contains(ingredient))
	//	{
	//		_ingredientsInPot.IngredientCollection.Remove(ingredient);
	//	}
	//}
}
