using UnityEngine;

public class PlayerManager : MonoBehaviour
    {
    private static PlayerManager manager;
    private Character player;

    public static PlayerManager Manager { get => manager; }
    public Character Player { get => player; }

    private const string TAG_PLAYER = "Player";

    // Use this for initialization
    void Awake()
        {
        Debug.Log(332434);
        if (manager == null)
        {
            manager = this;
        }

        else
        {
            Destroy(gameObject);
        }

        if (!GameObject.FindGameObjectWithTag(TAG_PLAYER).TryGetComponent(out player))
        {
            throw new PlayerManagerException("Player not found!");
        }
        }
    }