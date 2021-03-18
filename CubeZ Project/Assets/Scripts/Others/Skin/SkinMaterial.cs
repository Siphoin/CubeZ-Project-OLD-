using System.Collections;
using UnityEngine;
[RequireComponent(typeof(Renderer))]
    public class SkinMaterial : MonoBehaviour
    {
    [SerializeField] SkinType skinType = SkinType.Clothe;

    private const string PATH_SETTINGS_COLORS_CLOTHE = "Character/Customize/clotheColorsVariants";
    private const string PATH_SETTINGS_COLORS_HAIR = "Character/Customize/hairColorsVariants";

    private SkinnedMeshRenderer rendererMaterial;
    // Use this for initialization
    void Start()
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

    private void RandomizeColorMaterial ()
    {
        string path = skinType == SkinType.Clothe ? PATH_SETTINGS_COLORS_CLOTHE : PATH_SETTINGS_COLORS_HAIR;
        SkinSettings skinSettings = Resources.Load<SkinSettings>(path);
         rendererMaterial.material.color = skinSettings.GetRandomSkinColor();
        
    }
    }