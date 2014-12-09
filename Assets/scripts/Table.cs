using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[RequireComponent(typeof(BoxCollider2D), typeof(TableTimeline))]
public class Table : MonoBehaviour
{
	public AudioClip YumSound;
	public AudioClip YuckSound;
	public AudioClip WaiterSound;
	public AudioClip PaySound;

	public GameObject[] Characters;

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
	public PotDialogBox PotDialogBox;

	private NoticeHandler _activeNoticeHandler;
	private List<bool> _deliveredFood;
	private bool _hasPayed;
	private bool _hasLeft;
	public bool HasLeft { get { return _hasLeft; } }

	private int _incorrectDelivery;

	private TableTimeline _timeline;

	private GameController _controller;
	private Action<object> _onLeaveCallback;

	private AudioSource _audioSource;

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
		_audioSource = GetComponent<AudioSource>();
	}

	public void InitTable(FoodRequest request, Queue<TableEvent> events)
	{
		SetupFoodRequests(request);

		foreach (var chara in Characters)
			chara.SetActive(false);

		for (int i = 0; i < _requests.RequestedFood.Count; i++)
			Characters[i].SetActive(true);

		_timeline.TableEvents = events;
		_timeline.Reset();
		_hasLeft = false;
		_hasPayed = false;
		_incorrectDelivery = 0;
	}

	public void SetupFoodRequests(FoodRequest requests)
	{
		_deliveredFood = new List<bool>();

		for (int i = 0; i < Requests.RequestedFood.Count; i++)
			_deliveredFood.Add(false);
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
				if (!_hasLeft)
				{
					if (!_hasPayed)
						Debug.Log("Didn't pay");
					Leave();
				}
				break;
		}
	}

	private void Leave()
	{
		_hasLeft = true;

		if (_activeNoticeHandler != null)
			_activeNoticeHandler.ForceClose();

		GetComponent<BoxCollider2D>().enabled = false;

		foreach (var chara in Characters)
			chara.SetActive(false);

		Debug.Log("The Table left");
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
			//PaySound.Play();
			_audioSource.clip = PaySound;
			_audioSource.Play();

			Leave();
		}
	}

	public void AlertOfRequest(NoticeType type)
	{
		if (_activeNoticeHandler != null)
			_activeNoticeHandler.ForceClose();

		Vector3 pos = transform.position + new Vector3(0, 20, 0);
		pos.z = -10;
		NoticeHandler notice = (NoticeHandler)UnityEngine.Object.Instantiate(NoticeHandlerPrefab, pos, new Quaternion());
		notice.SetupRequestData(Requests);
		notice.NoticeType = type;
		notice.PotDialogBox = PotDialogBox;

		notice.SetOnNoticePressCallback(OnNoticePress);

		_activeNoticeHandler = notice;

		//OrderSound.Play();
		_audioSource.clip = WaiterSound;
		_audioSource.Play();
		Debug.Log("sound!");
	}

	public void DeliverDish(Food dish)
	{
		Debug.Log(dish.Name + " was delivered to a table");

		for (int i = 0; i < Requests.RequestedFood.Count; i++)
		{
			if (Requests.RequestedFood[i].Name == dish.Name && !_deliveredFood[i])
			{
				_deliveredFood[i] = true;

				//ReceiveRightSound.Play();
				_audioSource.clip = YumSound;
				_audioSource.Play();
				Debug.Log("sound!");
				_controller.GiveThumbsUp();

				if (FoodFullyDelivered)
				{
					Debug.Log("Fully delivered!");
					GetComponent<BoxCollider2D>().enabled = false;
				}

				return;
			}
		}

		//ReceiveWrongSound.Play();
		_audioSource.clip = YuckSound;
		_audioSource.Play();
		//Debug.Log("sound!");

		_controller.GiveCritique(this, 1, false);
		Debug.Log("Delivered the wrong food");
		_incorrectDelivery++;
	}

	public void SetLeaveCallback(Action<object> callback)
	{
		_onLeaveCallback = callback;
	}
}
