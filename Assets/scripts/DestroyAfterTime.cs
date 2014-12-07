using UnityEngine;
using System.Collections;

public class DestroyAfterTime : MonoBehaviour
{
	public GameObject ObjectToDestroy;
	public float Time = 2;

	private float _timer;

	void Awake()
	{
		_timer = 0.0f;
	}

	void Update()
	{
		_timer += UnityEngine.Time.deltaTime;

		if (_timer > Time)
		{
			if (ObjectToDestroy != null)
				Object.Destroy(ObjectToDestroy);
			else
				Object.Destroy(gameObject);
		}
	}
}
