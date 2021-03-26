using UnityEngine;
[CreateAssetMenu(menuName = "Inventory/Item/Resource", order = 3)]
public class ResourceItem : BaseItem
{
    [Header("Данные о ресурсе")]
    public ResourceParams dataResource = new ResourceParams();



    private void Awake()
    {
        data.typeItem = TypeItem.Resource;
    }

}