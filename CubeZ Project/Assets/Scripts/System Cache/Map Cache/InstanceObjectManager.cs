using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

    public class InstanceObjectManager : MonoBehaviour, IInvokerMono
    {
        private CacheObject[] instanceObjects;

        private Dictionary<string, SerializeObjectInstance> serializeObjectInstances = new Dictionary<string, SerializeObjectInstance>();
        // Use this for initialization
        void Start()
    {
        CallInvokingMethod(LoadInstanceObjects, 1);
    }

    private void LoadInstanceObjects()
    {
        CacheObject[] cacheObjects = FindObjectsOfType<CacheObject>();
        instanceObjects = cacheObjects.Where(item => item.IsClone == false).ToArray();
        for (int i = 0; i < instanceObjects.Length; i++)
        {
            CacheObject cacheObject = instanceObjects[i];
            cacheObject.onRemove += ObjectRemoved;
            SerializeObjectInstance objectInstance = new SerializeObjectInstance(cacheObject.gameObject, cacheObject.PrefabPath, false, cacheObject.Id);
            serializeObjectInstances.Add(cacheObject.Id, objectInstance);
        }
    }

    private void ObjectRemoved(string id)
        {
        SerializeObjectInstance cacheObject = serializeObjectInstances.First(i => i.Value.id == id).Value;
            cacheObject.isDead = true;
        }
        public Dictionary<string, SerializeObjectInstance> GetInstanceObjects ()
        {
            return serializeObjectInstances;
        }

    public void CallInvokingEveryMethod(Action method, float time)
    {
        InvokeRepeating(method.Method.Name, time, time);
    }

    public void CallInvokingMethod(Action method, float time)
    {
        Invoke(method.Method.Name, time);
    }
}