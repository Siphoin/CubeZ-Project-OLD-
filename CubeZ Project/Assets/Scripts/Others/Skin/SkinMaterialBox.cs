using System.Collections;
using UnityEngine;
[RequireComponent(typeof(Renderer))]
    public class SkinMaterialBox : MonoBehaviour
    {
    private Renderer renderer;

    private const string PATH_TEXTURES_BOX = "Textures/Box";


    private const float MIN_VALUE_OFFSET = 0.9f;
    private const float MAX_VALUE_OFFSET = 1001f;

    // Use this for initialization
    void Start()
        {
        if (!TryGetComponent(out renderer))
        {
            throw new SkinMaterialException("component Renderer not found");
        }

        float x = GetRandomOffset();
        float y = GetRandomOffset();
        Vector2 offset = new Vector2(x, y);
        renderer.material.mainTextureOffset = offset;

        SetRandomTexture();
        }

        // Update is called once per frame
        void Update()
        {

        }

    private float GetRandomOffset ()
    {
        return Random.Range(MIN_VALUE_OFFSET, MAX_VALUE_OFFSET);
    }

    private void SetRandomTexture ()
    {
        Texture[] textures = Resources.LoadAll<Texture>(PATH_TEXTURES_BOX);
        renderer.material.mainTexture = textures[Random.Range(0, textures.Length)];
    }
    }