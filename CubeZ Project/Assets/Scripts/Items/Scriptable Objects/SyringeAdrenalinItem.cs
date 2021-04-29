using UnityEngine;
[CreateAssetMenu(menuName = "Inventory/Item/Syringe Adrenalin Item", order = 4)]
public class SyringeAdrenalinItem : BaseItem
    {
    [Header("Данные о шприце адреналина")]
    public SyringeAdrenalinParams dataSyringleAdrenalin = new SyringeAdrenalinParams();

    private void Awake()
    {
        data.typeItem = TypeItem.SyringeAdrenalin;
    }

}