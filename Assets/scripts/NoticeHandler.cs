using UnityEngine;
using System.Collections;

public class NoticeHandler : MonoBehaviour
{
	public Animator NoticeGraphics;
	public GameObject DialogBoxPrefab;

	void Start()
	{
		Button button = GetComponent<Button>();
		button.SetTriggerCallback(OnPress);
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
		box.SetWidth(50);
		box.SetHeight(20);
		box.TransitionIn();
	}
}
