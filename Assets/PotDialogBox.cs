using UnityEngine;
using System.Collections;

public class PotDialogBox : MonoBehaviour
{
	public Button CookButton;
	public Button CancelButton;
	public Kitchen Kitchen;

	public DialogBox CookDialogBox;
	public DialogBox IngredientsDialogBox;

	void Start()
	{
		CookDialogBox.SetHeight(70);
		CookDialogBox.SetWidth(50);

		IngredientsDialogBox.SetHeight(50);
		IngredientsDialogBox.SetWidth(70);
	}

	public void Show()
	{
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
