using System;
using System.Collections;
using UnityEngine;

    public class ShowAutoSaveUIController : MonoBehaviour
    {
    private MapCacheManager mapCacheManager;

    private const string PATH_PREFAB_SHOW_AUTOSAVE_UI = "Prefabs/UI/ShowAutoSaveUI";

    private ShowAutoSaveUI showAutoSaveUIPrefab;


        // Use this for initialization
        void Start()
        {
        try
        {
            mapCacheManager = FindObjectOfType<MapCacheManager>();

            if (mapCacheManager == null)
            {
            throw new ShowAutoSaveUIControllerException("map cache manager not found");
            }
        }
        catch
        {


        }

        showAutoSaveUIPrefab = Resources.Load<ShowAutoSaveUI>(PATH_PREFAB_SHOW_AUTOSAVE_UI);

        if (showAutoSaveUIPrefab == null)
        {
            throw new ShowAutoSaveUIControllerException("prefab auto save ui not found");
        }

        mapCacheManager.onSaveSession += ShowText;
    }


    private void ShowText()
    {
         Instantiate(showAutoSaveUIPrefab);
    }
}