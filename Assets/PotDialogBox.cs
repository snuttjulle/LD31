using UnityEngine;
using System.Collections;

public class PotDialogBox : MonoBehaviour
{
	DialogBox _dialogBox;
	ContentHandler _content;

	void Start()
	{
		_content = gameObject.transform.GetComponentInChildren<ContentHandler>();
		_dialogBox = gameObject.transform.GetComponentInChildren<DialogBox>();
		_dialogBox.SetHeight(70);
		_dialogBox.SetWidth(30);
		_dialogBox.TransitionIn();
		_dialogBox.CloseOnFocusTap = true;
	}

	void Update()
	{
	}
}
