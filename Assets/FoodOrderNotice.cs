using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Text;
using System;

public class FoodOrderNotice : MonoBehaviour
{
	public Text OrderText;

	private string _stringFormat;

	void Awake()
	{
		_stringFormat = OrderText.text;
	}

	public void SetOrder(FoodRequest request)
	{
		StringBuilder sb = new StringBuilder();

		foreach(Food food in request.RequestedFood)
			sb.Append(food.Name + Environment.NewLine);

		OrderText.text = string.Format(_stringFormat, sb.ToString());
	}
}
