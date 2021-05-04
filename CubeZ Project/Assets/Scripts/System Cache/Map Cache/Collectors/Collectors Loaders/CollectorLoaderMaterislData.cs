using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

    public class CollectorLoaderMaterislData : CollectorLoaderDataBase, ICollectorLoader
    {
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

        MaterialDataMono[] materials = FindObjectsOfType<MaterialDataMono>();

        IEnumerable<object> dataContainer = GameCacheManager.gameCache.dataContainerObjects.objectsData[idCollection];

        List<JObject> array = dataContainer.Cast<JObject>().ToList();

        for (int i = 0; i < array.Count; i++)
        {
            yield return new WaitForSecondsRealtime(0.001f);

            MaterialDataMono mat = null;
            MaterialCacheData data = CacheSystem.DeserializeObject<MaterialCacheData>(array[i]);
            try
            {
             mat = materials.First(m => m.Id == data.idObject);
            }
            catch 
            {
            }
            if (mat != null)
            {
 mat.SetDataMaterial(data);
            mat.IsLoaded = true;
            CallEventOperationFinish();
            }
           
        }

        CallEventFinish();
        yield return null;


    }


    }