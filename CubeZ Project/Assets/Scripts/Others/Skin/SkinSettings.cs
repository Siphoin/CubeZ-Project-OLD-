using UnityEngine;
[CreateAssetMenu(menuName = "Customize/New Skin Colors Variants", order = 0)]
public class SkinSettings : ScriptableObject
    {
    [Header("Набор цветов")]
    public SkinColorsData data = new SkinColorsData();

    public Color GetRandomSkinColor ()
    {
       if (data.skinsColors.Length == 0)
        {
            throw new SkinSettingsException("colors variants length as 0");
        }
        return data.skinsColors[Random.Range(0, data.skinsColors.Length)];
    }
    }