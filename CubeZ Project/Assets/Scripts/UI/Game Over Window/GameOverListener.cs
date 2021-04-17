using UnityEngine;

public class GameOverListener : MonoBehaviour
    {
    private const string PATH_PREFAB_GAME_OVER_WINDOWS = "Prefabs/UI/GameOverWindow";

    private GameOverWindow gameOverWindowPrefab;
        // Use this for initialization
        void Start()
        {
        if (PlayerManager.Manager == null)
        {
            throw new GameOverListenerException("player manager not found");
        }

        if (PlayerManager.Manager.Player == null)
        {
            throw new GameOverListenerException("player not found");
        }

        gameOverWindowPrefab = Resources.Load<GameOverWindow>(PATH_PREFAB_GAME_OVER_WINDOWS);

        if (gameOverWindowPrefab == null)
        {
            throw new GameOverListenerException("prefab game over window not found");
        }


        PlayerManager.Manager.Player.onDead += CreateGameOverWindow;

    }

    private void CreateGameOverWindow()
    {
        PlayerManager.Manager.Player.onDead -= CreateGameOverWindow;
        Instantiate(gameOverWindowPrefab);
    }

    }