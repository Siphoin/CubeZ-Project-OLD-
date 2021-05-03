using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

    public class CollectorLoaderCacheObjects : CollectorLoaderBase, ICollectorLoader
    {
    Dictionary<string, SerializeObjectInstance> instancesObjects;
    Dictionary<string, SerializedObjectMono> clonesObjects;

    public void Load()
    {
        StartCoroutine(LoadAsync());
    }

    public IEnumerator LoadAsync()
    {
         clonesObjects = GameCacheManager.gameCache.containerCacheObjects.objectsClones;
         instancesObjects = GameCacheManager.gameCache.containerCacheObjects.objectsInstances;
        
        yield return new WaitForSecondsRealtime(1.0f / 60.0f);

        StartCoroutine(LoadInstanceObjects(instancesObjects));


        yield return new WaitForSecondsRealtime(1.0f / 60.0f);

      //  StartCoroutine(LoadClonesObjects(clonesObjects));
    }

    private IEnumerator LoadInstanceObjects (Dictionary<string, SerializeObjectInstance> data)
    {
        
        CacheObject[] cacheObjectsInstance = FindCacheObjectWithFitler(a => a.IsClone == false);
        for (int i = 0; i < cacheObjectsInstance.Length; i++)
        {
            yield return new WaitForSecondsRealtime(0.00001f);


            CacheObject cacheObject = cacheObjectsInstance[i];


            if (data.ContainsKey(cacheObject.name))
            {
              SerializeObjectInstance dataObject = data.First(b => b.Value.nameObject == cacheObjectsInstance[i].name).Value;


                if (dataObject.isDead)
                {
                    Destroy(cacheObject.gameObject);
                }

                else
                {
                    cacheObject.transform.position = dataObject.position;
                    cacheObject.transform.rotation = Quaternion.Euler(dataObject.rotation);

                    cacheObject.SetId(dataObject.id);
                }
            }
            CallEventOperationFinish();
           
        }
        StartCoroutine(LoadClonesObjects(clonesObjects));
        yield return null;


    }

    private IEnumerator LoadClonesObjects (Dictionary<string, SerializedObjectMono> data)
    {

        for (int i = 0; i < data.Count; i++)
        {
            yield return new WaitForSecondsRealtime(0.001f);
            SerializedObjectMono objData = data.ElementAt(i).Value;

            if (objData.prefabPath != "Not_Path")
            {
                GameObject newClonePrefab = Resources.Load<GameObject>(objData.prefabPath);


                GameObject newClone = Instantiate(newClonePrefab);


                newClone.transform.position = objData.position;
                newClone.transform.rotation = Quaternion.Euler(objData.rotation);

                newClone.GetComponent<CacheObject>().SetId(objData.id);
            }

            CallEventOperationFinish();
        }

        CallEventFinish();
        yield return null;
    }

    private CacheObject[] FindCacheObjectWithFitler (Func<CacheObject, bool> predicate)
    {
       return FindObjectsOfType<CacheObject>().Where(predicate).ToArray();
    }
}