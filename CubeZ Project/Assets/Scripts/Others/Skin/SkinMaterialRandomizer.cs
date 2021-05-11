using System.Collections;
using UnityEngine;
[RequireComponent(typeof(Renderer))]
    public class SkinMaterialRandomizer : MonoBehaviour, ISkinMaterial
    {
    private Renderer renderer;


    [Header("Имя папки текстур в папке Resources")]
    [SerializeField] private string nameTexturesFolber = "Box";




    [Header("Генерировать случайное положение UV карты")]
    [SerializeField] bool generateUVMap = true;


    [Header("Параметры генерации UV карты")]
    [SerializeField]
    private  float minValueOffset = 0.9f;
    [SerializeField]
    private  float maxValueOffset = 1001f;

    private const string PATH_TEXTURES_FOLBER = "Textures/";

    // Use this for initialization
    void Start()
    {
        Ini();

        if (!CheckSkinMaterialMono())
        {
            if (generateUVMap)
            {
                GenerateRandomUVMap();
            }


            SetRandomTexture();
        }


    }

    private void Ini()
    {
        if (string.IsNullOrEmpty(nameTexturesFolber))
        {
            throw new SkinMaterialException("string name folber textures not must be emtry!");
        }
        if (!TryGetComponent(out renderer))
        {
            throw new SkinMaterialException("component Renderer not found");
        }
    }

    private void GenerateRandomUVMap()
    {
        float x = GetRandomOffset();
        float y = GetRandomOffset();
        Vector2 offset = new Vector2(x, y);
        renderer.material.mainTextureOffset = offset;
    }

    // Update is called once per frame
    void Update()
        {

        }

    private float GetRandomOffset ()
    {
        return Random.Range(minValueOffset, maxValueOffset);
    }

    private void SetRandomTexture ()
    {
        Texture[] textures = Resources.LoadAll<Texture>(PATH_TEXTURES_FOLBER + nameTexturesFolber);

        if (textures.Length == 0)
        {
            throw new SkinMaterialException($"Not found textures in Folber {nameTexturesFolber} Path: {PATH_TEXTURES_FOLBER + nameTexturesFolber}");
        }
        renderer.material.mainTexture = textures[Random.Range(0, textures.Length)];
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

    public Texture GetTexture ()
    { 
        Ini();
        return renderer.material.mainTexture == null ? null : renderer.material.mainTexture;
    }
}