using UnityEngine;
using System.Collections;

public class PotDialogBox : MonoBehaviour
{
	public Button CookButton;
	public Button CancelButton;
	public Kitchen Kitchen;
	public DialogBox CookDialogBox;
	public DialogBox IngredientsDialogBox;
	public ContentHandler CookingContentHandler;
	public ContentHandler IngredientsContentHandler;
	public GameObject TextPrefab;
	public GameObject PotTextPrefab;

	private IngredientsInventory _inventory;

	void Start()
	{
		_inventory = Kitchen.GetInventory();
		_inventory.SetDialogContentHandler(CookingContentHandler, PotTextPrefab);

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
			var label = (GameObject)Object.Instantiate(TextPrefab, IngredientsContentHandler.transform.position, new Quaternion());
			label.transform.parent = IngredientsContentHandler.transform;
			label.transform.localScale = new Vector3(1, 1, 1); //wtf? Why do I need to do this?
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

			GameDataHandler data = label.AddComponent<GameDataHandler>();
			data.SetData(typeof(Ingredient), ing);
			Button button = label.GetComponent<Button>();
			button.MultiPress = true;
			button.SetTriggerCallback(OnIngredientAdd);
		}

		CookDialogBox.TransitionIn();
		CancelButton.SetTriggerCallback(CloseDialog);

		IngredientsDialogBox.TransitionIn();
	}

	private void OnIngredientAdd(object sender)
	{
		GameDataHandler data = ((Button)sender).GetComponent<GameDataHandler>();
		_inventory.AddIngredientToPot((Ingredient)data.GetData());
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
