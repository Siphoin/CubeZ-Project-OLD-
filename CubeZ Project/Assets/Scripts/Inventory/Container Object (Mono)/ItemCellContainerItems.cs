using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(Image))]
public class ItemCellContainerItems : MonoBehaviour, IItemCell
    {




    private ItemBaseData currentData;

    [SerializeField] private Image icon;
   private Image thisImage;

    [SerializeField] private Color selectedColor = Color.yellow;

    Color transperentColor;

    public Color SelectedColor { get => selectedColor; }

    public event Action<ItemBaseData, int> onSelected;
    // Use this for initialization
    void Start()
    {
        Ini();

        IniTransperentColor();

    }

    private void Ini()
    {
        if (thisImage == null)
        {
        if (!TryGetComponent(out thisImage))
        {
            throw new ItemCellException("component image not found");
        }
        }

        if (icon == null)
        {
            throw new ItemCellException("icon is null");
        }

        if (UIController.Manager == null)
        {
            throw new ItemCellException("UI controller not found");
        }
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
        onSelected?.Invoke(currentData, transform.GetSiblingIndex());

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
    public void SetColor (Color color)
    {
        Ini();
        thisImage.color = color;
    }


    public bool CompareItemData (ItemBaseData b)
    {
        if (b == null)
        {
            throw new ItemCellException("data is null");
        }

        return b == currentData;
    }

    public bool CellSelected ()
    {
        Ini();
        return thisImage.color == selectedColor;
    }
}