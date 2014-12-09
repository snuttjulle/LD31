using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Kitchen : MonoBehaviour
{
	public GameController Controller;
	public PotDialogBox PotDialogBox;
	public KitchenDatabase Database;
	public DishDelivery CookedDishDialogBoxPrefab;

	private Button _button;
	private Food _result;

	private Level _levelData;

	private bool _isCooking = false;

	void Start()
	{
		_button = GetComponent<Button>();
	}

	private void OnPress(object sender)
	{
		if (!_isCooking)
			PotDialogBox.Show();
		else
			((Button)sender).DontSwallowCallback();
	}

	public Level LoadLevelData(int level)
	{
		_levelData = Database.GetLevelData(level);
		PotDialogBox.InitPotDialog();
		return _levelData;
	}

	public uint GetTotalLevels()
	{
		return Database.GetTotalLevels();
	}

	public void ActivateButton()
	{
		_button.SetTriggerCallback(OnPress);
	}

	public void DeactivateButton()
	{
		_button.RemoveTriggerCallback();
	}

	public IngredientsInventory GetInventory()
	{
		IngredientCollections collection = _levelData.IngredientCollection;

		if (Controller.Day > 4) //after 5 days the list is shuffled
			collection.Shuffle();

		return new IngredientsInventory(collection);
	}

	public void Cook(IngredientCollections ingredients)
	{
		_result = Database.GetDish(ingredients);
		Debug.Log("Cooked " + _result.Name);
		Controller.Cook();
		Controller.StartProgressBar(1, new Vector3(103, -10, -15), OnCookingComplete); //TODO: get time from dish
		_isCooking = true;
	}

	private void OnCookingComplete(object sender)
	{
		Controller.UnCook();

		var cookedDish = (DishDelivery)Object.Instantiate(CookedDishDialogBoxPrefab, CookedDishDialogBoxPrefab.transform.position, new Quaternion());
		cookedDish.DisplayDelivery(_result);

		//ActivateButton();
		_isCooking = false;
	}
}
