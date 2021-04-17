using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loading : MonoBehaviour
    {
    private static string sceneName = "main_menu";

    private const string DEFAULT_SCENE_NAME = "main_menu";

    private const string SCENE_NAME_LOADING = "loading";

    private const string PATH_PREFAB_BACKGROUND_LOADING_FINISH = "Prefabs/UI/LoadingFinishAnimation";


    [SerializeField] private TextMeshProUGUI textLoad;

    private static GameObject backgroundFinishLoad = null;
        // Use this for initialization
        void Start()
        {
        Ini();
        LoadSceneAsync();
        SetStateCursorVisible(false);

        }


    private IEnumerator LoadSceneProgress ()
    {
        AsyncOperation asyncOperation = null;
        try
        {
           asyncOperation = SceneManager.LoadSceneAsync(sceneName);
        }
        catch
        {
            Debug.LogError($"{sceneName} not exits in Build Settings");
            LoadScene(DEFAULT_SCENE_NAME);
        }

        while (!asyncOperation.isDone)
        {
            float progress = asyncOperation.progress / 0.9f;
            textLoad.text = string.Format("{0:0}%", progress / 100);

            if (progress >= 0.9f)
            {
                GameObject backgroundFinish = Instantiate(backgroundFinishLoad);
                DontDestroyOnLoad(backgroundFinish);


                SetStateCursorVisible(true);
            }
            yield return null;
        }
    }

    private void LoadSceneAsync ()
    {
        StartCoroutine(LoadSceneProgress());
    }

    public static void LoadScene (string nameScene)
    {


        sceneName = nameScene;


        try
        {
            SceneManager.LoadScene(SCENE_NAME_LOADING);
        }
        catch 
        {

            throw new LoadingException($"scene {SCENE_NAME_LOADING} not exits in Build Settings");
        }
    }

    private void SetStateCursorVisible (bool visible)
    {
        Cursor.visible = visible;
    }

    private static void Ini ()
    {
        if (backgroundFinishLoad != null)
        {
            return;
        }


        backgroundFinishLoad = Resources.Load<GameObject>(PATH_PREFAB_BACKGROUND_LOADING_FINISH);

        if (backgroundFinishLoad == null)
        {
            throw new LoadingException("prefab background loading finish not found");
        }
    }
    }