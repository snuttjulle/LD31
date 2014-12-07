using UnityEngine;
using System.Collections;

public class RandomUtils : MonoBehaviour
{
	private static System.Random _rnd = new System.Random();
	public static System.Random GetRandom { get { return _rnd; } }
}
