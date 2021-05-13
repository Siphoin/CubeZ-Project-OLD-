using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectMapButton : MonoBehaviour
    {
    [Header("Текст названия карты")]
    [SerializeField] private TextMeshProUGUI textNameMap;

    private Button button;

    private MapData currentMapData;

    public event Action<MapData> onClick;

        // Use this for initialization
        void Start()
    {
        Ini();
    }

    private void Ini()
    {
        if (textNameMap == null)
        {
            throw new SelectMapButtonException("text name map not seted");
        }

        if (button == null)
        {
            if (!TryGetComponent(out button))
            {
                throw new SelectMapButtonException($"{name} not have component Button");
            }

            button.onClick.AddListener(Select);
        }
    }

    private void Select ()
    {
        onClick?.Invoke(currentMapData);
    }

    private void SetText ()
    {
        textNameMap.text = currentMapData.MapName;
    }

    public void SetMapData (MapData mapData)
    {
        if (mapData == null)
        {
            throw new SelectMapButtonException("map data is null");
        }
        Ini();
        currentMapData = mapData;

        SetText();
    }
}