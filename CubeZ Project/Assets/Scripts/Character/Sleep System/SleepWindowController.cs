using UnityEngine;

public class SleepWindowController : MonoBehaviour
    {
    private const string PATH_PREFAB_SLEEP_WINDOW = "Prefabs/UI/SleepWindow";

    private SleepWindow sleepWindowPrefab;
        // Use this for initialization
        void Start()
        {
        if (PlayerManager.Manager == null)
        {
            throw new SleepWindowControllerException("Player manager not found");
        }

        if (PlayerManager.Manager.Player == null)
        {
            throw new SleepWindowControllerException("Player not found");
        }

        sleepWindowPrefab = Resources.Load<SleepWindow>(PATH_PREFAB_SLEEP_WINDOW);

        if (sleepWindowPrefab == null)
        {
            throw new SleepWindowControllerException("prefab sleep window not found");
        }

        PlayerManager.Manager.Player.onSleep += CreateSleepWindow;
    }

    private void CreateSleepWindow(bool isSleeping)
    {
        if (isSleeping)
        {
            Instantiate(sleepWindowPrefab);
        }
    }


    }