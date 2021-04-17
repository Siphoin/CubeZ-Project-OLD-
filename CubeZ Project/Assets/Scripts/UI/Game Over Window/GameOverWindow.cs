using System.Collections;
using TMPro;
using UnityEngine;

    public class GameOverWindow : MonoBehaviour
    {
    private const string NAME_SCENE_MAIN_MENU = "main_menu";

    [SerializeField] private TextMeshProUGUI textResultSession;
        // Use this for initialization
        void Start()
        {

        if (textResultSession == null)
        {
            throw new GameOverWindowException("text result session not seted");
        }
        try
        {
            UIController.Manager.On = false;
        }
        catch
        {

        }

        textResultSession.text = $"Time Session: {GameCacheManager.gameCache.timeSession.ToLongTimeString()}\nZombies killed: {GameCacheManager.gameCache.zombieKils}";
        GameCacheManager.StartNewGameSession();
        }

    public void QuitToWindows ()
    {
        Application.Quit();
    }

    public void BackToMainMenu ()
    {
        UIController.Manager.On = true;
        Loading.LoadScene(NAME_SCENE_MAIN_MENU);
    }
    }