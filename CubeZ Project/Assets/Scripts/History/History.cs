using System;
using System.Collections;
using TMPro;
using UnityEngine;

    public class History : MonoBehaviour
    {
    [SerializeField] private TextMeshProUGUI textHistory;
    [SerializeField] private float timeOutNextText = 0.5f;

    private Animator animatorText;


    private string[] historyTexts;

    private const string NAME_FOLBER_DATA_HISTORY = "TXT/";

    private const string NAME_FILE_DATA = "history_";

    private const string PREFIX_LANG_EN = "EN";

    private const string PREFIX_LANG_RU = "RU";

    private const string NAME_ANIM_SHOW_TEXT = "text_history_show";

    private const string NAME_ANIM_HIDE_TEXT = "text_history_hide";


    private const string NAME_FILE_HISTORY = "history.json";


    private const float TIME_OUT_ANIM_TEXT_SPEED = 1.45f;


    private Color startColorText;
    private Color alphaColorText;
    // Use this for initialization
    void Start()
        {

        if (textHistory == null)
        {
            throw new HistoryException("text histoery not seted");
        }


        string fullPath = NAME_FOLBER_DATA_HISTORY + NAME_FILE_DATA;

        switch (Application.systemLanguage)
        {
            case SystemLanguage.Russian:
                fullPath += PREFIX_LANG_RU;
                break;
            default:
                fullPath += PREFIX_LANG_EN;
                break;
        }

        TextAsset textAsset = Resources.Load<TextAsset>(fullPath);

        if (textAsset == null)
        {
            throw new HistoryException($"Not found text asset: Path {fullPath}");
        }

        if (string.IsNullOrEmpty(textAsset.text.Trim()))
        {

            throw new HistoryException("text history is null or emtry");
        }
        string[] stringSeparators = new string[] { "\n" };


        historyTexts =  textAsset.text.Split(stringSeparators, StringSplitOptions.None);

        startColorText = textHistory.color;


        var alphaColor = startColorText;
        alphaColor.a = 0;
        alphaColorText = alphaColor;

        if (!textHistory.TryGetComponent(out animatorText))
        {
            throw new HistoryException("not found component Animator on text history");
        }

        StartCoroutine(ShowingText());
    }

    private IEnumerator ShowingText ()
    {
        animatorText.enabled = false;
        textHistory.color = alphaColorText;
        int currentIndex = 0;
        yield return new WaitForSeconds(3);
        animatorText.enabled = true;
        while (true)
        {
            if (currentIndex < historyTexts.Length)
            {
                textHistory.text = historyTexts[currentIndex];
                currentIndex++;

                animatorText.Play(NAME_ANIM_SHOW_TEXT);
                yield return new WaitForSeconds(TIME_OUT_ANIM_TEXT_SPEED + (timeOutNextText * textHistory.text.Length));

                animatorText.Play(NAME_ANIM_HIDE_TEXT);
                yield return new WaitForSeconds(TIME_OUT_ANIM_TEXT_SPEED);
            }

            else
            {
                yield return new WaitForSeconds(3);
                Exit();
            }
        }
    }

    private void Exit ()
    {
            CacheSystem.SaveSerializeObject(null, NAME_FILE_HISTORY, new HistoryData(true));
        Loading.LoadScene(Loading.LastSceneName);
    }

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            Exit();
        }
    }
}