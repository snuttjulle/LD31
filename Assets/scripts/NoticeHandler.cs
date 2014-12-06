using UnityEngine;
using System.Collections;

public class NoticeHandler : MonoBehaviour
{
	public Animator NoticeGraphics;

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
	}
}
