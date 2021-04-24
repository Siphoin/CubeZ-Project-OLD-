using System.Collections;
using UnityEngine;
[System.Serializable]
    public class MaterialCacheData : CacheObjectData
    {
    public string texturePath;
    public Vector3 offset;
    public Vector3 tiling;
    public SerializeColor colorTexture;

    public MaterialCacheData ()
    {

    }

    public MaterialCacheData (string idObjectOwnerMaterial, Material material)
    {
        if (material == null)
        {
            throw new MaterialCacheDataException("material is null");
        }


        idObject = idObjectOwnerMaterial;
        isMaterial = true;
        tiling = material.mainTextureScale;
        offset = material.mainTextureOffset;
        if (material.mainTexture != null)
        {
        texturePath = material.mainTexture.name;
        }

        colorTexture = material.color.Serialize();
    }

    }