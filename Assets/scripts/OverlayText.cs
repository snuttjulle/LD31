using UnityEngine;
using System.Collections;

public class OverlayText : MonoBehaviour
{
	const float duration_time = 2;

	GUIText _text;
	TransitionState _currentState;
	float _time = 0;

	void Start()
	{
		_text = GetComponent<GUIText>();
	}

	void Update()
	{
		if (_currentState == TransitionState.None)
			return;

		float from = 1.0f; float to = 1.0f;
		_time += Time.deltaTime * duration_time;

		if (_currentState == TransitionState.In)
			from = 0.0f;
		else if (_currentState == TransitionState.Out)
			to = 0.0f;

		float value = MathUtils.Sinerp(from, to, _time);
		_text.color = new Color(_text.color.r, _text.color.g, _text.color.b, value);
		
		if (_time > 1.0f && _currentState == TransitionState.Out)
			gameObject.SetActive(false);

		if (_time > 1.0f)
			_currentState = TransitionState.None;
	}

	public void Hide()
	{
		_time = 0.0f;
		Color color = _text.color;
		color.a = 1.0f;
		_currentState = TransitionState.Out;
	}

	public void Show()
	{
		_time = 0.0f;
		Color color = _text.color;
		color.a = 0.0f;
		_currentState = TransitionState.In;
		gameObject.SetActive(true);
	}
}
