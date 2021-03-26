using System;
using System.Collections;
using UnityEngine;

    public class NewDayShowUIController : MonoBehaviour
    {
    private TextShowNewDayUI showDayTextPrefab;
    private const string PATH_SHOW_DAY_UI_TEXT_PREFAB = "Prefabs/UI/ShowDayUI";

    private WorldManager worldManager;
        // Use this for initialization
        void Start()
        {
        showDayTextPrefab = Resources.Load<TextShowNewDayUI>(PATH_SHOW_DAY_UI_TEXT_PREFAB);

        if (showDayTextPrefab == null)
        {
            throw new NewDayShowUIControllerException("prefab new day twext ui not found");
        }
        if (WorldManager.Manager == null)
        {
            throw new NewDayShowUIControllerException("world manager not found");
        }
        worldManager = WorldManager.Manager;
        worldManager.onDayChanged += NewDayListener;
    }

    private void NewDayListener(DayTimeType day)
    {
        if (day == DayTimeType.Morming)
        {
            TextShowNewDayUI newDayText = Instantiate(showDayTextPrefab);
            newDayText.SetDay(worldManager.CurrentDay);
        }
    }

    // Update is called once per frame
    void Update()
        {

        }
    }