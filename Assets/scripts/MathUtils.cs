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

	public static float Hermite(float start, float end, float time)
	{
		return Lerp(start, end, time * time * (3.0f - 2.0f * time));
	}

	public static float Sinerp(float start, float end, float time)
	{
		return Lerp(start, end, Mathf.Sin(time * Mathf.PI * 0.5f));
	}

	public static float Coserp(float start, float end, float time)
	{
		return Lerp(start, end, 1.0f - Mathf.Cos(time * Mathf.PI * 0.5f));
	}

	public static float Lerp(float start, float end, float time)
	{
		return (1 - time) * start + time * end;
	}
}
