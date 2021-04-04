using System.Collections;
using UnityEngine;

    public static class ProbabilityUtility
    {
    public static int GenerateProbalityInt ()
    {
        return Random.Range(0, 101);
    }

    public static long GenerateProbalityLong()
    {
        return Random.Range(0, 101);
    }

    public static double GenerateProbalityDouble()
    {
        return Random.Range(0.0f, 101.0f);
    }

    public static float GenerateProbalityFloat()
    {
        return Random.Range(0.0f, 101.0f);
    }


}