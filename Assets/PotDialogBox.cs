using UnityEngine;
using System.Collections;

public class PotDialogBox : MonoBehaviour
{
	public Button CookButton;
	public Button CancelButton;
	public Kitchen Kitchen;

	public DialogBox CookDialogBox;
	public DialogBox IngredientsDialogBox;

	private IngredientsInventory _inventory;

	void Start()
	{
		_inventory = Kitchen.GetInventory();

		CookDialogBox.SetHeight(70);
		CookDialogBox.SetWidth(50);

		IngredientsDialogBox.SetHeight(50);
		IngredientsDialogBox.SetWidth(70);
	}

	public void Show()
	{
		foreach (Ingredient ing in _inventory.Collection.IngredientCollection)
		{
			Debug.Log(ing.Name);
		}

		CookDialogBox.TransitionIn();
		CancelButton.SetTriggerCallback(CloseDialog);

		IngredientsDialogBox.TransitionIn();
	}

	public void Hide()
	{
		CookDialogBox.TransitionOut();
		Kitchen.ActivateButton();
	}

	void CloseDialog(object sender)
	{
		CookDialogBox.TransitionOut();
		IngredientsDialogBox.TransitionOut();
		CookDialogBox.SetOnTransitionComplete((x) => {
			Kitchen.ActivateButton();
		});
	}
}
