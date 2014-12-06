using UnityEngine;
using System.Collections;

public class OverlayHandler : MonoBehaviour
{
	public OverlayText OverlayText;
	public SpriteRenderer BlackScreen;


	const float duration_time = 2;

	private TextFlash _flash;
	private TransitionState _currentState;
	float _time = 0;

	void Start()
	{
		_flash = OverlayText.GetComponent<TextFlash>();
	}

	void Update()
	{
		if (Input.GetKeyUp(KeyCode.P)) {
			_flash.Reset();
		}

		if (_currentState == TransitionState.None)
			return;
		else if (OverlayText.gameObject.activeInHierarchy && _currentState == TransitionState.Out)
			return;

		float from = 1.0f; float to = 1.0f;
		_time += Time.deltaTime * duration_time;

		if (_currentState == TransitionState.In)
			from = 0.0f;
		else if (_currentState == TransitionState.Out)
			to = 0.0f;

		float value = MathUtils.Sinerp(from, to, _time);
		BlackScreen.color = new Color(BlackScreen.color.r, BlackScreen.color.g, BlackScreen.color.b, value);

		if (_time > 1.0f && _currentState == TransitionState.Out)
			gameObject.SetActive(false);
		else if (_time > 1.0f && _currentState == TransitionState.In)
			OverlayText.Show();

		if (_time > 1.0f)
			_currentState = TransitionState.None;
	}

	public void Hide()
	{
		OverlayText.Hide();
		_time = 0.0f;
		Color color = BlackScreen.color;
		color.a = 1.0f;
		_currentState = TransitionState.Out;
	}

	public void Show()
	{
		_time = 0.0f;
		Color color = BlackScreen.color;
		color.a = 0.0f;
		_currentState = TransitionState.In;
		gameObject.SetActive(true);
	}
}
