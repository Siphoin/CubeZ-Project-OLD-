using UnityEngine;

public static class ColorExtensions
    {
    public static SerializeColor Serialize (this Color color)
    {
        return new SerializeColor(color);
    }

    public static Color Deserialize(this SerializeColor color)
    {
        return new Color(color.r, color.g, color.b, color.a);
    }
}