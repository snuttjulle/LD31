using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(BoxCollider2D), typeof(TableTimeline))]
public class Table : MonoBehaviour
{
	private FoodRequest _requests;
	public FoodRequest Requests
	{
		get
		{
			return _requests;
		}

		set
		{
			_requests = value;
			SetupFoodRequests(_requests);
		}
	}
	public NoticeHandler NoticeHandlerPrefab;

	private NoticeHandler _activeNoticeHandler;
	private List<bool> _deliveredFood;
	private bool _hasPayed;

	private int _incorrectDelivery;

	private TableTimeline _timeline;

	private GameController _controller;

	private bool FoodFullyDelivered
	{
		get
		{
			bool isDelivered = true;
			foreach (bool value in _deliveredFood)
			{
				if (!value)
				{
					isDelivered = false;
					break;
				}
			}
			return isDelivered;
		}
	}

	void Awake()
	{
		_controller = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
		_deliveredFood = new List<bool>();
		_timeline = GetComponent<TableTimeline>();
		_timeline.SetEventTriggerCallback(OnTableEventTrigger);
	}

	public void InitTable(FoodRequest request, Queue<TableEvent> events)
	{
		SetupFoodRequests(request);
		_timeline.TableEvents = events;
		_timeline.Reset();
	}

	public void UpdateTime(float time)
	{
		_timeline.UpdateTime(time);
	}

	private void OnTableEventTrigger(TableEventType type)
	{
		switch (type)
		{
			case TableEventType.Order:
				AlertOfRequest(NoticeType.OrderFood);
				Debug.Log("Table wants to order");
				break;
			case TableEventType.HasOrderedYet:
				if (_activeNoticeHandler != null)
				{
					if (_activeNoticeHandler.NoticeType == NoticeType.OrderFood)
					{
						Debug.Log("Table hasn't gotten to order yet");
						_controller.GiveCritique(this, 1);
						//1- point
					}
				}
				break;
			case TableEventType.HasFoodYet:
				if (!FoodFullyDelivered)
				{
					Debug.Log("Table hasn't gotten any food");
					_controller.GiveCritique(this, 1);
					//-1 point
				}
				break;
			case TableEventType.Pay:
				if (_activeNoticeHandler != null)
				{
					if (_activeNoticeHandler.NoticeType == NoticeType.OrderFood)
						Leave();
				}
				else
				{
					Debug.Log("Table wants to pay");
					AlertOfRequest(NoticeType.Pay);
				}
				break;

			case TableEventType.HasYetToPay:
				if (!_hasPayed)
				{
					Debug.Log("Table has not gotten to pay yet");
					_controller.GiveCritique(this, 1);
					//-1 point
				}
				break;

			case TableEventType.Leave:
				if (!_hasPayed)
					Debug.Log("Didn't pay");
				Leave();
				break;
		}
	}

	private void Leave()
	{
		if (_activeNoticeHandler != null)
			_activeNoticeHandler.ForceClose();

		GetComponent<BoxCollider2D>().enabled = false;

		Debug.Log("The Table left");
	}

	public void SetupFoodRequests(FoodRequest requests)
	{
		_deliveredFood = new List<bool>();

		for (int i = 0; i < Requests.RequestedFood.Count; i++)
			_deliveredFood.Add(false);
	}

	private void OnNoticePress(object sender)
	{
		NoticeHandler handler = (NoticeHandler)sender;

		if (handler.NoticeType == NoticeType.OrderFood)
			GetComponent<BoxCollider2D>().enabled = true;
		else if (handler.NoticeType == NoticeType.Pay)
		{
			_hasPayed = true;
			_activeNoticeHandler.ForceClose();

			int receivedFood = 0;
			foreach (bool delivered in _deliveredFood)
			{
				if (delivered)
					receivedFood++;
			}

			_controller.Pay(this, (uint)(receivedFood * 200)); //TODO: get price depending on food?
			//TODO: sound play here

			Leave();
		}
	}

	public void AlertOfRequest(NoticeType type)
	{
		if (_activeNoticeHandler != null)
			_activeNoticeHandler.ForceClose();

		Vector3 pos = transform.position + new Vector3(0, 20, 0);
		pos.z = -10;
		NoticeHandler notice = (NoticeHandler)Object.Instantiate(NoticeHandlerPrefab, pos, new Quaternion());
		notice.SetupRequestData(Requests);
		notice.NoticeType = type;

		notice.SetOnNoticePressCallback(OnNoticePress);

		_activeNoticeHandler = notice;
	}

	public void DeliverDish(Food dish)
	{
		Debug.Log(dish.Name + " was delivered to a table");

		for (int i = 0; i < Requests.RequestedFood.Count; i++)
		{
			if (Requests.RequestedFood[i].Name == dish.Name && !_deliveredFood[i])
			{
				_deliveredFood[i] = true;

				if (FoodFullyDelivered)
				{
					Debug.Log("Fully delivered!");
					GetComponent<BoxCollider2D>().enabled = false;
				}

				return;
			}
		}

		Debug.Log("Delivered the wrong food");
		_incorrectDelivery++;
	}
}
