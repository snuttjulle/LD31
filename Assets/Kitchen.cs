using UnityEngine;
using System.Collections;

public class Kitchen : MonoBehaviour
{
	public PotDialogBox PotDialogBox;

	private Button _button;

	void Start()
	{
		_button = GetComponent<Button>();
		ActivateButton();
	}

	private void OnPress(object sender)
	{
		PotDialogBox.Show();
	}

	public void ActivateButton()
	{
		_button.SetTriggerCallback(OnPress);
	}
}
