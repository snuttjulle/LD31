using UnityEngine;
using System.Collections;

public class Score
{
	public uint Money { get; set; }
	public uint Critiques { get; set; }

	public Score()
	{
		Money = 0;
		Critiques = 0;
	}
}
