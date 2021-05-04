using UnityEngine;
public class SkinMaterial : MonoBehaviour, ISkinMaterial
{
    [SerializeField] SkinType skinType = SkinType.Clothe;

    [SerializeField] int indexMaterial = -1;
    protected const string PATH_FOLBER_SETTINGS = "Character/Customize/";
    protected const string PREFIX_NAME_SETTINGS = "ColorsVariants";

    private Renderer rendererMaterial;

    public SkinType SkinType { get => skinType; }
    public int IndexMaterial { get => indexMaterial; }

    // Use this for initialization
    void Awake()
    {
        Ini();
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
        if (!CheckSkinMaterialMono())
        {
        RandomizeColorMaterial();
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
        if (indexMaterial > -1)
        {
            if (rendererMaterial.materials.Length < indexMaterial)
            {
                throw new SkinMaterialException("index material out of range");
            }

            rendererMaterial.materials[indexMaterial].color = skinSettings.GetRandomSkinColor();
            return;
        }
        rendererMaterial.material.color = skinSettings.GetRandomSkinColor();

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
}