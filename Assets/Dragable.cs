using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider2D))]
public class Dragable : MonoBehaviour //TODO: TYPO!!!
{
	private Vector3 _startPosition;
	private Vector3 _dropPosition;
	private Vector3 _screenPoint;
	private Vector3 _offset;

	private const float return_time = 0.1f;
	private bool _pressing;
	private float _timer = 1.0f;

	void Awake()
	{
		_startPosition = transform.position;
		_startPosition.z = -30;
		_dropPosition = _startPosition;
	}

	void Update()
	{
		if (!_pressing && _timer < 1.0f)
		{
			_timer += Time.deltaTime / return_time;

			transform.position = Vector3.Lerp(_dropPosition, _startPosition, _timer);
		}
	}

	void OnMouseDown()
	{
		_timer = 0.0f;
		_pressing = true;
		_offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, _startPosition.z));
		_offset.z = _startPosition.z;
	}

	void OnMouseDrag()
	{
		Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, _startPosition.z);
		Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + _offset;
		curPosition.z = _startPosition.z;
		transform.position = curPosition;
	}

	void OnMouseUp()
	{
		Debug.Log("moyse up");
		Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, _startPosition.z);
		_dropPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + _offset;
		_dropPosition.z = _startPosition.z;
		_pressing = false;
		//raycast to see if hit

	}
}
