using UnityEngine;
using System.Collections;
using System;

public class DialogBox : MonoBehaviour
{
	public GameObject[] TopRow;
	public GameObject[] MiddleRow;
	public GameObject[] BottomRow;
	public GameObject Arrow;

	private Vector3[,] _defaultSizes = null;
	private Vector3 _defaultArrowSize;

	const int default_size = 10;
	const float transition_time = 2.0f;

	private enum TransitionState { None, In, Out };

	TransitionState _currentState;
	float _time = 0.0f;

	void Awake()
	{
		Hide();
		Init();

		SetWidth(50);
		SetHeight(30);
	}

	private void Hide()
	{
		this.transform.position = new Vector3(transform.position.x, transform.position.y, 100); //the uglies of fixes, getting desperate here
	}

	private void Show()
	{
		this.transform.position = new Vector3(transform.position.x, transform.position.y, -10); //the uglies of fixes, getting desperate here
	}

	private void Init()
	{
		int rows = 3; int columns = 3;
		_defaultSizes = new Vector3[rows, columns];

		for (int i = 0; i < rows; i++)
			_defaultSizes[0, i] = TopRow[i].transform.position;

		for (int i = 0; i < rows; i++)
			_defaultSizes[1, i] = MiddleRow[i].transform.position;

		for (int i = 0; i < rows; i++)
			_defaultSizes[2, i] = BottomRow[i].transform.position;

		_defaultArrowSize = Arrow.transform.position;
	}

	public void TransitionIn()
	{
		transform.localScale = new Vector3(transform.localScale.x, 0, transform.localScale.z);
		_time = 0.0f;
		_currentState = TransitionState.In;
		Show();
	}

	public void TransitionOut()
	{
		transform.localScale = new Vector3(transform.localScale.x, 1, transform.localScale.z);
		_time = 0.0f;
		_currentState = TransitionState.Out;
	}

	void Update()
	{
		if (_currentState == TransitionState.None)
			return;

		float from = 1.0f; float to = 1.0f;
		_time += Time.deltaTime * transition_time;

		if (_currentState == TransitionState.In)
			from = 0.0f;
		else if (_currentState == TransitionState.Out)
			to = 0.0f;

		float value = MathUtils.Berp(from, to, _time);
		transform.localScale = new Vector3(transform.localScale.x, value, transform.localScale.z);

		if (_time > 1.0f && _currentState == TransitionState.Out)
			Hide();

		if (_time > 1.0f)
			_currentState = TransitionState.None;
	}

	public void SetWidth(float width)
	{
		if (width < 10)
			throw new Exception("Too width");

		float factor = width / (float)default_size;
		float scaleFactor = (width / (default_size / 2)) - 1;

		for (int i = 0; i < TopRow.Length; i++)
			TopRow[i].transform.position = new Vector3(_defaultSizes[0, i].x * factor, TopRow[i].transform.position.y, TopRow[i].transform.position.z);

		TopRow[1].transform.localScale = new Vector3(scaleFactor, TopRow[1].transform.localScale.y, TopRow[1].transform.localScale.z);

		for (int i = 0; i < TopRow.Length; i++)
			MiddleRow[i].transform.position = new Vector3(_defaultSizes[1, i].x * factor, MiddleRow[i].transform.position.y, MiddleRow[i].transform.position.z);

		MiddleRow[1].transform.localScale = new Vector3(scaleFactor, MiddleRow[1].transform.localScale.y, MiddleRow[1].transform.localScale.z);

		for (int i = 0; i < BottomRow.Length; i++)
			BottomRow[i].transform.position = new Vector3(_defaultSizes[2, i].x * factor, BottomRow[i].transform.position.y, BottomRow[i].transform.position.z);

		BottomRow[1].transform.localScale = new Vector3(scaleFactor, BottomRow[1].transform.localScale.y, BottomRow[1].transform.localScale.z);
	}

	public void SetHeight(float height)
	{
		if (height < 10)
			throw new Exception("Too width");

		float factor = height / (float)default_size;
		float scaleFactor = (height / (default_size / 2)) - 1;
		scaleFactor += 0.2f; //stupid fix to avoid seeing through the box in transitions

		for (int i = 0; i < TopRow.Length; i++)
			TopRow[i].transform.position = new Vector3(TopRow[i].transform.position.x, _defaultSizes[0, i].y * factor, TopRow[i].transform.position.z);

		for (int i = 0; i < TopRow.Length; i++)
			MiddleRow[i].transform.position = new Vector3(MiddleRow[i].transform.position.x, _defaultSizes[1, i].y * factor, MiddleRow[i].transform.position.z);

		for(int i = 0; i < MiddleRow.Length; i++)
			MiddleRow[i].transform.localScale = new Vector3(MiddleRow[i].transform.localScale.x, scaleFactor, MiddleRow[i].transform.localScale.z);

		for (int i = 0; i < BottomRow.Length; i++)
			BottomRow[i].transform.position = new Vector3(BottomRow[i].transform.position.x, _defaultSizes[2, i].y * factor, BottomRow[i].transform.position.z);
	}
}
