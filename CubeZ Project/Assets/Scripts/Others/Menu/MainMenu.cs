using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
    {
    [SerializeField] TextMeshProUGUI versionText;
    [SerializeField] Button buttonContunie;
    // Use this for initialization
    void Start()
        {
        if (buttonContunie == null)
        {
            throw new MainMenuException("button continue not seted");
        }


        if (versionText == null)
        {
            throw new MainMenuException("version text not seted");
        }

        versionText.text = Application.version;
        buttonContunie.interactable = LoaderGameCache.IsLoaded;
        }


        public void Quit ()
        {
            Application.Quit();
        }


        public void SelectMap ()
        {
        GameCacheManager.StartNewGameSession();
        Loading.LoadScene("map1");
        }

            public void ContinueSession ()
        {
        Loading.LoadScene(GameCacheManager.gameCache.mapName);
    }

        public void OpenSettings ()
        {

        }


    }