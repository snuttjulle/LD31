using UnityEngine;
using System.Collections;

public class GameDataHandler : MonoBehaviour
{
	private System.Type _type;
	private object _data;

	public object GetData()
	{
		return _data;
	}

	public void SetData(System.Type type, object data)
	{
		_data = data;
	}
}
