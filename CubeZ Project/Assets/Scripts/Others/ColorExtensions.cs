using UnityEngine;

public static class ColorExtensions
    {
    public static SerializeColor Serialize (this Color color)
    {
        return new SerializeColor(color);
    }
    }