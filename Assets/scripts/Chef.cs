using UnityEngine;
using System.Collections;

public class Chef : MonoBehaviour
{
	private Animator _animator;

	void Start()
	{
		_animator = GetComponent<Animator>();
	}

	void Update()
	{

	}

	public void GiveThumbsUp()
	{
		_animator.SetTrigger("positive");
	}
}
