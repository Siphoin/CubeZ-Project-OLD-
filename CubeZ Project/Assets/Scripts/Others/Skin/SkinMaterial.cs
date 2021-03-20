using UnityEngine;
public class SkinMaterial : MonoBehaviour
{
    [SerializeField] SkinType skinType = SkinType.Clothe;

    protected const string PATH_SETTINGS_COLORS_CLOTHE = "Character/Customize/clotheColorsVariants";
    protected const string PATH_SETTINGS_COLORS_HAIR = "Character/Customize/hairColorsVariants";

    private SkinnedMeshRenderer rendererMaterial;

    public SkinType SkinType { get => skinType; }

    // Use this for initialization
    void Start()
    {
        Ini();
    }

    protected void Ini()
    {
        if (!TryGetComponent(out rendererMaterial))
        {
            throw new SkinMaterialException("not found component Renderer");
        }
        RandomizeColorMaterial();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public virtual void RandomizeColorMaterial()
    {
        string path = skinType == SkinType.Clothe ? PATH_SETTINGS_COLORS_CLOTHE : PATH_SETTINGS_COLORS_HAIR;
        SkinSettings skinSettings = Resources.Load<SkinSettings>(path);
        rendererMaterial.material.color = skinSettings.GetRandomSkinColor();

    }
}