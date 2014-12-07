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
		_deliveredFood = new List<bool>();
		GetComponent<TableTimeline>().SetEventTriggerCallback(OnTableEventTrigger);
	}

	public void InitTable(FoodRequest request, Queue<TableEvent> events)
	{
		SetupFoodRequests(request);
		TableTimeline timeline = GetComponent<TableTimeline>();
		timeline.TableEvents = events;
		timeline.Reset();
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
						//1- point
					}
				}
				break;
			case TableEventType.HasFoodYet:
				if (!FoodFullyDelivered)
				{
					Debug.Log("Table hasn't gotten any food");
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
					//-1 point
				}
				break;

			case TableEventType.Leave:
				if (!_hasPayed)
					Debug.Log("Didn't paying");
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
		GetComponent<BoxCollider2D>().enabled = true;
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

		if (type == NoticeType.OrderFood)
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
