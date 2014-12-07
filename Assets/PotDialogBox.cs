using UnityEngine;
using System.Collections;

public class PotDialogBox : MonoBehaviour
{
	public Button CookButton;
	public Button CancelButton;
	public Kitchen Kitchen;
	public DialogBox CookDialogBox;
	public DialogBox IngredientsDialogBox;
	public ContentHandler IngredientsContentHandler;
	public GameObject TextPrefab;

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
		float listPosY = 33;
		float listPosX = -53;
		foreach (Ingredient ing in _inventory.Collection.IngredientCollection)
		{
			Debug.Log(ing.Name);
			var label = (GameObject)Instantiate(TextPrefab, IngredientsContentHandler.transform.position, new Quaternion());
			label.transform.parent = IngredientsContentHandler.transform;
			Vector3 pos = label.transform.localPosition;
			pos.x = listPosX;
			pos.z = -15;
			pos.y = listPosY;
			listPosY -= 15;
			label.transform.localPosition = pos;
			label.GetComponent<TextMesh>().text = ing.Name;

			if (listPosY < -50)
			{
				listPosY = 48;
				listPosX = 15;
			}

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
