using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;

public class MainMenu : MonoBehaviour, IFaderImage
    {

    private const string PATH_PREFAB_SELECT_MAP_WINDOW = "Prefabs/UI/WindowSelectMap";

    private SelectMapWindow selectMapWindowPrefab;

    [SerializeField] private TextMeshProUGUI versionText;

    [SerializeField] private ButtonMenu buttonContunie;
    [SerializeField] private ButtonMenu buttonQuit;
    [SerializeField] private ButtonMenu buttonSettings;
    [SerializeField] private ButtonMenu buttonSelectMap;

    [SerializeField] private Image _logoText;
    [SerializeField] private Image _logo;

   
    // Use this for initialization
    void Start()
    {
        if (!buttonContunie)
        {
            throw new MainMenuException("button continue not seted");
        }

        if (!buttonSelectMap)
        {
            throw new MainMenuException("button select map not seted");
        }

        if (!buttonSettings)
        {
            throw new MainMenuException("button settings not seted");
        }

        if (!buttonQuit)
        {
            throw new MainMenuException("button quit not seted");
        }

        if (!_logo)
        {
            throw new MainMenuException("logo not seted");
        }

        if (!_logoText)
        {
            throw new MainMenuException("logo text not seted");
        }


        if (!versionText)
        {
            throw new MainMenuException("version text not seted");
        }

        selectMapWindowPrefab = Resources.Load<SelectMapWindow>(PATH_PREFAB_SELECT_MAP_WINDOW);

        if (selectMapWindowPrefab == null)
        {
            throw new MainMenuException("select map window prefab not found");
        }

        versionText.text = Application.version;
        buttonContunie.SetInteractable(LoaderGameCache.IsLoaded);

        AddListenersOnButtons();

        TurnAnimation();
    }

    private void AddListenersOnButtons()
    {
        buttonContunie.AddListener(ContinueSession);
        buttonQuit.AddListener(Quit);
        buttonSelectMap.AddListener(SelectMap);
        buttonSettings.AddListener(OpenSettings);
    }

    private void OpenSettings ()
        {

        }


    private void TurnAnimation ()
    {
        FadeImage(_logo, Color.white, 2);
        FadeImage(_logoText, Color.white, 2);

        versionText.color = new Color();
        versionText.DOColor(Color.white, 2);
        versionText.DOFade(1, 2);
    }

    public void FadeImage (Image image, Color color, float time)
    {
        image.color = new Color();
        image.DOColor(color, 2);
        image.DOFade(1, 2);
    }

    private void Quit() => Application.Quit();

    private void SelectMap() => Instantiate(selectMapWindowPrefab);

    private void ContinueSession() => Loading.LoadScene(GameCacheManager.gameCache.mapName);


}