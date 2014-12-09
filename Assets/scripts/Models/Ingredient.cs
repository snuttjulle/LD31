using UnityEngine;
using System.Collections;
#if UNITY_EDITOR
using UnityEditor;
#endif

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

#if UNITY_EDITOR
	[MenuItem("Assets/Create/Ingredient")]
	public static void CreateAsset()
	{
		ScriptableObjectUtility.CreateAsset<Ingredient>();
	}
#endif
}
