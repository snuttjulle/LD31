using UnityEngine;
using System.Collections;

public class Burner : MonoBehaviour
{
	private Animator _animator;

	void Start()
	{
		_animator = this.GetComponent<Animator>();
	}

	void Update()
	{

	}

	public void TurnOn()
	{
		_animator.SetTrigger("on");
	}

	public void TurnOff()
	{
		_animator.SetTrigger("off");
	}
}
