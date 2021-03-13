using System.Collections.Generic;

[System.Serializable]

public class InventoryContainer 
{
    [Newtonsoft.Json.JsonRequired]
    private List<ItemBaseData> items = new List<ItemBaseData>(0);

    public int Length { get => items.Count; }

    public InventoryContainer ()
    {
        items = new List<ItemBaseData>(0);
    }

    public InventoryContainer (InventoryContainer copyClass)
    {
        copyClass.CopyAll(this);
    }

    public void Add (ItemBaseData item)
    {
        items.Add(item);
    }

    public void Remove (ItemBaseData item)
    {
        if (!items.Contains(item))
        {
            throw new InventoryContainerException("Item not found in list items");
        }

        else
        {
            items.Remove(item);
        }
    }


    public void Remove(int index)
    {
        if (items[index] == null)
        {
            throw new InventoryContainerException("Item not found in list items");
        }

        else
        {
            items.Remove(items[index]);
        }
    }

    public void Clear ()
    {
        items.Clear();
    }

    public bool TryGet(int index, out ItemBaseData item)
    {
        if (index < 0 || index > items.Count)
        {
            item = null;
            return false;
        }
        if (items[index] == null)
        {
            item = null;
            return false;
        }

        item = items[index];
        return true;
    }

    public ItemBaseData Get (int index)
    {
        if (index < 0 || index > items.Count)
        {
            return null;
        }
        if (items[index] == null)
        {
            return null;
        }

        return items[index];
    }

    public bool Contains (ItemBaseData item)
    {
        return items.Contains(item);
    }

    
}