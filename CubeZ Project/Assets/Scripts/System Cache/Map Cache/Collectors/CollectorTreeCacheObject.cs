using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class CollectorTreeCacheObject : CollectorCacheObjectDataBase, ICollectorCacheObjectData<CacheObjectData>
    {
    public IEnumerable<CacheObjectData> GetCollection()
    {
        List<TreeCacheData> dataCollection = new List<TreeCacheData>();

        TreeCacheDataMono[] trees = FindObjectsOfType<TreeCacheDataMono>();

        for (int i = 0; i < trees.Length; i++)
        {
            TreeCacheDataMono tree = trees[i];
            TreeCacheData treeData = tree.GetTreeData();
            dataCollection.Add(treeData);
        }


        return dataCollection;
    }



    // Use this for initialization
    void Start()
        {
        Ini();
    }


    }