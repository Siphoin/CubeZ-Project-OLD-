using UnityEngine;

public class SkinBottonPartMaterial : SkinMaterial
    {
    [SerializeField] private SkinnedMeshRenderer firstPart;
    [SerializeField] private SkinnedMeshRenderer secondPart;
        // Use this for initialization
        void Start()
        {
        if (secondPart == null)
        {
            throw new SkinMaterialException("second part not seted");
        }
        if (firstPart == null)
        {
            throw new SkinMaterialException("second part not seted");
        }
        RandomizeColorMaterial();
        }
    public override void RandomizeColorMaterial()
    {
        string path = SkinType == SkinType.Clothe ? PATH_SETTINGS_COLORS_CLOTHE : PATH_SETTINGS_COLORS_HAIR;
        SkinSettings skinSettings = Resources.Load<SkinSettings>(path);
        Color selectedColor = skinSettings.GetRandomSkinColor();
        SetMaterialColorSkinnedRenderer(selectedColor, firstPart);
        SetMaterialColorSkinnedRenderer(selectedColor, secondPart);
    }

    private void SetMaterialColorSkinnedRenderer (Color color, SkinnedMeshRenderer renderer)
    {
        renderer.material.color = color;
    }
}