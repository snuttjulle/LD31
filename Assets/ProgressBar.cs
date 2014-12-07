using UnityEngine;
using System.Collections;
using System;

public class ProgressBar : MonoBehaviour
{
	const float default_time = 5;

	public GameObject Bar;
	public float ProgressTime = default_time;
	public bool DestroyOnCompletion = true;

	private Action<object> _callback;

	private bool _running = false;
	private float _timer = 0.0f;

	void Update()
	{
		if (!_running)
			return;

		_timer += Time.deltaTime / ProgressTime;

		Vector3 scale = Bar.transform.localScale;
		scale.x = MathUtils.Lerp(0.0f, 1.0f, _timer);
		Bar.transform.localScale = scale;

		if (_timer > 1.0f)
		{
			_running = false;

			if (_callback != null)
			{
				_callback(this);
				_callback = null;
			}

			if (DestroyOnCompletion)
				UnityEngine.Object.Destroy(gameObject);
		}
	}

	public void Start()
	{
		_running = true;
	}

	public void Reset()
	{
		_timer = 0;
	}

	public void SetCallback(Action<object> callback)
	{
		_callback = callback;
	}
}
