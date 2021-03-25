using System.Linq;
using UnityEngine;

public class SkinArrayMaterial : SkinMaterial
    {
    [SerializeField] Renderer[] renderers;
        // Use this for initialization
        void Start()
        {
        if (renderers.Length == 0)
        {
            throw new SkinMaterialException("renderers array is emtry!");
        }
        
        if (renderers.Any(render => render == null))
        {
            throw new SkinMaterialException("any render is null");
        }


        RandomizeColorMaterial();
        }
    public override void RandomizeColorMaterial()
    {
        string path = PATH_FOLBER_SETTINGS + SkinType.ToString().ToLower() + PREFIX_NAME_SETTINGS;
        SkinSettings skinSettings = Resources.Load<SkinSettings>(path);
        if (skinSettings == null)
        {
            throw new SkinMaterialException("not found component settings materiaL color materials");
        }

        Color selectedColor = skinSettings.GetRandomSkinColor();
        for (int i = 0; i < renderers.Length; i++)
        {
            SetMaterialColorSkinnedRenderer(selectedColor, renderers[i]);
        }

    }

    private void SetMaterialColorSkinnedRenderer (Color color, Renderer renderer)
    {
        renderer.material.color = color;
    }
}