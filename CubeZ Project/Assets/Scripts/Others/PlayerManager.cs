using UnityEngine;

public class PlayerManager : MonoBehaviour
    {
    private static PlayerManager manager;
    private Character player;
    private CharacterStatsController playerStats;

    public static PlayerManager Manager { get => manager; }
    public Character Player { get => player; }
    public CharacterStatsController PlayerStats { get => playerStats; }

    private const string TAG_PLAYER = "MyPlayer";

    // Use this for initialization
    void Awake()
        {
        if (manager == null)
        {
            manager = this;
        }

        else
        {
            Destroy(gameObject);
        }
        GameObject player = GameObject.FindGameObjectWithTag(TAG_PLAYER);
        if (player == null)
        {
            throw new PlayerManagerException("Player not found!");
        }
        if (!player.TryGetComponent(out this.player))
        {
            throw new PlayerManagerException("Player not exits component Character");
        }

        if (!player.TryGetComponent(out playerStats))
        {
            throw new PlayerManagerException("Player not exits component CharacterStatsController");
        }
    }
    }