using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(BoxCollider2D))]
public class Button : MonoBehaviour
{
	public bool PressAnywhere { get; set; }

	Action<object> OnTriggerCallback;
	BoxCollider2D _collider;

	void Awake()
	{
		_collider = GetComponent<BoxCollider2D>();
	}

	void Update()
	{
		for (var i = 0; i < Input.touchCount; i++)
		{
			if (Input.GetTouch(i).phase == TouchPhase.Began)
			{
				RaycastHit2D hitInfo = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.GetTouch(i).position), Vector2.zero);
				if (hitInfo)
					OnPress(hitInfo);
				else if (PressAnywhere)
					OnPress(hitInfo);
			}
		}

		if (Input.GetMouseButtonDown(0))
		{
			Vector2 pos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
			RaycastHit2D hitInfo = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(pos), Vector2.zero);
			if (hitInfo)
				OnPress(hitInfo);
			else if(PressAnywhere)
				OnPress(hitInfo);
		}
	}

	void OnPress(RaycastHit2D hitInfo)
	{
		if (_collider.bounds.Contains(new Vector3(hitInfo.point.x, hitInfo.point.y, _collider.transform.position.z)) || PressAnywhere)
		{
			if (OnTriggerCallback != null)
			{
				OnTriggerCallback(this);
				OnTriggerCallback = null;
			}
		}
	}

	public void SetTriggerCallback(Action<object> callback)
	{
		OnTriggerCallback = callback;
	}
}
