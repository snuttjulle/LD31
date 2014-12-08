using UnityEngine;
using System.Collections;
using UnityEditor;

public class Ingredient : ScriptableObject
{
	public string Name;

	public override bool Equals(object o)
	{
		if (o is Ingredient)
		{
			Ingredient other = (Ingredient)o;
			if (other.Name == this.Name)
				return true;
		}

		return false;
	}

	[MenuItem("Assets/Create/Ingredient")]
	public static void CreateAsset()
	{
		ScriptableObjectUtility.CreateAsset<Ingredient>();
	}
}
