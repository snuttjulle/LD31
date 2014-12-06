using UnityEngine;
using System.Collections;

public class MathUtils
{
	public static float Berp(float start, float end, float time)
	{
		time = Mathf.Clamp01(time);
		time = (Mathf.Sin(time * Mathf.PI * (0.2f + 2.5f * time * time * time)) * Mathf.Pow(1f - time, 2.2f) + time) * (1f + (1.2f * (1f - time)));
		return start + (end - start) * time;
	}
}
