using UnityEngine;
[CreateAssetMenu(menuName = "Inventory/Item/Kit", order = 1)]
public class Kittem : BaseItem
{
    public KitParams dataKit = new KitParams();


    private void Awake()
    {
        data.typeItem = TypeItem.Kit;
    }

}