using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public enum DayScreenState { InSession, End }

public class DayScreen : MonoBehaviour
{
	public GameController GameController;
	public Text DayText;
	public Text MoneyText;
	public Text CritiquesText;

	public Text StartText;
	public Text GameOverText;
	public Text GameOverInfoText;

	public DayScreenState ScreenState = DayScreenState.InSession;

	private Button _tap;

	private string _dayText;
	private string _moneyText;
	private string _critiquesText;

	void Awake()
	{
		_tap = GetComponent<Button>();
		_tap.MultiPress = true;
		_tap.SetTriggerCallback(OnTap);

		_dayText = DayText.text;
		_moneyText = MoneyText.text;
		_critiquesText = CritiquesText.text;

		UpdateText();
	}

	private void OnTap(object sender)
	{
		gameObject.SetActive(false);
		GameController.StartDay();
	}

	public void UpdateText()
	{
		if (_dayText == null)
			return;

		DayText.text = string.Format(_dayText, GameController.Day + 1);
		MoneyText.text = string.Format(_moneyText, GameController.Score.Money);
		CritiquesText.text = string.Format(_critiquesText, GameController.Score.Critiques);

		if (ScreenState == DayScreenState.InSession)
		{
			StartText.gameObject.SetActive(true);

			GameOverText.gameObject.SetActive(false);
			GameOverInfoText.gameObject.SetActive(false);
		}
		else if (ScreenState == DayScreenState.End)
		{
			StartText.gameObject.SetActive(false);

			GameOverText.gameObject.SetActive(true);
			GameOverInfoText.gameObject.SetActive(true);
		}
	}
}
