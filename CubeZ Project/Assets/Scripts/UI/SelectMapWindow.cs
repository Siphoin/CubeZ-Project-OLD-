using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectMapWindow : Window
    {
    private const string PATH_PREFAB_SELECT_MAP_BUTTON = "Prefabs/UI/SelectMapButton";
    private const string PATH_DATA_LIST_MAPS = "Db/mapsDb";

    private const string PREFIX_SIZE_MAP_STRING = "Size:";
    private const string PREFIX_NAME_MAP_STRING = "\n\n";
    private const string PREFIX_DECRIPTION_MAP_STRING = "\n\n";


    private MapData selectedMapData;

    private SelectMapButton selectMapButtonPrefab;

    private MapData[] mapsData;

    [Header("Контейнер кнопок выбора карт")]
    [SerializeField] Transform contentButtonsMap;

    [Header("Контейнер информации о карте")]
    [SerializeField] GameObject containerInfoMap;

    [Header("Текст информации карты")]
    [SerializeField] private TextMeshProUGUI textInfoMap;



    [Header("Пиктограмма карты")]
    [SerializeField] private Image pictogram;

    [Header("Кнопка закрыть окно")]
    [SerializeField] private Button buttonCancel;

    [Header("Кнопка выбрать карту")]
    [SerializeField] private Button buttonSelectMap;
    // Use this for initialization
    void Start()
    {
        Ini();

        LoadButtonsMaps();

        SetStateVisibleInfoMap(false);

    }

    private void LoadButtonsMaps()
    {
        for (int i = 0; i < mapsData.Length; i++)
        {
            CreateMapButton(mapsData[i]);
        }
    }

    private void Ini()
    {
        if (contentButtonsMap == null)
        {
            throw new SelectMapWindowException("content buttons map not seted");
        }

        if (pictogram == null)
        {
            throw new SelectMapWindowException("pictogram image map not seted");
        }

        if (textInfoMap == null)
        {
            throw new SelectMapWindowException("text info map not seted");
        }

        if (buttonSelectMap == null)
        {
            throw new SelectMapWindowException("button select map not seted");
        }

        if (buttonCancel == null)
        {
            throw new SelectMapWindowException("button cancel not seted");
        }



        selectMapButtonPrefab = Resources.Load<SelectMapButton>(PATH_PREFAB_SELECT_MAP_BUTTON);

        if (selectMapButtonPrefab == null)
        {
            throw new SelectMapWindowException("select map button prefab not found");
        }

        mapsData = Resources.LoadAll<MapData>(PATH_DATA_LIST_MAPS);

        if (mapsData == null || mapsData.Length == 0)
        {
            throw new SelectMapWindowException("map data list is emtry");
        }

        buttonSelectMap.onClick.AddListener(SelectScene);
        buttonCancel.onClick.AddListener(Exit);
    }

    private void SelectScene()
    {
        if (selectedMapData != null)
        {
        GameCacheManager.StartNewGameSession();
        Loading.LoadScene(selectedMapData.SceneName);
        }

    }

    private SelectMapButton CreateMapButton (MapData mapData)
    {
        SelectMapButton button = Instantiate(selectMapButtonPrefab, contentButtonsMap);
        button.SetMapData(mapData);
        button.onClick += SelectMap;
        return button;
    }

    private void SelectMap(MapData data)
    {
        selectedMapData = data;
        SetStateVisibleInfoMap(true);
        UpdateInfoMap();
    }

    private void UpdateInfoMap ()
    {
        textInfoMap.text = $"{selectedMapData.MapName}{PREFIX_NAME_MAP_STRING}{PREFIX_SIZE_MAP_STRING} {selectedMapData.Size}{PREFIX_DECRIPTION_MAP_STRING}{selectedMapData.Decription}";
        pictogram.sprite = selectedMapData.Icon;
    }

    private void SetStateVisibleInfoMap (bool status)
    {
        containerInfoMap.SetActive(status);
    }
}