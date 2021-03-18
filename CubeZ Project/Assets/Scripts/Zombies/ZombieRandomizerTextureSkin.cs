using System.Collections;
using UnityEngine;
[RequireComponent(typeof(SkinnedMeshRenderer))]
    public class ZombieRandomizerTextureSkin : MonoBehaviour
    {
    SkinnedMeshRenderer meshRenderer;

    private const float MAX_OFFSET_TEXTURE = 1000;

    private const float MAX_TILING_TEXTURE = 2;

    private const string PATH_TEXTURES = "Textures/Zombie";

    Texture[] textureVariants;
        // Use this for initialization
        void Start()
    {
        if (!TryGetComponent(out meshRenderer))
        {
            throw new ZombieRandomizerTextureSkinException("not found component SkinnedMeshRenderer");
        }

        textureVariants = Resources.LoadAll<Texture>(PATH_TEXTURES);

        if (textureVariants == null || textureVariants.Length == 0)
        {
            throw new ZombieRandomizerTextureSkinException("textures zombies not found");
        }

        GenerateOffsetTexture();
        GenerateTilingTexture();
        SetRandomTexture();

    }

    private void SetRandomTexture()
    {
        meshRenderer.material.mainTexture = textureVariants[Random.Range(0, textureVariants.Length)];
    }

    private void GenerateOffsetTexture()
    {
        float x = GenerateСoordinateVectorOffset();
        float y = GenerateСoordinateVectorOffset();
        Vector2 newOffset = new Vector2(x, y);
        meshRenderer.material.mainTextureOffset = newOffset;
    }

    private void GenerateTilingTexture()
    {
        float x = GenerateСoordinateVectorTiling();
        float y = GenerateСoordinateVectorTiling();
        Vector2 newOffset = new Vector2(x, y);
        meshRenderer.material.mainTextureScale = newOffset;
    }

    private float GenerateСoordinateVectorOffset ()
    {
        return Random.Range(1.0f, MAX_OFFSET_TEXTURE + 1.0f);
    }

    private float GenerateСoordinateVectorTiling()
    {
        return Random.Range(1.0f, MAX_TILING_TEXTURE + 1.0f);
    }

}