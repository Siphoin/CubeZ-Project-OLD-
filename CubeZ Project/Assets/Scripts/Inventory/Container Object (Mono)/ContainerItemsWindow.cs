using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ContainerItemsWindow : Window
{
    BaseInventoryContainer itemsContainer = new BaseInventoryContainer();
    private ItemCellContainerItems itemCellContainerItemsPrefab;

    private ItemBaseData currentSelectedItem = null;

    private List<ItemCellContainerItems> cells = new List<ItemCellContainerItems>(0);


    [SerializeField] private GridLayoutGroup gridItems;
    [SerializeField] private Button buttonGet;
    [SerializeField] private Button buttonGetAllItems;

    private static ContainerItemsWindow activeContainerItemsWindow = null;


    private const string PATH_PREFAB_ITEM_CELL_CONTAINER_ITEMS = "Prefabs/UI/ItemCellContainerItems";
    // Use this for initialization
    void Start()
    {
        Ini();


        if (activeContainerItemsWindow != null)
        {
            activeContainerItemsWindow.Exit();
        }

        activeContainerItemsWindow = this;


        if (gridItems == null)
        {
            throw new ContainerItemsWindowException("Grid items not seted");
        }

        if (buttonGet == null)
        {
            throw new ContainerItemsWindowException("button get item not seted");
        }

        if (buttonGetAllItems == null)
        {
            throw new ContainerItemsWindowException("button get all items not seted");
        }

        itemCellContainerItemsPrefab = Resources.Load<ItemCellContainerItems>(PATH_PREFAB_ITEM_CELL_CONTAINER_ITEMS);


        if (itemCellContainerItemsPrefab == null)
        {
            throw new ContainerItemsWindowException("preffab item cell container items not found");
        }
        SetStateInterableButtonGet(false);

        LoadItems();

        buttonGet.onClick.AddListener(GetItem);
        buttonGetAllItems.onClick.AddListener(GetAllItems);
    }

    private void SetStateInterableButtonGet(bool state)
    {
        buttonGet.interactable = state;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetContainer(BaseInventoryContainer container)
    {
        if (container == null)
        {
            throw new ContainerItemsWindowException("container items argument is null");
        }

        itemsContainer = container;
    }

    private void CreateItemCell(ItemBaseData data)
    {
        ItemCellContainerItems newItemCell = Instantiate(itemCellContainerItemsPrefab, gridItems.transform);
        newItemCell.SetData(data);
        newItemCell.onSelected += ItemSelected;
        cells.Add(newItemCell);
    }

    private void LoadItems ()
    {
        if (itemsContainer.Length < 1)
        {
            SetStateInterableButtonGet(false);
            
        }
        for (int i = 0; i < itemsContainer.Length; i++)
        {
            ItemBaseData data;
            itemsContainer.TryGet(i, out data);
            CreateItemCell(data);
        }
    }

    private void ItemSelected(ItemBaseData data, int index)
    {
        SetStateInterableButtonGet(true);
        currentSelectedItem = data;
        MarkingItems(index);
    }

    private void MarkingItems(int index)
    {
        for (int i = 0; i < cells.Count; i++)
        {
            ItemCellContainerItems cell = cells[i];
            cell.SetColor(cell.CompareItemData(currentSelectedItem) == true && index == cell.transform.GetSiblingIndex() ? cell.SelectedColor : Color.white);
        }
    }

    private void GetItem ()
    {
        if (GameCacheManager.gameCache.inventory.TryAdd(currentSelectedItem))
        {
            ItemCellContainerItems cellRemoving = cells.Single(item => item.CellSelected());
            cells.Remove(cellRemoving);
            itemsContainer.Remove(currentSelectedItem);
            currentSelectedItem = itemsContainer.Get(itemsContainer.Length - 1);
            RefreshListItems();
            if (cells.Count > 0)
            {
                ItemCellContainerItems cellNext = cells[cells.Count - 1];
            cellNext.SetColor(cellNext.SelectedColor);
            }


        }


    }

    private void GetAllItems ()
    {
        int j = 0;
        for (int i = 0; i < itemsContainer.Length; i++)
        {
            if (GameCacheManager.gameCache.inventory.TryAdd(itemsContainer.Get(i)))
            {
                j++;

            }

        }
       
        itemsContainer.RemoveOf(j);
        RefreshListItems();
    }

    private void ClearItemsOnGrid ()
    {
        for (int i = 0; i < gridItems.transform.childCount; i++)
        {
            GameObject obj = gridItems.transform.GetChild(i).gameObject;
            Destroy(obj);
        }
        cells.Clear();
    }

    private void RefreshListItems ()
    {
        ClearItemsOnGrid();
        LoadItems();
    }
}