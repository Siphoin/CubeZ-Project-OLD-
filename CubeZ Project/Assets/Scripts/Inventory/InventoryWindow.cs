using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryWindow : Window
{
    protected const string PATH_ITEMCELL_PREFAB = "Prefabs/UI/ItemCell";
    protected const string PATH_ITEMCELL_EMTRY_PREFAB = "Prefabs/UI/ItemCellEmtry";
    protected const string PATH_CHARACTER_INVENTORY_SETTINGS = "Character/InventoryPlayerSettings";
    protected const string PATH_ITEMS_DATA = "Items/";

    private const string NAME_FOLBER_PATH_EAT_AUDIO = "Audio/character/character_eat";

    [Header("Окно характеристик предмета")]
    [SerializeField] protected GameObject infoItem;

    [Header("Текст описания предмета")]
    [SerializeField] protected TextMeshProUGUI textDescriptionItem;

    [Header("Текст имени предмета")]
    [SerializeField] protected TextMeshProUGUI textNameItem;

    [Header("Иконка предмета в информации предмета")]
    [SerializeField] protected Image icoInfoItem;

    [Header("Грид предметов")]
    [SerializeField] protected GridLayoutGroup gridItems;

    [Header("Грид пустых ячеек")]
    [SerializeField] protected GridLayoutGroup gridItemsEmtry;

    protected  ItemCell itemCellPrefab;
    protected ItemCellEmtry itemCellEmtryPrefab;

    private AudioClip eatCharacterClip;


  

    protected InventoryPlayerSettings inventoryPlayerSettings;

    [Header("Грид ячеек быстрого доступа")]

    [SerializeField] VerticalLayoutGroup gridsItemsFastPanels;


    protected ItemBaseData currentItemData = null;

    // Use this for initialization
    void Awake()
    {
        IniInventory();
    }

    public virtual void IniInventory()
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
        if (gridsItemsFastPanels == null)
        {
            throw new InventoryWindowException("grid items fast panel is null");
        }

        if (gridItemsEmtry == null)
        {
            throw new InventoryWindowException("grid items emtry is null");
        }


        eatCharacterClip = Resources.Load<AudioClip>(NAME_FOLBER_PATH_EAT_AUDIO);

        if (eatCharacterClip == null)
        {
            throw new InventoryWindowException("clip eat character not found");
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

    // Update is called once per frame
    void Update()
    {

    }

    protected void SetStateInfoItem(bool state)
    {
        infoItem.SetActive(state);
    }

    public virtual void LoadItems()
    {
        ClearInventoryWindow();
        List<ItemBaseData> items = GameCacheManager.gameCache.inventory.GetItemsWithTypeIgnore(TypeItem.Resource);
        
        for (int i = 0; i < items.Count; i++)
        {
            CreateItemCell(items[i]);
        }
    }

    protected void LoadEmtryCells()
    {
        for (int i = 0; i < inventoryPlayerSettings.data.maxCountItems; i++)
        {
            CreateEmtryCell();
        }
    }

    protected void CreateItemCell(ItemBaseData data)
    {
        if (data == null)
        {
            throw new ArgumentNullException("data item is null");
        }

        ItemCell newItemCell = Instantiate(itemCellPrefab, gridItems.transform);
        newItemCell.SetData(data);
        newItemCell.onClick += ShowInfoItem;

        if (data.inFastPanel)
        {
            RectTransform targetRam = gridsItemsFastPanels.transform.GetChild(data.indexFastPanel).GetComponent<RectTransform>();
            newItemCell.SetToFastPanel(targetRam);
        }
    }

    private void CreateEmtryCell()
    {
        Instantiate(itemCellEmtryPrefab, gridItemsEmtry.transform);
    }

    private void ShowInfoItem(ItemBaseData data)
    {
        currentItemData = data;
        SetStateInfoItem(true);
        textDescriptionItem.text = data.itemDecription;
        textNameItem.text = data.ItemName;
        icoInfoItem.sprite = data.icon;
    }

    public void RemoveItem()
    {
        if (PlayerManager.Manager.Player.CurrentWeapon != null && currentItemData == PlayerManager.Manager.Player.CurrentWeapon.data)
        {
        RemoveActiveWeaponPlayer();
        }

        GameCacheManager.gameCache.inventory.Remove(currentItemData);
        LoadItems();
    }

    private void RemoveActiveWeaponPlayer()
    {
        Character currentPlayer = PlayerManager.Manager.Player;
        if (currentPlayer.CurrentWeapon != null)
        {
            if (currentPlayer.CurrentWeapon.data.id == currentItemData.id)
            {
                currentPlayer.ReturnToBaseDamage();
                currentPlayer.SetWeapon(null);
            }
        }
    }

    protected void ClearInventoryWindow()
    {
        SetStateInfoItem(false);
        for (int i = 0; i < gridItems.transform.childCount; i++)
        {
            Destroy(gridItems.transform.GetChild(i).gameObject);
        }
        if (gridsItemsFastPanels != null)
        {
            ClearFastPanel();
        }

    }

    private void ClearFastPanel()
    {
        for (int i = 0; i < gridsItemsFastPanels.transform.childCount; i++)
        {
            Transform rectTransformCell = gridsItemsFastPanels.transform.GetChild(i);
            for (int j = 0; j < rectTransformCell.childCount; j++)
            {
                Destroy(rectTransformCell.GetChild(j).gameObject);
            }
        }
    }

    public void SetTargetItem(ItemBaseData target)
    {
        ShowInfoItem(target);
    }
    #region Use Mechanim
    public void UseItem()
    {
        switch (currentItemData.typeItem)
        {
            case TypeItem.Food:
                UseFood();
                break;
            case TypeItem.Kit:
                UseKit();
                break;
            case TypeItem.Weapon:
                UseWeapon();
                break;
            case TypeItem.SyringeAdrenalin:
                UseSyringeAdrenalin();
                break;
            default:
                throw new InventoryWindowException($"invalid type use: You must set case actions for type {currentItemData.typeItem}");
        }
    }

    private void UseFood()
    {
        if (PlayerManager.Manager == null)
        {
            throw new InventoryWindowException("player manager not found");
        }

        if (PlayerManager.Manager.Player == null)
        {
            throw new InventoryWindowException("player not found");
        }

        CharacterStatsDataNeed statsHunger = PlayerManager.Manager.PlayerStats.Hunger;
        if (statsHunger.value >= statsHunger.GetDefaultValue())
        {
            return;
        }
        string path = GetPathToItem(currentItemData.typeItem, currentItemData.idItem);
        FoodParams foodItem = Resources.Load<FoodItem>(path).dataFood;
        AddPlayerValueStatsToNeed(currentItemData.typeItem, foodItem.satietyRange);
        RemoveItem();

        if (AudioDataManager.Manager == null)
        {
                throw new InventoryWindowException("audio manager not found");
            
        }

        AudioObject audioObject = AudioDataManager.Manager.CreateAudioObject(PlayerManager.Manager.Player.transform.position, eatCharacterClip);
        audioObject.GetAudioSource().Play();
        audioObject.RemoveIFNotPlaying();
    }

    private void UseKit()
    {
        CharacterStatsDataNeed statsHealth = PlayerManager.Manager.PlayerStats.Health;
        if (statsHealth.value >= statsHealth.GetDefaultValue())
        {
            return;
        }
        string path = GetPathToItem(currentItemData.typeItem, currentItemData.idItem);
        KitParams kittem = Resources.Load<Kittem>(path).dataKit;
        AddPlayerValueStatsToNeed(currentItemData.typeItem, kittem.regenRange);
        RemoveItem();
    }

    private void UseSyringeAdrenalin()
    {
        if (PlayerManager.Manager.Player.IsAdrenalin)
        {
            return;
        }


        string path = GetPathToItem(currentItemData.typeItem, currentItemData.idItem);
        SyringeAdrenalinParams syringeAdrenalinParams = Resources.Load<SyringeAdrenalinItem>(path).dataSyringleAdrenalin;
        PlayerManager.Manager.Player.BuffAdrenalin(syringeAdrenalinParams);
        RemoveItem();
    }

    private void UseWeapon()
    {
        string path = GetPathToItem(currentItemData.typeItem, currentItemData.idItem);
        WeaponItem weaponItem = Resources.Load<WeaponItem>(path);
        Character currentPlayer = PlayerManager.Manager.Player;
        weaponItem = new WeaponItem(weaponItem.dataWeapon.damageBonus, weaponItem.dataWeapon.strength);
        weaponItem.data = currentItemData;
        if (currentPlayer.CurrentWeapon == null)
        {
            currentPlayer.SetWeapon(weaponItem);
            currentPlayer.IncrementDamage(weaponItem.dataWeapon.damageBonus);
            return;
        }
        if (currentPlayer.CurrentWeapon.data.ItemName == currentItemData.ItemName)
        {
            return;
        }

        else
        {
            currentPlayer.SetWeapon(weaponItem);
            currentPlayer.IncrementDamage(weaponItem.dataWeapon.damageBonus);
        }




    }

    private void AddPlayerValueStatsToNeed(TypeItem typeItem, int value)
    {
        NeedCharacterType needCharacterType = NeedCharacterType.Eat;

        switch (typeItem)
        {
            case TypeItem.Food:
                needCharacterType = NeedCharacterType.Eat;
                break;
            case TypeItem.Kit:
                needCharacterType = NeedCharacterType.Health;
                break;
            default:
                break;
        }

        PlayerManager.Manager.PlayerStats.AddValueToNeed(needCharacterType, value);
    }


    private string GetPathToItem(TypeItem typeItem, string id)
    {
        return $"{PATH_ITEMS_DATA}{typeItem}s/{id}";
    }




    #endregion
}