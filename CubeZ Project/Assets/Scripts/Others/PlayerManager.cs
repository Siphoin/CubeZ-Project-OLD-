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


    private void Awake()
    {
        Ini();

    }

    private void Ini()
    {
        if (manager == null)
        {
            manager = this;
        }

        else
        {
            Destroy(gameObject);
        }
    }

    public void SetPlayer (Character player)
    {
        if (player == null)
        {
            throw new PlayerManagerException("player argument is null");
        }

        this.player = player;
#if UNITY_EDITOR
        Debug.Log($"player {this.player.name} seted as my player");
#endif
    }
}