using UnityEngine;
using System.Collections;
using System;

public class DialogBox : MonoBehaviour
{
	public GameObject[] TopRow;
	public GameObject[] MiddleRow;
	public GameObject[] BottomRow;
	public GameObject Arrow;

	private Vector3[,] _defaultSizes;
	private Vector3 _defaultArrowSize;

	const int default_size = 10;

	void Awake()
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

		SetWidth(50);
		SetHeight(30);
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
