using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
[CreateAssetMenu(menuName = "Inventory/Item/Resource ID List", order = 4)]
public class ResourcesIDList : ScriptableObject
    {
    [SerializeField] private List<string> idList = new List<string>();

    public bool IDResourceRequest (string id)
    {
        return idList.Contains(id);
    }
    }