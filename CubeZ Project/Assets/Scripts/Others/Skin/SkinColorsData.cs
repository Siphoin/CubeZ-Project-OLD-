using UnityEngine;

[System.Serializable]
public class SkinColorsData
{
    [Header("Цвета")]
    public Color[] skinsColors = new Color[]
    {
        Color.white
    };
    public SkinColorsData ()
    {
        skinsColors = new Color[]
    {
        Color.white
    };
    }

    public SkinColorsData (SkinColorsData copyClass)
    {
        copyClass.CopyAll(this);
    }
}