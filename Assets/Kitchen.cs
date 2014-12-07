using UnityEngine;
using System.Collections;

public class Kitchen : MonoBehaviour
{
	public GameController Controller;
	public PotDialogBox PotDialogBox;
	public KitchenDatabase Database;

	private Button _button;

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

	public void ActivateButton()
	{
		_button.SetTriggerCallback(OnPress);
	}

	public IngredientsInventory GetInventory()
	{
		return new IngredientsInventory(Database.IngredientCollection[0]); //TODO: use level here instead of 0
	}

	public void Cook(IngredientCollections ingredients)
	{
		Food result = Database.GetDish(ingredients);
		Controller.Cook();
		Controller.StartProgressBar(10, new Vector3(103, -10, -15), OnCookingComplete);
		_isCooking = true;
	}

	private void OnCookingComplete(object sender)
	{
		_isCooking = false;
	}
}
