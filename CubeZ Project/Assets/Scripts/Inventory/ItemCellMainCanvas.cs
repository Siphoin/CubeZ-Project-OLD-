using UnityEngine;
using UnityEngine.UI;

public class ItemCellMainCanvas : MonoBehaviour, IItemCell
{
    [SerializeField] Window windowInventory;

    private UIController uIController;


    private ItemBaseData currentData;

    [SerializeField] private Image icon;

    Color transperentColor;
    // Use this for initialization
    void Start()
    {
        if (icon == null)
        {
            throw new ItemCellException("icon is null");
        }
        if (windowInventory == null)
        {
            throw new ItemCellException("window inventory not seted to item cell canvas");
        }

        if (UIController.Manager == null)
        {
            throw new ItemCellException("UI controller not found");
        }

        uIController = UIController.Manager;
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


        if (!uIController.ContainsWindow(windowInventory.name))
        {
            InventoryWindow inventoryWindow = (InventoryWindow)uIController.OpenWindow(windowInventory);
            inventoryWindow.SetTargetItem(currentData);
        }
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