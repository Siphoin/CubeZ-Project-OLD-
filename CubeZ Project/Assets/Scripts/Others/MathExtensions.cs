using System;
using UnityEngine;

public struct MathfExtensions
{
    public static float LerpRound(float a, float b, float time, int roundValue)
    {
        float value = Mathf.Lerp(a, b, time);
        value = (float)Math.Round(value, roundValue);
        return value;
    }
}