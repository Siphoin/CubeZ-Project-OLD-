using System.Collections;
using UnityEngine;
    public class TreeCacheDataMono : CacheObjectDataMono
    {

    [Header("Дерево")]
    [SerializeField] private Tree tree;
        // Use this for initialization
        void Start()
        {
        Ini();
        }

        // Update is called once per frame
        void Update()
        {

        }

    public TreeCacheData GetTreeData ()
    {
        return new TreeCacheData(Id, new HealthData(tree.CurrentHealth));
    }
    }