using UnityEngine;

public static class Vector3Extensions
{
    public static Bounds ToBounds(this Vector3 vector)
    {
        return new Bounds(vector, Vector3.one);
    }

    public static Bounds ToBounds(this Vector3 vector, Vector3 size)
    {
        return new Bounds(vector, size);
    }
}