using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class InventoryWindow : Window
    {
    private const string PATH_ITEMCELL_PREFAB = "Prefabs/UI/ItemCell";

       [Header("Окно характеристик предмета")]
        [SerializeField] GameObject infoItem;

    [Header("Текст описания предмета")]
    [SerializeField] TextMeshProUGUI textDescriptionItem;

    [Header("Текст имени предмета")]
    [SerializeField] TextMeshProUGUI textNameItem;

    [Header("Иконка предмета в информации предмета")]
    [SerializeField] Image icoInfoItem;

    [Header("Грид предметов")]
    [SerializeField] GridLayoutGroup gridItems;

 [SerializeField, ReadOnlyField]   private ItemCell itemCellPrefab;

    [Header("Грид ячеек быстрого доступа")]

    [SerializeField] VerticalLayoutGroup gridsItemsFastPanels;


    private ItemBaseData currentItemData = null;

    // Use this for initialization
    void Start()
        {
        FrezzePlayer();
        if (infoItem == null)
        {
            throw new InventoryWindowException("info item object is null");
        }

        if (textNameItem == null)
        {
            throw new InventoryWindowException("text name item info is null");
        }

        if (textDescriptionItem == null)
        {
            throw new InventoryWindowException("info text description item is null");
        }

        if (icoInfoItem == null)
        {
            throw new InventoryWindowException("info ico item is null");
        }

        if (gridItems == null)
        {
            throw new InventoryWindowException("grid items is null");
        }
        if (gridsItemsFastPanels == null)
        {
            throw new InventoryWindowException("grid items fast panel is null");
        }


        itemCellPrefab = Resources.Load<ItemCell>(PATH_ITEMCELL_PREFAB);

        if (itemCellPrefab == null)
        {
            throw new InventoryWindowException($"item cell prefab not found. Path: {PATH_ITEMCELL_PREFAB}");
        }


        SetStateInfoItem(false);

        LoadItems();
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void SetStateInfoItem (bool state)
        {
            infoItem.SetActive(state);
        }

    private void LoadItems ()
    {
        ClearInventoryWindow();
        for (int i = 0; i < GameCacheManager.gameCache.inventory.Length; i++)
        {
            ItemBaseData targetData = null;
            if (GameCacheManager.gameCache.inventory.TryGet(i, out targetData))
            {
                CreateItemCell(targetData);
            }
        }
    }

    private void CreateItemCell (ItemBaseData data)
    {
        if (data == null)
        {
            throw new ArgumentNullException("data item is null");
        }

        ItemCell newItemCell = Instantiate(itemCellPrefab, gridItems.transform);
        newItemCell.SetData(data);
        newItemCell.onClick += ShowInfoItem;
    }

    private void ShowInfoItem(ItemBaseData data)
    {
        currentItemData = data;
        SetStateInfoItem(true);
        textDescriptionItem.text = data.itemDecription;
        textNameItem.text = data.ItemName;
        icoInfoItem.sprite = data.icon;
    }

    public void RemoveItem ()
    {
        GameCacheManager.gameCache.inventory.Remove(currentItemData);
        LoadItems();
    }

    private void ClearInventoryWindow ()
    {
        SetStateInfoItem(false);
        for (int i = 0; i < gridItems.transform.childCount; i++)
        {
            Destroy(gridItems.transform.GetChild(i).gameObject);
        }

        for (int i = 0; i < gridsItemsFastPanels.transform.childCount; i++)
        {
            Transform rectTransformCell = gridsItemsFastPanels.transform.GetChild(i);
            for (int j = 0; j < rectTransformCell.childCount; j++)
            {
                Destroy(rectTransformCell.GetChild(j).gameObject);
            }
        }
    }
}