using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class GameController : MonoBehaviour
{
	public AudioClip CritiqueSound;

	public Chef Chef;
	public CookingDevice[] CookingDevices;
	public Burner Burner;
	public Fumes Fumes;
	public OverlayHandler OverlayHandler;
	public ProgressBar ProgressBarPrefab;
	public List<Table> Tables;
	public Kitchen Kitchen;
	public GameObject CoinPrefab;
	public GameObject AngryFacePrefab;
	public DayScreen DayScreen;
	public Menu MenuPrefab;

	public Score Score { get; private set; }

	AnimationStateMachine _animationStateMachine;
	private AudioSource _audioSource;
	private float _dayLength = 0.0f;
	private float _timer = 0.0f;
	private bool _runDay = false;
	private int _critiqueLimit = 0;
	private int _critiquesGottenToday;
	private int _activeTables;

	private uint _day = 0;
	public uint Day { get { return _day; } }

	void Start()
	{
		Screen.fullScreen = false;
	}

	void Awake()
	{
		Score = new Score();
		_animationStateMachine = new AnimationStateMachine(this);
		_audioSource = GetComponent<AudioSource>();
	}

	void Update()
	{
		//if (Input.GetKeyUp(KeyCode.W))
		//	_animationStateMachine.SetAnimationState(AnimationStates.Cooking);
		//else if (Input.GetKeyUp(KeyCode.Q))
		//	_animationStateMachine.SetAnimationState(AnimationStates.NotCooking);
		//else if (Input.GetKeyUp(KeyCode.D))
		//	OverlayHandler.Show();
		//else if (Input.GetKeyUp(KeyCode.F))
		//	OverlayHandler.Hide();
		//else if (Input.GetKeyUp(KeyCode.T))
		//	Chef.GiveThumbsUp();
		//else if (Input.GetKeyUp(KeyCode.H))
		//	StartDay();

		if (!_runDay)
			return;

		if (_critiquesGottenToday > _critiqueLimit)
		{
			Debug.Log("Game over!");
			_runDay = false;
			DayScreen.gameObject.SetActive(true);
			DayScreen.ScreenState = DayScreenState.End;
			DayScreen.UpdateText();
			_day = 0;
			Score = new Score();
			_timer = 0;
			_activeTables = 0;
			UnityEngine.Object.Destroy(GameObject.FindGameObjectWithTag("Draggable"));
		}

		_timer += Time.deltaTime;

		int tablesLeft = 0;
		foreach (Table table in Tables)
		{
			table.UpdateTime(_timer);
			if (table.HasLeft)
				tablesLeft++;
		}

		if (_timer > _dayLength || tablesLeft >= _activeTables)
		{
			Debug.Log("The day is over!");
			_runDay = false;
			_day++;
			DayScreen.gameObject.SetActive(true);
			DayScreen.ScreenState = DayScreenState.InSession;
			DayScreen.UpdateText();
		}
	}

	public Level SetupLevel(int level)
	{
		_day = (uint)level;
		DayScreen.UpdateText();
		Level data = Kitchen.LoadLevelData(level);
		_activeTables = data.ActiveTables.Count;
		_critiqueLimit = data.CritiqueLimit;
		_critiquesGottenToday = 0;
		_dayLength = 0; //reset day

		//disable hitboxes for tables
		foreach (Table table in Tables)
		{
			table.GetComponent<BoxCollider2D>().enabled = false;
		}

		//randomize table
		for (int i = 0; i < data.ActiveTables.Count; i++)
		{
			//Tables[i].GetComponent<BoxCollider2D>().enabled = true;
			Tables[i].Requests = new FoodRequest(data.ActiveTables[i], data.Dishes);

			//setup timeline
			Queue<TableEvent> events = new Queue<TableEvent>();

			float timestamp = 0.0f;
			int moment;

			//nothing
			moment = RandomUtils.GetRandom.Next(data.TimelineSettings.MinNothingTime, data.TimelineSettings.MaxNothingTime);
			timestamp += moment;
			events.Enqueue(new TableEvent(timestamp, TableEventType.Order));

			//order
			moment = RandomUtils.GetRandom.Next(data.TimelineSettings.MinOrderTime, data.TimelineSettings.MaxOrderTime);
			timestamp += moment;
			events.Enqueue(new TableEvent(timestamp, TableEventType.HasOrderedYet));

			//order delay
			moment = RandomUtils.GetRandom.Next(data.TimelineSettings.MinOrderTimeDelay, data.TimelineSettings.MinOrderTimeDelay);
			timestamp += moment;
			events.Enqueue(new TableEvent(timestamp, TableEventType.HasFoodYet));

			//food
			moment = RandomUtils.GetRandom.Next(data.TimelineSettings.MinPrepareFoodTime, data.TimelineSettings.MaxPrepareFoodTime);
			timestamp += moment;
			events.Enqueue(new TableEvent(timestamp, TableEventType.Pay));

			//pay
			moment = RandomUtils.GetRandom.Next(data.TimelineSettings.MinWaitToLeaveTime, data.TimelineSettings.MaxWaitToLeaveTime);
			timestamp += moment;
			events.Enqueue(new TableEvent(timestamp, TableEventType.Leave));

			//leave

			Tables[i].InitTable(new FoodRequest(data.ActiveTables[i], data.Dishes), events);

			if (timestamp > _dayLength)
				_dayLength = timestamp + 3; //delay of 3 seconds
		}

		return data;
	}

	public void StartProgressBar(float time, Vector3 position, Action<object> callback)
	{
		var progressBar = (ProgressBar)UnityEngine.Object.Instantiate(ProgressBarPrefab, position, new Quaternion());
		Vector3 pos = progressBar.transform.position;
		pos.z = -15;
		progressBar.transform.position = pos;

		progressBar.ProgressTime = time;
		progressBar.SetCallback(callback);
		progressBar.Start();
	}

	public void StartDay()
	{
		uint day = _day;
		if (day > Kitchen.GetTotalLevels() - 1)
			day = Kitchen.GetTotalLevels() - 1;

		Level data = SetupLevel((int)day);
		Menu menu = (Menu)UnityEngine.Object.Instantiate(MenuPrefab, new Vector3(0, 0, 0), new Quaternion());
		menu.SetMenu(data.Dishes, Kitchen);
		menu.SetPressCallback(OnMenuClose);

		//Debug.Log(String.Format("Day {0} has started!", _day));
		//_timer = 0.0f;
		//_runDay = true;
	}

	public void Cook()
	{
		_animationStateMachine.SetAnimationState(AnimationStates.Cooking);
	}

	public void UnCook()
	{
		_animationStateMachine.SetAnimationState(AnimationStates.NotCooking);
	}

	public void GiveCritique(Table table, uint amount, bool playSound = true)
	{
		Score.Critiques += amount;
		GameObject obj = (GameObject)UnityEngine.Object.Instantiate(AngryFacePrefab, table.transform.position, new Quaternion());
		DayScreen.UpdateText();
		_critiquesGottenToday++;

		if (playSound)
		{
			_audioSource.clip = CritiqueSound;
			_audioSource.Play();
			Debug.Log("sound!");
		}

		Debug.Log("Critiques: " + Score.Critiques);
	}

	public void Pay(Table table, uint amount)
	{
		Score.Money += amount;
		UnityEngine.Object.Instantiate(CoinPrefab, table.transform.position, new Quaternion());
		DayScreen.UpdateText();
		Debug.Log("Money: " + Score.Money);
		Chef.GiveThumbsUp();
	}

	public void GiveThumbsUp()
	{
		Chef.GiveThumbsUp();
	}

	public void OnMenuClose(object sender)
	{
		//StartDay();
		Kitchen.ActivateButton();
		Debug.Log(String.Format("Day {0} has started!", _day));
		_timer = 0.0f;
		_runDay = true;
	}
}
