using System;
using UnityEngine;

public class Window : MonoBehaviour
{
    public event Action<Window> onExit;

    protected Character player;
    // Use this for initialization
    void Start()
    {
        FrezzePlayer();
    }

    protected void FrezzePlayer()
    {
        if (PlayerManager.Manager == null)
        {
            throw new GameWindowException("Player manager not found");
        }

        else
        {
            player = PlayerManager.Manager.Player;

            if (player == null)
            {
                throw new GameWindowException("Player not found");
            }
            player.FrezzeCharacter();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Exit()
    {
        player.ActivateCharacter();
        onExit?.Invoke(this);
        Destroy(gameObject);
    }
}