using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;

public class Level : ScriptableObject
{
	public List<Food> Dishes;
	public List<int> ActiveTables; //list.count = amount of tables active, int is max number of people at each table
	public TimelineEvent TimelineSettings;
	public int CritiqueLimit;

	public IngredientCollections IngredientCollection
	{
		get
		{
			return new IngredientCollections(Dishes);
		}
	}

	[MenuItem("Assets/Create/Level")]
	public static void CreateAsset()
	{
		ScriptableObjectUtility.CreateAsset<Level>();
	}
}
