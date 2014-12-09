using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public enum NoticeType { OrderFood, Pay }

public class NoticeHandler : MonoBehaviour
{
	public PotDialogBox PotDialogBox;
	public Animator NoticeGraphics;
	public FoodOrderNotice OrderBoxPrefab;
	public NoticeType NoticeType;

	private Button _button;
	private FoodRequest _requests;

	private Action<object> _onPressCallback;

	void Start()
	{
		_button = GetComponent<Button>();
		_button.SetTriggerCallback(OnPress);
	}

	void Update()
	{

	}

	void OnPress(object sender)
	{
		if (PotDialogBox.IsOpen)
			return;

		Debug.Log("Press:" + sender);

		if (_onPressCallback != null)
		{
			_onPressCallback(this);
			_onPressCallback = null;
		}

		if (NoticeType == NoticeType.OrderFood)
		{
			FoodOrderNotice spawn = (FoodOrderNotice)Instantiate(OrderBoxPrefab, new Vector3(0, 0, 0), new Quaternion());
			spawn.SetOrder(_requests);

			DialogBox box = spawn.GetComponent<DialogBox>();
			box.SetWidth(50);
			box.SetHeight(30);
			box.CloseOnFocusTap = true;
			box.TransitionIn();
			box.SetOnTransitionComplete(OnDialogBoxClose);
		}
		else if (NoticeType == NoticeType.Pay)
		{
			//PAY!
		}
	}

	void OnDialogBoxClose(object sender)
	{
		ForceClose();
		((DialogBox)sender).gameObject.AddComponent<DestroyAfterTime>();
		//_button.SetTriggerCallback(OnPress);
	}

	public void ForceClose()
	{
		NoticeGraphics.SetTrigger("remove");
		var destroy = gameObject.AddComponent<DestroyAfterTime>();
		destroy.ObjectToDestroy = gameObject;
	}

	public void SetupRequestData(FoodRequest requests)
	{
		_requests = requests;
	}

	public void SetOnNoticePressCallback(Action<object> callback)
	{
		_onPressCallback = callback;
	}
}
