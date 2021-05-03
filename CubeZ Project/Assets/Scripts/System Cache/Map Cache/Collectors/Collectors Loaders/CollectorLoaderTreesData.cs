using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

    public class CollectorLoaderTreesData : CollectorLoaderDataBase, ICollectorLoader
    {

    // Use this for initialization
    void Start()
        {

        }

    public void Load()
    {
        Ini();
        StartCoroutine(LoadAsync());

    }

    public IEnumerator LoadAsync()
    {
        if (!GameCacheManager.gameCache.dataContainerObjects.objectsData.ContainsKey(idCollection))
        {
            throw new CollectorLoaderException("collection id data not found");
        }

        Tree[] tress = FindObjectsOfType<Tree>();

        IEnumerable<object> dataContainer = GameCacheManager.gameCache.dataContainerObjects.objectsData[idCollection];

        List<JObject> array = dataContainer.Cast<JObject>().ToList();

        for (int i = 0; i < array.Count; i++)
        {
            yield return new WaitForSecondsRealtime(0.001f);

            Tree tree = tress[i];
            TreeCacheData data = CacheSystem.DeserializeObject<TreeCacheData>(array[i]);
            tree.SetData(data);

            CallEventOperationFinish();
        }

        CallEventFinish();

        


    }

}