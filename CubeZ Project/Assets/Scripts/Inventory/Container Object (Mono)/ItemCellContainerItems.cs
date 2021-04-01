using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ItemCellContainerItems : MonoBehaviour, IItemCell
    {




    private ItemBaseData currentData;

    [SerializeField] private Image icon;

    Color transperentColor;

    public event Action<ItemBaseData> onSelected;
    // Use this for initialization
    void Start()
    {
        if (icon == null)
        {
            throw new ItemCellException("icon is null");
        }

        if (UIController.Manager == null)
        {
            throw new ItemCellException("UI controller not found");
        }

        IniTransperentColor();

    }

    private void IniTransperentColor()
    {
        var color = Color.white;
        color.a = 0;
        transperentColor = color;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Select()
    {
        if (currentData == null)
        {
            throw new ItemCellException("data is null");
        }
        onSelected?.Invoke(currentData);
        Debug.Log(323);

    }

    public void LoadIcon()
    {
        Sprite iconSprite = currentData.icon;
        if (iconSprite == null)
        {
            icon.color = transperentColor;
            throw new ItemCellException("sprite icon is null");
        }
        icon.sprite = iconSprite;
    }

    public void SetData(ItemBaseData data)
    {
        if (data == null)
        {
            throw new ItemCellException("data is null");
        }

        currentData = data;
        LoadIcon();
    }
}