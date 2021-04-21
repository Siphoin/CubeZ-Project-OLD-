using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapCacheManager : MonoBehaviour
    {
    private const string FOLBER_NAME = "session/";

    private const string FILE_NAME = "game_session.json";

    private const string TAG_INSTANCE_OBJECT_MANAGER = "InstanceObjectManager";

    InstanceObjectManager instanceObjectManager;
        // Use this for initialization
        void Start()
        {
        instanceObjectManager = GameObject.FindGameObjectWithTag(TAG_INSTANCE_OBJECT_MANAGER).GetComponent<InstanceObjectManager>();
        }

        // Update is called once per frame
        void Update()
        {

        }

    private void OnApplicationQuit()
    {
       


        string mapName = SceneManager.GetActiveScene().name;


        CacheObject[] cacheObjects = FindObjectsOfType<CacheObject>();

        for (int i = 0; i < cacheObjects.Length; i++)
        {
            CacheObjectAdding(cacheObjects[i]);
        }

        GameCacheManager.gameCache.versionClient = Application.version;
        GameCacheManager.gameCache.mapName = mapName;


        GameCacheManager.gameCache.containerCacheObjects.objectsInstances = instanceObjectManager.GetInstanceObjects();


        CacheSystem.SaveSerializeObject(FOLBER_NAME, FILE_NAME, GameCacheManager.gameCache, Newtonsoft.Json.Formatting.Indented);
        
    }

    private void CacheObjectAdding (CacheObject cacheObject)
    {
        if (cacheObject.IsClone)
        {
            
            SerializedObjectMono serializedObject = new SerializedObjectMono(cacheObject.gameObject, cacheObject.PrefabPath, cacheObject.Id);
                GameCacheManager.gameCache.containerCacheObjects.objectsClones.Add(cacheObject.Id, serializedObject);
            
        }

    }
}