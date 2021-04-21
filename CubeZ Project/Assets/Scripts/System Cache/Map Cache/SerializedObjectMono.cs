using System.Collections;
using UnityEngine;
[System.Serializable]
    public class SerializedObjectMono
    {
    public Vector3 position;
    public Vector3 rotation;
    public Vector3 scale;
    public string prefabPath;
    public string id;

    public SerializedObjectMono ()
    {

    }

    public SerializedObjectMono (GameObject gameObject, string prefabPath, string id)
    {
        SetData(gameObject, prefabPath, id);

    }

    protected void SetData(GameObject gameObject, string prefabPath, string id)
    {
        position = gameObject.transform.position;
        rotation = gameObject.transform.rotation.eulerAngles;
        scale = gameObject.transform.localScale;
        this.prefabPath = prefabPath;
        this.id = id;
    }
}