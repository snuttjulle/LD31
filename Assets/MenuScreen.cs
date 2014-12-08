using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MenuScreen : MonoBehaviour
{
	public DayScreen DayScreen;
	public Button Play;
	public Button How;
	public Text PlayText;
	public Text HowText;
	public Button RulesDialogBox;
	public Text RulesText;

	void Awake()
	{
		ActivateButtons();
	}

	public void ActivateButtons()
	{
		Play.SetTriggerCallback(OnPlayPress);
		How.SetTriggerCallback(OnHowPress);
	}

	private void OnPlayPress(object sender)
	{
		gameObject.SetActive(false);
		DayScreen.ScreenState = DayScreenState.InSession;
		DayScreen.UpdateText();
		DayScreen.gameObject.SetActive(true);
	}

	private void OnHowPress(object sender)
	{
		PlayText.gameObject.SetActive(false);
		HowText.gameObject.SetActive(false);

		RulesText.gameObject.SetActive(true);
		RulesDialogBox.gameObject.SetActive(true);
		RulesDialogBox.SetTriggerCallback(OnHowClose);
	}

	private void OnHowClose(object sender)
	{
		PlayText.gameObject.SetActive(true);
		HowText.gameObject.SetActive(true);

		RulesText.gameObject.SetActive(false);
		RulesDialogBox.gameObject.SetActive(false);

		Play.SetTriggerCallback(OnPlayPress);
		How.SetTriggerCallback(OnHowPress);
	}
}
