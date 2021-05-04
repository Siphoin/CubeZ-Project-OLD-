using System.Collections;
using System.Linq;
using UnityEngine;

    public class SkinMaterialRandomizerArray : MonoBehaviour, ISkinMaterial
    {

        [SerializeField] Renderer[] renderers;



        [Header("Имя папки текстур в папке Resources")]
        [SerializeField] private string nameTexturesFolber = "Box";




        [Header("Генерировать случайное положение UV карты")]
        [SerializeField] bool generateUVMap = true;


        [Header("Параметры генерации UV карты")]
        [SerializeField]
        private float minValueOffset = 0.9f;
        [SerializeField]
        private float maxValueOffset = 1001f;

        private const string PATH_TEXTURES_FOLBER = "Textures/";

        private Texture currentSelectedfTexture;
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


        if (CheckSkinMaterialMono())
        {
            return;
        }

        if (generateUVMap)
            {
                for (int i = 0; i < renderers.Length; i++)
                {
                    GenerateRandomUVMap(renderers[i].material);
                }

            }

            SetRandomTexture();
            SetCurrentTextureToMaterials();
        }


        private void GenerateRandomUVMap(Material material)
        {
            float x = GetRandomOffset();
            float y = GetRandomOffset();
            Vector2 offset = new Vector2(x, y);
           material.mainTextureOffset = offset;
        }

        private float GetRandomOffset()
        {
            return Random.Range(minValueOffset, maxValueOffset);
        }

        private void SetRandomTexture()
        {
            Texture[] textures = Resources.LoadAll<Texture>(PATH_TEXTURES_FOLBER + nameTexturesFolber);

            if (textures.Length == 0)
            {
                throw new SkinMaterialException($"Not found textures in Folber {nameTexturesFolber} Path: {PATH_TEXTURES_FOLBER + nameTexturesFolber}");
            }
            currentSelectedfTexture = textures[Random.Range(0, textures.Length)];
        }

        private void SetCurrentTextureToMaterials ()
        {
        for (int i = 0; i < renderers.Length; i++)
        {
            renderers[i].material.mainTexture = currentSelectedfTexture;
        }
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
