using UnityEngine;
using System.Collections;

public class ContentHandler : MonoBehaviour
{


	public void CleanContent()
	{
		foreach (Transform child in transform)
		{
			Object.Destroy(child.gameObject);
		}
	}
}
