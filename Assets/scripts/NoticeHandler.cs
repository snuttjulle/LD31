using UnityEngine;
using System.Collections;

public class NoticeHandler : MonoBehaviour
{
	public Animator NoticeGraphics;
	public GameObject DialogBoxPrefab;

	private Button _button;

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
		Debug.Log("Press:" + sender);
		NoticeGraphics.SetTrigger("remove");

		GameObject spawn = (GameObject)Instantiate(DialogBoxPrefab, new Vector3(0, 0, 0), new Quaternion());
		DialogBox box = spawn.GetComponent<DialogBox>();
		box.SetWidth(70);
		box.SetHeight(10);
		box.CloseOnFocusTap = true;
		box.TransitionIn();
		box.SetOnTransitionComplete(OnDialogBoxClose);
	}

	void OnDialogBoxClose(object sender)
	{
		Debug.Log("Notice dialog closed");
		//_button.SetTriggerCallback(OnPress);
	}
}
