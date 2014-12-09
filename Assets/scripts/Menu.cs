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

	void Awake()
	{
		_button = GetComponent<Button>();
		_button.SetTriggerCallback(OnClose);
		_defaultStringFormat = DishTexts[0].text;

		foreach(Text text in DishTexts)
			text.text = "";
	}

	public void SetMenu(List<Food> dishes)
	{
		if (dishes.Count > 4)
		{
			Debug.Log("Too many dishes, removing randomly");
			for (int i = 0; i < dishes.Count - 4; i++)
				dishes.RemoveAt(RandomUtils.GetRandom.Next(0, dishes.Count - 1)); //TODO: test this
		}


		for (int i = 0; i < dishes.Count; i++)
		{
			StringBuilder sb = new StringBuilder();
			foreach (Ingredient ing in dishes[i].Ingredients)
			{
				sb.Append(ing.Name + Environment.NewLine);
			}

			DishTexts[i].text = string.Format(_defaultStringFormat, dishes[i].name, sb.ToString());
		}
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
