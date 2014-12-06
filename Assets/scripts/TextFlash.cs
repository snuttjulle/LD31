using UnityEngine;
using System.Collections;

//assumes that the text is lit
public class TextFlash : MonoBehaviour
{
	public float FlashTime;
	public int Flashes;

	private bool _direction; //true -> 1, false -> 0
	private GUIText _text;
	private float _timer;
	private float _totalTimer;

	public bool IsDone { get; private set; }

	void Start()
	{
		IsDone = true;
		_text = GetComponent<GUIText>();
		_direction = false;
	}

	void Update()
	{
		if (IsDone)
			return;

		float from = 1.0f; float to = 1.0f;

		if (!_direction)
			to = 0.0f;
		else
			from = 0.0f;

		_timer += Time.deltaTime / FlashTime * FlashTime * 2;
		_totalTimer += Time.deltaTime / FlashTime;

		Color color = _text.color;
		color.a = MathUtils.Lerp(from, to, _timer);
		_text.color = color;

		if (_timer > 1.0f)
		{
			_direction = !_direction;
			_timer = 0.0f;
		}

		if (_totalTimer > 1.0f)
			IsDone = true;
	}

	public void Reset()
	{
		IsDone = false;
		_totalTimer = 0.0f;
		_timer = 0.0f;
	}
}
