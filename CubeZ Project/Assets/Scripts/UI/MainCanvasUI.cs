using UnityEngine;

public class MainCanvasUI : MonoBehaviour
    {
    
        // Use this for initialization
        void Start()
        {
        if (PlayerManager.Manager == null)
        {
            throw new MainCanvasUIException("Player manager not found");
        }

        if (PlayerManager.Manager.Player == null)
        {
            throw new MainCanvasUIException("Player not found");
        }

        PlayerManager.Manager.Player.onDead += PlayerDead;
        }

    private void PlayerDead()
    {
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
        {

        }
    }