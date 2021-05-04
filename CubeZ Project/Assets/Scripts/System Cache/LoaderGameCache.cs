using UnityEngine;

public class LoaderGameCache : MonoBehaviour
    {
        private const string NAME_FOLBER_SESSION = "session/";

        private const string NAME_FILE = "game_session.json";
        public static bool IsLoaded { get; set; }

        // Use this for initialization
        void Awake()
        {
     string   path = CacheSystem.GetPathAssetsData() + "localData/" + NAME_FOLBER_SESSION;
        Debug.Log(path);
        if (CacheSystem.FileExits(NAME_FOLBER_SESSION, NAME_FILE) && !IsLoaded)
            {
                try
                {
                    GameCacheManager.gameCache = CacheSystem.DeserializeObject<GameCache>(path + NAME_FILE);
                
                    IsLoaded = Application.version == GameCacheManager.gameCache.versionClient;

#if UNITY_EDITOR
                    Debug.Log($"Game cache loaded is sucess. Path: {path}");
#endif
                }
                catch (System.Exception e)
                {
                Debug.LogError($"on load game cache error: {e.Message}");
                    Application.Quit();
                }
            }

            else
        {
#if UNITY_EDITOR
            Debug.Log("file game session not found");
#endif
        }

        Destroy(gameObject);

        }

    }