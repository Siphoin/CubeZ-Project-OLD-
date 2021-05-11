using System.Linq;
using UnityEngine;

public class SkinArrayMaterial : SkinMaterial
    {
    [SerializeField] Renderer[] renderers;
        // Use this for initialization
        void Awake()
    {
        Ini();
        if (AwakeGenerationColor)
        {
        if (!CheckSkinMaterialMono() && !colorsSeted)
        {
            RandomizeColorMaterial();
        }
        }


    }

    private void Ini()
    {
        if (renderers.Length == 0)
        {
            throw new SkinMaterialException("renderers array is emtry!");
        }

        if (renderers.Any(render => render == null))
        {
            throw new SkinMaterialException("any render is null");
        }
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
        SetColorMaterial(selectedColor);

    }

    private void SetMaterialColorSkinnedRenderer (Color color, Renderer renderer)
    {
            base.Ini();
        
        renderer.material.color = color;

    }

    public override void SetColorMaterial(Color color)
    {
        for (int i = 0; i < renderers.Length; i++)
        {
            SetMaterialColorSkinnedRenderer(color, renderers[i]);
        }

        colorsSeted = true;

       
    }

    public override Color GetColor()
    {
        return renderers[0].materials[0].color;

    }



}