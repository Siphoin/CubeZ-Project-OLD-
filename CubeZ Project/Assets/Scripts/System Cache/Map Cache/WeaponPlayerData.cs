[System.Serializable]
public class WeaponPlayerData
    {
    public string idWeapon;
    public WeaponParams paramsWeapon;
    public int indexItem;

    public WeaponPlayerData ()
    {

    }

    public WeaponPlayerData (WeaponItem weaponItem, int index)
    {
        idWeapon = weaponItem.data.idItem;
        paramsWeapon = weaponItem.dataWeapon;
        indexItem = index;
    }

    }