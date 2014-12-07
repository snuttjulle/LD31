using UnityEngine;
using System.Collections;

public class PotDialogBox : MonoBehaviour
{
	public Button CloseButton;
	public Kitchen Kitchen;

	public DialogBox CookDialogBox;

	void Start()
	{
		CookDialogBox.SetHeight(70);
		CookDialogBox.SetWidth(50);
	}

	public void Show()
	{
		CookDialogBox.TransitionIn();
		CloseButton.SetTriggerCallback(CloseDialog);
	}

	public void Hide()
	{
		CookDialogBox.TransitionOut();
		Kitchen.ActivateButton();
	}

	void CloseDialog(object sender)
	{
		CookDialogBox.TransitionOut();
		CookDialogBox.SetOnTransitionComplete((x) =>
		{
			Kitchen.ActivateButton();
		});
	}
}
