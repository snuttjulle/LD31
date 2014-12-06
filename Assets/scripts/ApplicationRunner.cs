using UnityEngine;
using System.Collections;

public class ApplicationRunner : MonoBehaviour
{
	void Start()
	{
		Object.DontDestroyOnLoad(this);
	}

	void Update()
	{

	}
}
