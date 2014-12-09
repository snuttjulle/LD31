using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Text;
using System;
using System.Collections.Generic;

public class Menu : MonoBehaviour
{
	public Text[] DishTexts;

	private string _defaultStringFormat;

	private Button _button;
	private Action<object> _onCloseCallback;

	private List<Food> _dishSelection;
	public List<Food> GetDishSelection { get { return _dishSelection; } }

	void Awake()
	{
		_button = GetComponent<Button>();
		_button.SetTriggerCallback(OnClose);
		_defaultStringFormat = DishTexts[0].text;

		foreach (Text text in DishTexts)
			text.text = "";
	}

	public void SetMenu(List<Food> dishes, Kitchen kitchen)
	{
		_dishSelection = dishes;
		if (dishes.Count > 4)
		{
			_dishSelection = new List<Food>();

			for (int i = 0; i < 4; i++)
			{
				bool unique = false;
				while (!unique)
				{
					int rnd = RandomUtils.GetRandom.Next(0, dishes.Count - 1);
					if(!_dishSelection.Contains(dishes[rnd]) || _dishSelection.Count >= 4)
					{
						_dishSelection.Add(dishes[rnd]);
						unique = true;
					}
				}
			}

			Debug.Log("Too many dishes, removing randomly");
		}

		for (int i = 0; i < _dishSelection.Count; i++)
		{
			StringBuilder sb = new StringBuilder();
			foreach (Ingredient ing in _dishSelection[i].Ingredients)
			{
				sb.Append(ing.Name + Environment.NewLine);
			}

			DishTexts[i].text = string.Format(_defaultStringFormat, _dishSelection[i].name, sb.ToString());
		}
		kitchen.Inventory = new IngredientCollections(_dishSelection);
	}

	private void OnClose(object sender)
	{
		if (_onCloseCallback != null)
		{
			GetComponent<Animator>().SetTrigger("remove");

			_onCloseCallback(this);

			DestroyAfterTime des = gameObject.AddComponent<DestroyAfterTime>();
			des.Time = 4;
		}
	}

	public void SetPressCallback(Action<object> callback)
	{
		_onCloseCallback = callback;
	}
}
