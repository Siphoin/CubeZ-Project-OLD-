using System.Collections.Generic;
[System.Serializable]
public class BaseInventoryContainer
{
    [Newtonsoft.Json.JsonRequired]
    protected List<ItemBaseData> items = new List<ItemBaseData>(0);

    public int Length { get => items.Count; }

    public BaseInventoryContainer()
    {
        items = new List<ItemBaseData>(0);
    }

    public BaseInventoryContainer(BaseInventoryContainer copyClass)
    {
        copyClass.CopyAll(this);
    }

    public virtual void Add(ItemBaseData item)
    {
        item.inFastPanel = false;
        items.Add(item);
    }

    public virtual void Remove(ItemBaseData item)
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


    public virtual void Remove(int index)
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

    public virtual void Clear()
    {
        items.Clear();
    }

    public virtual bool TryGet(int index, out ItemBaseData item)
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

    public virtual ItemBaseData Get(int index)
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

    public virtual bool Contains(ItemBaseData item)
    {
        return items.Contains(item);
    }
}