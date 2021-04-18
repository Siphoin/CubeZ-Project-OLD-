using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]

public class InventoryContainerPlayer : BaseInventoryContainer
{
    private InventoryPlayerSettingsData settingsData;
    public event Action<List<ItemBaseData>> onListItemsChanged;
    public event Action<string> onItemOfTypeAdded;

    public InventoryContainerPlayer(InventoryContainerPlayer copyClass, InventoryPlayerSettingsData settingsData)
    {
        copyClass.CopyAll(this);
        this.settingsData = settingsData;
    }

    public InventoryContainerPlayer(InventoryContainerPlayer copyClass)
    {
        copyClass.CopyAll(this);
    }

    public InventoryContainerPlayer()
    {

    }

    public InventoryContainerPlayer(InventoryPlayerSettingsData settingsData)
    {
        this.settingsData = settingsData;
    }

    public override void Clear()
    {
        base.Clear();
        CallEventChanges();
    }

    public override void Remove(int index)
    {
        base.Remove(index);
        CallEventChanges();
    }

    public override void Add(ItemBaseData item)
    {
        base.Add(item);
        CallEventChanges();
        onItemOfTypeAdded?.Invoke(items[Length - 1].idItem);

    }

    public override void Remove(ItemBaseData item)
    {
        base.Remove(item);
        CallEventChanges();
    }

    public override void RemoveOf(int count)
    {
        base.RemoveOf(count);
        CallEventChanges();
    }

    private void CallEventChanges()
    {
        onListItemsChanged?.Invoke(items);
    }

    public bool TryAdd(ItemBaseData data, out ItemBaseData output)
    {
        if (settingsData == null)
        {
            throw new InventoryContainerException("settings inventory player not seted");
        }

        if (items.Count <= settingsData.maxCountItems)
        {
            Debug.Log($"Inventory player message: limit items. Max count items as {settingsData.maxCountItems}");
            output = null;
            return false;
        }
        Add(data);
        output = items[items.Count - 1];
        return true;
    }

    public bool TryAdd(ItemBaseData data)
    {
        if (settingsData == null)
        {
            throw new InventoryContainerException("settings inventory player not seted");
        }

        if (items.Count >= settingsData.maxCountItems)
        {
            Debug.Log($"Inventory player message: limit items. Max count items as {settingsData.maxCountItems}");
            return false;
        }
        Add(data);
        return true;
    }

    public void CallEventMarkingItem()
    {
        CallEventChanges();

    }

    public bool TryRemoveItemsWithCountOfType (string typeId, int count)
    {
        if (Length < count)
        {
            return false;
        }


        List<ItemBaseData> whereList = items.Where(item => item.idItem == typeId).ToList();


        if (count > whereList.Count)
        {
            return false;
        }

        List<ItemBaseData> oldList = items.Where(item => item.idItem != typeId).ToList();


        whereList.RemoveRange(0, count);
        whereList.Concat(items);


        items = whereList;

        for (int i = 0; i < oldList.Count; i++)
        {
            Add(oldList[i]);
        }


        return true;
    }

    public int CountItemOfID (string id)
    {
        return items.Count(a => a.idItem == id);
    }

   public bool NotEnoughValueTypeItem (BuildObjectData buildObjectData)
    {
        for (int i = 0; i < buildObjectData.RequirementsResources.Length; i++)
        {
            int countItemsExits = items.Count(item => item.idItem == buildObjectData.RequirementsResources[i].typeResource.data.idItem);

            if (countItemsExits < buildObjectData.RequirementsResources[i].requirementsValue)
            {
                return true;
            }
        }

        return false;
    }

    private void Ini ()
    {

    }
}