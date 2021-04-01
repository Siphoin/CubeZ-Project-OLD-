using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class ItemsManager : MonoBehaviour
    {
    private static ItemsManager manager = null;
    private const string PATH_ITEMS_FOLBER = "Items/";
    private const string PREFIX_FOLBER_CATEGORY_ITEMS = "s";
    List<BaseItem> allItemsOfTheGame = new List<BaseItem>(0);

    public static ItemsManager Manager { get => manager; }

    // Use this for initialization
    void Awake()
        {
        if (manager == null)
        {
            manager = this;
            DontDestroyOnLoad(gameObject);

            Ini();
        }

        else
        {
            Destroy(gameObject);
        }
    }

    private  void Ini()
    {
        Array enumsValues = Enum.GetValues(typeof(TypeItem)).Cast<TypeItem>().ToArray();
        for (int i = 0; i < enumsValues.Length; i++)
        {
            BaseItem[] categoryItems = Resources.LoadAll<BaseItem>(PATH_ITEMS_FOLBER + enumsValues.GetValue(i).ToString() + PREFIX_FOLBER_CATEGORY_ITEMS);
            for (int j = 0; j < categoryItems.Length; j++)
            {
                allItemsOfTheGame.Add(categoryItems[j]);
            }
        }

        Debug.Log($"Loaded {allItemsOfTheGame.Count} items");
    }

    // Update is called once per frame
    void Update()
        {

        }

    public BaseItem GetRandomItem ()
    {
        int index = Random.Range(0, allItemsOfTheGame.Count);
        BaseItem item = new BaseItem(allItemsOfTheGame[index]);
        return item;
    }

    public BaseInventoryContainer GetListItemsOfType (TypeItem typeItem)
    {
        BaseItem[] orderedItems = allItemsOfTheGame.Where(item => item.data.typeItem == typeItem).ToArray();
        List<ItemBaseData> listData = new List<ItemBaseData>();
        for (int i = 0; i < orderedItems.Length; i++)
        {
            listData.Add(orderedItems[i].data);
        }

        return new BaseInventoryContainer(listData);
    }
    }