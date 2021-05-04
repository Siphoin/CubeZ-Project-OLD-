using System.Collections;
using UnityEngine;
[RequireComponent(typeof(Renderer))]
    public class MaterialDataMono : CacheObjectDataMono
    {
    private Renderer renderer;


    public bool IsLoaded { get; set; }

    // Use this for initialization
    void Start()
        {
        if (!TryGetComponent(out renderer))
        {
            throw new CacheObjectException($"({name}) not have component Renderer");
        }

        Ini();


        }

    public MaterialCacheData GetData ()
    {
        return new MaterialCacheData(Id, renderer.material);
    }

    public void SetDataMaterial (MaterialCacheData data)
    {
        if (!data.isMaterial)
        {
            throw new CacheObjectException("data not valid. data is not the material cache data");
        }
        Material targetMaterial = null;
        SkinMaterial skinMaterial = null;

        if (TryGetComponent(out skinMaterial))
        {
            targetMaterial = skinMaterial.IndexMaterial > -1 ? renderer.materials[skinMaterial.IndexMaterial] : renderer.material;
        }

        else
        {
            targetMaterial = renderer.material;
        }
        targetMaterial.color = data.colorTexture.Deserialize();


        if (!string.IsNullOrEmpty(data.texturePath))
        {
            if (SkinDBManager.Manager == null)
            {
                throw new CacheObjectException("skin db manager not found");
            }

            targetMaterial.mainTexture = SkinDBManager.Manager.GetTextureFromPath(data.texturePath);

            targetMaterial.mainTextureOffset = data.offset;
            targetMaterial.mainTextureScale = data.tiling;
        }

    }


}