using System.Collections.Generic;
[System.Serializable]
    public class ContainerCacheObjects
    {
    public Dictionary<string, SerializedObjectMono> objectsClones = new Dictionary<string, SerializedObjectMono>();

    public Dictionary<string, SerializeObjectInstance> objectsInstances = new Dictionary<string, SerializeObjectInstance>();
}