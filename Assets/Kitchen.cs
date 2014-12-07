using UnityEngine;
using System.Collections;

public class Kitchen : MonoBehaviour
{
	public DialogBox DialogBoxPrefab;

	private Button _button;

	void Start()
	{
		_button = GetComponent<Button>();
		_button.SetTriggerCallback(OnPress);
	}

	private void OnPress(object sender)
	{
		//gameObject spawn = (gameObject)Instantiate(DialogBoxPrefab, 
	}
}
