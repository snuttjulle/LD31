using UnityEngine;
using System.Collections;

public class Kitchen : MonoBehaviour
{
	public PotDialogBox PotDialogBox;
	public KitchenDatabase Database;

	private Button _button;

	void Start()
	{
		_button = GetComponent<Button>();
		ActivateButton();
	}

	private void OnPress(object sender)
	{
		PotDialogBox.Show();
	}

	public void ActivateButton()
	{
		_button.SetTriggerCallback(OnPress);
	}

	public IngredientsInventory GetInventory()
	{
		return new IngredientsInventory(Database.IngredientCollection[0]); //TODO: use level here instead of 0
	}
}
