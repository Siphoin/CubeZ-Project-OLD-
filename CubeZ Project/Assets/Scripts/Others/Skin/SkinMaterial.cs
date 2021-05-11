using UnityEngine;
public class SkinMaterial : MonoBehaviour, ISkinMaterial
{
    [SerializeField] SkinType skinType = SkinType.Clothe;

    [SerializeField] int indexMaterial = -1;

    [SerializeField] private bool awakeGenerationColor = true;
    protected const string PATH_FOLBER_SETTINGS = "Character/Customize/";
    protected const string PREFIX_NAME_SETTINGS = "ColorsVariants";

    private Renderer rendererMaterial;

    public SkinType SkinType { get => skinType; }
    public int IndexMaterial { get => indexMaterial; }
    public bool AwakeGenerationColor { get => awakeGenerationColor; }

    protected bool colorsSeted = false;


    

    // Use this for initialization
    void Awake()
    {
        if (awakeGenerationColor)
        {
        if (!CheckSkinMaterialMono() && !colorsSeted)
        {
            RandomizeColorMaterial();
        }
        }


    }

    protected void Ini()
    {
        if (!TryGetComponent(out rendererMaterial))
        {
            if (this is SkinArrayMaterial == false)
            {
            throw new SkinMaterialException("not found component Renderer");
            }

        }


    }

    // Update is called once per frame
    void Update()
    {

    }

    public virtual void RandomizeColorMaterial()
    {
        string path = PATH_FOLBER_SETTINGS + skinType.ToString().ToLower() + PREFIX_NAME_SETTINGS;
        SkinSettings skinSettings = Resources.Load<SkinSettings>(path);
        if (skinSettings == null)
        {
            throw new SkinMaterialException("not found component settings materiaL color materials");
        }
        SetColorMaterial(skinSettings.GetRandomSkinColor());

    }

    public bool CheckSkinMaterialMono()
    {
        MaterialDataMono materialDataMono = null;


        if (TryGetComponent(out materialDataMono))
        {
            return materialDataMono.IsLoaded;
        }

        return false;
    }

    public virtual void SetColorMaterial (Color color)
    {
        Ini();


        if (indexMaterial > -1)
        {
            if (rendererMaterial.materials.Length < indexMaterial)
            {
                throw new SkinMaterialException("index material out of range");
            }

            rendererMaterial.materials[indexMaterial].color = color;
            return;
        }
        rendererMaterial.material.color = color;
        colorsSeted = true;
    }

    public virtual Color GetColor ()
    {
        Ini();
        return rendererMaterial.material.color;
    }

    public virtual void SetTexture (Texture texture)
    {
        if (indexMaterial > -1)
        {
            if (rendererMaterial.materials.Length < indexMaterial)
            {
                throw new SkinMaterialException("index material out of range");
            }

            rendererMaterial.materials[indexMaterial].mainTexture = texture;
            return;
        }
        rendererMaterial.material.mainTexture = texture;
    }
}