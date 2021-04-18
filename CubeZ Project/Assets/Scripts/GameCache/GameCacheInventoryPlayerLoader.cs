using UnityEngine;

public class GameCacheInventoryPlayerLoader : MonoBehaviour
{
    [SerializeField] InventoryPlayerSettings inventoryPlayerSettings;
    // Use this for initialization
    void Awake()
    {
        if (inventoryPlayerSettings == null)
        {
            throw new GameCacheInventoryPlayerException("settings inventory player  not seted!!!");
        }

            GameCacheManager.gameCache.inventory = new InventoryContainerPlayer(inventoryPlayerSettings.data);
            Debug.Log("inventory player settings is loaded");
        

        Destroy(gameObject);
    }

}