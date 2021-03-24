using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ContainerItemsWindow : Window
{
    BaseInventoryContainer itemsContainer = new BaseInventoryContainer();
    private ItemCellContainerItems itemCellContainerItemsPrefab;

    private ItemBaseData currentSelectedItem = null;


    [SerializeField] private GridLayoutGroup gridItems;
    [SerializeField] private Button buttonGet;


    private const string PATH_PREFAB_ITEM_CELL_CONTAINER_ITEMS = "Prefabs/UI/ItemCellContainerItems";
    // Use this for initialization
    void Start()
    {

        if (gridItems == null)
        {
            throw new ContainerItemsWindowException("Grid items not seted");
        }

        if (buttonGet == null)
        {
            throw new ContainerItemsWindowException("button get item not seted");
        }

        itemCellContainerItemsPrefab = Resources.Load<ItemCellContainerItems>(PATH_PREFAB_ITEM_CELL_CONTAINER_ITEMS);


        if (itemCellContainerItemsPrefab == null)
        {
            throw new ContainerItemsWindowException("preffab item cell container items not found");
        }
        SetStateInterableButtonGet(false);

        LoadItems();

        buttonGet.onClick.AddListener(GetItem);
        FrezzePlayer();
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
    }

    private void LoadItems ()
    {
        for (int i = 0; i < itemsContainer.Length; i++)
        {
            ItemBaseData data;
            itemsContainer.TryGet(i, out data);
            CreateItemCell(data);
        }
    }

    private void ItemSelected(ItemBaseData data)
    {
        SetStateInterableButtonGet(true);
        currentSelectedItem = data;
    }

    private void GetItem ()
    {
        if (GameCacheManager.gameCache.inventory.TryAdd(currentSelectedItem))
        {
        itemsContainer.Remove(currentSelectedItem);
            RefreshListItems();
        }


    }

    private void ClearItemsOnGrid ()
    {
        for (int i = 0; i < gridItems.transform.childCount; i++)
        {
            GameObject obj = gridItems.transform.GetChild(i).gameObject;
            Destroy(obj);
        }
    }

    private void RefreshListItems ()
    {
        ClearItemsOnGrid();
        LoadItems();
    }
}