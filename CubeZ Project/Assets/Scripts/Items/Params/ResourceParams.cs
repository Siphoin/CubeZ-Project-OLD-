using UnityEngine;

[System.Serializable]
public class ResourceParams : BaseParamsItem
{
    [Header("ID ресурса")]
    public string idResource = "ID_RESOURCE";
    public ResourceParams()
    {

    }

    public ResourceParams(ResourceParams copyClass)
    {
        copyClass.CopyAll(this);
    }
}