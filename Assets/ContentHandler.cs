using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]
public class ContentHandler : MonoBehaviour
{
	Animator _animator;

	void Start()
	{
		_animator = GetComponent<Animator>();
	}
}
