using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
    public class ResourcesWindow : InventoryWindow
    {

    // Use this for initialization
    void Awake()
        {
        Ini();
        IniInventory();
        }

        // Update is called once per frame
        void Update()
        {

        }

    public override void IniInventory()
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

        if (gridItemsEmtry == null)
        {
            throw new InventoryWindowException("grid items emtry is null");
        }


        itemCellPrefab = Resources.Load<ItemCell>(PATH_ITEMCELL_PREFAB);

        if (itemCellPrefab == null)
        {
            throw new InventoryWindowException($"item cell prefab not found. Path: {PATH_ITEMCELL_PREFAB}");
        }

        itemCellEmtryPrefab = Resources.Load<ItemCellEmtry>(PATH_ITEMCELL_EMTRY_PREFAB);

        if (itemCellEmtryPrefab == null)
        {
            throw new InventoryWindowException($"item cell emtry prefab not found. Path: {PATH_ITEMCELL_EMTRY_PREFAB}");
        }

        inventoryPlayerSettings = Resources.Load<InventoryPlayerSettings>(PATH_CHARACTER_INVENTORY_SETTINGS);

        if (inventoryPlayerSettings == null)
        {
            throw new InventoryWindowException($"inventory player settings not found. Path: {PATH_CHARACTER_INVENTORY_SETTINGS}");
        }


        SetStateInfoItem(false);
        LoadEmtryCells();
        LoadItems();
    }

    public override void LoadItems()
    {
        ClearInventoryWindow();
        List<ItemBaseData> items = GameCacheManager.gameCache.inventory.GetItemsWithType(TypeItem.Resource);

        for (int i = 0; i < items.Count; i++)
        {
            CreateItemCell(items[i]);
        }
    }
}