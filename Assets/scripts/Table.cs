using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider2D))]
public class Table : MonoBehaviour
{
	void Update()
	{

	}

	public void AlertOfRequest()
	{
 		
	}

	public void DeliverDish(Food dish)
	{
		Debug.Log(dish.Name + " was delivered to a table");
	}
}
