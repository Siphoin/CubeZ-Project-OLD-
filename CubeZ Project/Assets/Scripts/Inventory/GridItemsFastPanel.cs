using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(GridLayoutGroup))]
public class GridItemsFastPanel : MonoBehaviour
    {
     GridLayoutGroup grid;

    private ItemCellMainCanvas itemCellMainCanvasPrefab;

    private const string PATH_ITEM_CELL_MAIN_CANVAS = "Prefabs/UI/itemCellMainCanvas";
        // Use this for initialization
        void Start()
        {
        grid = GetComponent<GridLayoutGroup>();
        if (grid == null)
        {
            throw new GridItemsFastPanelException("component Grid Layout Group is null");
        }
        itemCellMainCanvasPrefab = Resources.Load<ItemCellMainCanvas>(PATH_ITEM_CELL_MAIN_CANVAS);

        if (itemCellMainCanvasPrefab == null)
        {
            throw new GridItemsFastPanelException("prefab item cell main canvas not found");
        }
        GameCacheManager.gameCache.inventory.onListItemsChanged += LoadItems;
        }


    private void ClearItems ()
    {
        for (int i = 0; i < grid.transform.childCount; i++)
        {
            Destroy(grid.transform.GetChild(i).gameObject);
        }
    }

    private void LoadItems(List<ItemBaseData> list)
    {
        ClearItems();
        List<ItemBaseData> sortedList = list.Where(item => item.inFastPanel == true).ToList();
        for (int i = 0; i < sortedList.Count; i++)
        {
            CreateItem(sortedList[i]);
        }
    }

    private void CreateItem (ItemBaseData data)
    {
        if (data == null)
        {
            throw new GridItemsFastPanelException("data argument is null");
        }

        ItemCellMainCanvas newCell = Instantiate(itemCellMainCanvasPrefab, transform);
        newCell.SetData(data);


    }
}