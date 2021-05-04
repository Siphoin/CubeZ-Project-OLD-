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
    public bool child = false;
    

    public SerializedObjectMono ()
    {

    }

    public SerializedObjectMono (GameObject gameObject, string prefabPath, string id)
    {
        SetData(gameObject, prefabPath, id);

    }

    protected void SetData(GameObject gameObject, string prefabPath, string id)
    {
        if (gameObject.transform.parent == null)
        {
        position = gameObject.transform.position;
        rotation = gameObject.transform.rotation.eulerAngles;
        }

        else
        {
            position = gameObject.transform.localPosition;
            rotation = gameObject.transform.localRotation.eulerAngles;
            child = true;
        }

        scale = gameObject.transform.localScale;
        this.prefabPath = prefabPath;
        this.id = id;
    }
}