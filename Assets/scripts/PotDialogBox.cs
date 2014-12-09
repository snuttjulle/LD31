using UnityEngine;
using System.Collections;

public class PotDialogBox : MonoBehaviour
{
	public Button CookButton;
	public Button CancelButton;
	public Button ResetButton;
	public Kitchen Kitchen;
	public DialogBox CookDialogBox;
	public DialogBox IngredientsDialogBox;
	public ContentHandler CookingContentHandler;
	public ContentHandler IngredientsContentHandler;
	public GameObject TextPrefab;
	public GameObject PotTextPrefab;

	private IngredientsInventory _inventory;
	private AudioSource _audioSource;

	public bool IsOpen { get; private set; }

	void Start()
	{
		
	}

	void Awake()
	{
		_audioSource = GetComponent<AudioSource>();
	}

	public void InitPotDialog()
	{
		_inventory = Kitchen.GetInventory(); //TODO: set inventory from gamecontroller
		_inventory.SetDialogContentHandler(CookingContentHandler, PotTextPrefab);

		ResetButton.MultiPress = true;

		CookDialogBox.SetHeight(70);
		CookDialogBox.SetWidth(50);

		IngredientsDialogBox.SetHeight(50);
		IngredientsDialogBox.SetWidth(70);

		float listPosY = 33;
		float listPosX = -53;

		foreach (Transform child in IngredientsContentHandler.transform)
			Object.Destroy(child.gameObject);

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
	}

	public void Show()
	{
		IsOpen = true;
		CookDialogBox.TransitionIn();
		CancelButton.SetTriggerCallback(CloseDialog);
		ResetButton.SetTriggerCallback(OnPotReset);
		CookButton.SetTriggerCallback(OnCook);

		IngredientsDialogBox.TransitionIn();
	}

	private void OnCook(object sender)
	{
		if (_inventory.NumIngredientsInPot > 0)
		{
			_audioSource.Play();
			Debug.Log("Let's cook!");
			CookDialogBox.TransitionOut();
			IngredientsDialogBox.TransitionOut();
			Kitchen.Cook(_inventory.IngredientsInPot);
			_inventory.NewPot();
			IsOpen = false;
		}
		else
		{
			Debug.Log("No ingredients to cook with");
			((Button)sender).DontSwallowCallback();
		}
	}

	private void OnPotReset(object sender)
	{
		_inventory.NewPot();
	}

	private void OnIngredientAdd(object sender)
	{
		GameDataHandler data = ((Button)sender).GetComponent<GameDataHandler>();
		_inventory.AddIngredientToPot((Ingredient)data.GetData());
	}

	public void Hide()
	{
		IsOpen = false;
		CookDialogBox.TransitionOut();
		IngredientsDialogBox.TransitionOut();
		CookDialogBox.SetOnTransitionComplete((x) =>
		{
			Kitchen.ActivateButton();
		});
	}

	void CloseDialog(object sender)
	{
		Hide();
	}
}
