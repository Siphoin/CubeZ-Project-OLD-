using System.Collections;
using UnityEngine;
[System.Serializable]
    public class SerializeObjectInstance : SerializedObjectMono
    {
    public bool isDead = false;
       public SerializeObjectInstance ()
    {

    }

    public SerializeObjectInstance(GameObject gameObject, string prefabPath, bool isDead, string id)
    {
        this.isDead = isDead;
        SetData(gameObject, prefabPath, id);
    }
    }