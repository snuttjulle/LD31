using UnityEngine;
using System.Collections;
using System;

public enum TransitionState { None, In, Out };

public class DialogBox : MonoBehaviour
{
	public GameObject[] TopRow;
	public GameObject[] MiddleRow;
	public GameObject[] BottomRow;
	public GameObject Arrow;

	private bool _closeOnFocusTap = false;
	public bool CloseOnFocusTap
	{
		get
		{
			return _closeOnFocusTap;
		}
		set
		{
			if (value)
			{
				Button button = GetComponent<Button>();
				button.SetTriggerCallback(OnPress);
				button.PressAnywhere = true;
				_closeOnFocusTap = true;
			}
		}
	}

	private Vector3[,] _defaultSizes = null;
	private Vector3 _defaultArrowSize;

	private Animator _animator;

	private BoxCollider2D _collider;

	private Action<object> _onTransitionComplete;

	const int default_size = 10;
	const float transition_time = 2.0f;

	private TransitionState _currentState;
	public TransitionState CurrentState { get { return _currentState; } }

	float _time = 0.0f;

	private float _setWidth = default_size * 3;
	private float _setHeight = default_size * 3;

	void Awake()
	{
		Hide();
		Init();

		_collider = GetComponent<BoxCollider2D>();
		_animator = GetComponent<Animator>();
		CalculateColliderSize();
	}

	private void OnPress(object sender)
	{
		TransitionOut();
	}

	private void CalculateColliderSize()
	{
		_collider.size = new Vector2(_setWidth, _setHeight);
	}

	private void Hide()
	{
		this.transform.position = new Vector3(transform.position.x, transform.position.y, 100); //the uglies of fixes, getting desperate here
	}

	private void Show()
	{
		this.transform.position = new Vector3(transform.position.x, transform.position.y, -30); //the uglies of fixes, getting desperate here
	}

	private void Init()
	{
		int rows = 3; int columns = 3;
		_defaultSizes = new Vector3[rows, columns];

		for (int i = 0; i < rows; i++)
			_defaultSizes[0, i] = TopRow[i].transform.localPosition;

		for (int i = 0; i < rows; i++)
			_defaultSizes[1, i] = MiddleRow[i].transform.localPosition;

		for (int i = 0; i < rows; i++)
			_defaultSizes[2, i] = BottomRow[i].transform.localPosition;

		_defaultArrowSize = Arrow.transform.localPosition;
	}

	public void TransitionIn()
	{
		transform.localScale = new Vector3(transform.localScale.x, 0, transform.localScale.z);
		_time = 0.0f;
		_currentState = TransitionState.In;
		Show();
		_animator.SetTrigger("add");
	}

	public void TransitionOut()
	{
		transform.localScale = new Vector3(transform.localScale.x, 1, transform.localScale.z);
		_time = 0.0f;
		_currentState = TransitionState.Out;
		_animator.SetTrigger("remove");
	}

	void Update()
	{
		//if (_currentState == TransitionState.None)
		//	return;

		//float from = 1.0f; float to = 1.0f;
		//_time += Time.deltaTime * transition_time;

		//if (_currentState == TransitionState.In)
		//	from = 0.0f;
		//else if (_currentState == TransitionState.Out)
		//	to = 0.0f;

		//float value = MathUtils.Berp(from, to, _time);
		//transform.localScale = new Vector3(transform.localScale.x, value, transform.localScale.z);

		//if (_time > 1.0f && _currentState == TransitionState.Out)
		//	Hide();

		//if (_time > 1.0f)
		//{
		//	_currentState = TransitionState.None;

		//	
		//}

		if (_animator.GetCurrentAnimatorStateInfo(0).IsName("content_holder_disappear") && _currentState == TransitionState.Out)
		{
			if (_onTransitionComplete != null)
			{
				_onTransitionComplete(this);
				_onTransitionComplete = null;
			}
		}
	}

	public void SetWidth(float width)
	{
		if (width < 10)
			throw new Exception("Too width");

		_setWidth = width * 2 + default_size;
		CalculateColliderSize();

		float factor = width / (float)default_size;
		float scaleFactor = (width / (default_size / 2)) - 1;

		for (int i = 0; i < TopRow.Length; i++)
			TopRow[i].transform.localPosition = new Vector3(_defaultSizes[0, i].x * factor, TopRow[i].transform.localPosition.y, TopRow[i].transform.localPosition.z);

		TopRow[1].transform.localScale = new Vector3(scaleFactor, TopRow[1].transform.localScale.y, TopRow[1].transform.localScale.z);

		for (int i = 0; i < TopRow.Length; i++)
			MiddleRow[i].transform.localPosition = new Vector3(_defaultSizes[1, i].x * factor, MiddleRow[i].transform.localPosition.y, MiddleRow[i].transform.localPosition.z);

		MiddleRow[1].transform.localScale = new Vector3(scaleFactor, MiddleRow[1].transform.localScale.y, MiddleRow[1].transform.localScale.z);

		for (int i = 0; i < BottomRow.Length; i++)
			BottomRow[i].transform.localPosition = new Vector3(_defaultSizes[2, i].x * factor, BottomRow[i].transform.localPosition.y, BottomRow[i].transform.localPosition.z);

		BottomRow[1].transform.localScale = new Vector3(scaleFactor, BottomRow[1].transform.localScale.y, BottomRow[1].transform.localScale.z);
	}

	public void SetHeight(float height)
	{
		if (height < 10)
			throw new Exception("Too width");

		_setHeight = height * 2 + default_size;
		CalculateColliderSize();

		float factor = height / (float)default_size;
		float scaleFactor = (height / (default_size / 2)) - 1;
		scaleFactor += 0.2f; //stupid fix to avoid seeing through the box in transitions

		for (int i = 0; i < TopRow.Length; i++)
			TopRow[i].transform.localPosition = new Vector3(TopRow[i].transform.localPosition.x, _defaultSizes[0, i].y * factor, TopRow[i].transform.localPosition.z);

		for (int i = 0; i < TopRow.Length; i++)
			MiddleRow[i].transform.localPosition = new Vector3(MiddleRow[i].transform.localPosition.x, _defaultSizes[1, i].y * factor, MiddleRow[i].transform.localPosition.z);

		for (int i = 0; i < MiddleRow.Length; i++)
			MiddleRow[i].transform.localScale = new Vector3(MiddleRow[i].transform.localScale.x, scaleFactor, MiddleRow[i].transform.localScale.z);

		for (int i = 0; i < BottomRow.Length; i++)
			BottomRow[i].transform.localPosition = new Vector3(BottomRow[i].transform.localPosition.x, _defaultSizes[2, i].y * factor, BottomRow[i].transform.localPosition.z);
	}

	public void SetOnTransitionComplete(Action<object> callback)
	{
		_onTransitionComplete = callback;
	}
}
