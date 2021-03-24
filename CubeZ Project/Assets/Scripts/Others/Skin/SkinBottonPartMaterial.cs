using UnityEngine;

public class SkinBottonPartMaterial : SkinMaterial
    {
    [SerializeField] private Renderer firstPart;
    [SerializeField] private Renderer secondPart;
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
        string path = PATH_FOLBER_SETTINGS + SkinType.ToString().ToLower() + PREFIX_NAME_SETTINGS;
        SkinSettings skinSettings = Resources.Load<SkinSettings>(path);
        Color selectedColor = skinSettings.GetRandomSkinColor();
        SetMaterialColorSkinnedRenderer(selectedColor, firstPart);
        SetMaterialColorSkinnedRenderer(selectedColor, secondPart);
    }

    private void SetMaterialColorSkinnedRenderer (Color color, Renderer renderer)
    {
        renderer.material.color = color;
    }
}