using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class RandomUtils : MonoBehaviour
{
	private static System.Random _rnd = null;
	public static System.Random GetRandom { get { if (_rnd == null) _rnd = new System.Random(); return _rnd; } }

	public static IList Shuffle(IList list)
	{
		System.Random rng = RandomUtils.GetRandom;
		int n = list.Count;
		while (n > 1)
		{
			n--;
			int k = rng.Next(n + 1);
			var value = list[k];
			list[k] = list[n];
			list[n] = value;
		}

		return list;
	}
}
