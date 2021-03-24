using UnityEngine;
public class SkinMaterial : MonoBehaviour
{
    [SerializeField] SkinType skinType = SkinType.Clothe;
    protected const string PATH_FOLBER_SETTINGS = "Character/Customize/";
    protected const string PREFIX_NAME_SETTINGS = "ColorsVariants";

    private Renderer rendererMaterial;

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
        string path = PATH_FOLBER_SETTINGS + skinType.ToString().ToLower() + PREFIX_NAME_SETTINGS;
        SkinSettings skinSettings = Resources.Load<SkinSettings>(path);
        rendererMaterial.material.color = skinSettings.GetRandomSkinColor();

    }

}