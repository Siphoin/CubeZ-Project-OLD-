using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
    {

    private const string PATH_PREFAB_SELECT_MAP_WINDOW = "Prefabs/UI/WindowSelectMap";

    private SelectMapWindow selectMapWindowPrefab;

    [SerializeField] private TextMeshProUGUI versionText;
    [SerializeField] private Button buttonContunie;

   
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

        selectMapWindowPrefab = Resources.Load<SelectMapWindow>(PATH_PREFAB_SELECT_MAP_WINDOW);

        if (selectMapWindowPrefab == null)
        {
            throw new MainMenuException("select map window prefab not found");
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
        Instantiate(selectMapWindowPrefab);
        }

            public void ContinueSession ()
        {
        Loading.LoadScene(GameCacheManager.gameCache.mapName);
    }

        public void OpenSettings ()
        {

        }


    }