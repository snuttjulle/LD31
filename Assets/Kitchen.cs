using UnityEngine;
using System.Collections;

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
		ActivateButton();
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
		return _levelData;
	}

	public void ActivateButton()
	{
		_button.SetTriggerCallback(OnPress);
	}

	public IngredientsInventory GetInventory()
	{
		return new IngredientsInventory(_levelData.IngredientCollection);
	}

	public void Cook(IngredientCollections ingredients)
	{
		_result = Database.GetDish(ingredients);
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
