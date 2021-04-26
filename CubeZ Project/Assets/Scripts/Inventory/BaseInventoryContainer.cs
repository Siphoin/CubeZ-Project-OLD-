using System.Collections.Generic;
using System.Linq;
using Newtonsoft;
[System.Serializable]
public class BaseInventoryContainer
{
    [Newtonsoft.Json.JsonIgnore]

    protected List<ItemBaseData> items = new List<ItemBaseData>(0);
    [Newtonsoft.Json.JsonIgnore]

    public int Length { get => items.Count; }


    [Newtonsoft.Json.JsonRequired]

    [Newtonsoft.Json.JsonProperty("items")]


    private Dictionary<long, string> itemsIDList = new Dictionary<long, string>();

    public BaseInventoryContainer()
    {
        items = new List<ItemBaseData>(0);
    }


    public BaseInventoryContainer(List<ItemBaseData> list)
    {
        items = list;
    }

    public virtual void Add(ItemBaseData item)
    {
        item.inFastPanel = false;
        items.Add(item);


        if (itemsIDList.ContainsKey(Length))
        {
            itemsIDList.Remove(Length);
        }
        itemsIDList.Add(Length, item.idItem);
    }

    public virtual void Remove(ItemBaseData item)
    {
        
        if (!items.Contains(item))
        {
            throw new InventoryContainerException("Item not found in list items");
        }

        else
        {
            itemsIDList.Remove(items.FindIndex(i => i == item));
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
            itemsIDList.Remove(index);
            items.Remove(items.ElementAt(index));
        }
    }

    public virtual void RemoveOf(int count)
    {
        items.RemoveRange(0, count);
        
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

    public virtual int GetIndexByItem (ItemBaseData item)
    {
        return items.FindIndex(i => i == item);
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

    public void Copy(BaseInventoryContainer targetContainer)
    {
        if (targetContainer == this)
        {
            throw new InventoryContainerException("target container equals this container");
        }
        for (int i = 0; i < Length; i++)
        {
            targetContainer.Add(items[i]);
        }
    }



    public virtual bool Contains(ItemBaseData item)
    {
        return items.Contains(item);
    }

   public List<ItemBaseData> GetItemsWithTypeIgnore (TypeItem typeItem)
    {
        return items.Where(item => item.typeItem != typeItem).ToList();
    }

    public List<ItemBaseData> GetItemsWithType(TypeItem typeItem)
    {
        return items.Where(item => item.typeItem == typeItem).ToList();
    }
}