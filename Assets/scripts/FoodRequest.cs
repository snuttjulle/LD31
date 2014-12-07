using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

public class FoodRequest
{
	public List<Food> RequestedFood;

	public FoodRequest(int numberOfRequests, List<Food> allDishes)
	{
		RequestedFood = new List<Food>();
		for (int i = 0; i < numberOfRequests; i++)
		{
			int dish = RandomUtils.GetRandom.Next(0, allDishes.Count - 1);
			RequestedFood.Add(allDishes[dish]);			
		}
	}
}
