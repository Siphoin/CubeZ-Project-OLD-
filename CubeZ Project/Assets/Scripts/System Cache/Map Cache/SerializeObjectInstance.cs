using System.Collections;
using UnityEngine;
[System.Serializable]
    public class SerializeObjectInstance : SerializedObjectMono
    {
    public bool isDead = false;
    public string nameObject = "";
       public SerializeObjectInstance ()
    {

    }

    public SerializeObjectInstance(GameObject gameObject, string prefabPath, bool isDead, string id)
    {
        nameObject = gameObject.name;
        this.isDead = isDead;
        SetData(gameObject, prefabPath, id);
    }
    }