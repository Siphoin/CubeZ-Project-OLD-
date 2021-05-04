using System.Collections;
using UnityEngine;
[System.Serializable]
    public class SerializeObjectInstance : SerializedObjectMono
    {
    public bool isDead = false;
    public string nameObject;
       public SerializeObjectInstance ()
    {

    }

    public SerializeObjectInstance(GameObject gameObject, string prefabPath, bool isDead, string id)
    {
        this.isDead = isDead;
        nameObject = gameObject.name;
        SetData(gameObject, prefabPath, id);
    }
    }