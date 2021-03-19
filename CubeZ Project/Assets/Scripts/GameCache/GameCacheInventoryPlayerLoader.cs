using UnityEngine;

public class GameCacheInventoryPlayerLoader : MonoBehaviour
{
    [SerializeField] InventoryPlayerSettings inventoryPlayerSettings;
    private static bool isLoaded = false;
    // Use this for initialization
    void Awake()
    {
        if (inventoryPlayerSettings == null)
        {
            throw new GameCacheInventoryPlayerException("settings inventory player  not seted!!!");
        }

        if (!isLoaded)
        {
            GameCacheManager.gameCache.inventory = new InventoryContainerPlayer(inventoryPlayerSettings.data);
            isLoaded = true;
            Debug.Log("inventory player settings is loaded");
        }

        Destroy(gameObject);
    }

}