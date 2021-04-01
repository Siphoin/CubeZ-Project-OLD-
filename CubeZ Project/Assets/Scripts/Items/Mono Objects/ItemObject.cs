using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class ItemObject : InteractionObjectContainerItems, IObjectAddingInventory
{
    [Header("Вращается ли объект")]
    [SerializeField] bool rotating = false;



    private const float SPEED_ROTATING = 110.0f;

    private Vector3 rotateVector = new Vector3(0, 1, 0);

    private const string TAG_PLAYER = "MyPlayer";
    private const string TRIGGER_NAME_KEY_CODE = "Interaction";
    private const string PATH_REQUEST_ID_LIST_RESOURCES = "Items/Resources/ResourcesIDList";
    

    [Header("Этот ресурс содержит предмет:")]
    [SerializeField] BaseItem item;

    private ItemBaseData dataItem;


    public TypeItem TypeItem { get => item.data.typeItem; }




    // Use this for initialization
    void Start()
    {
        if (SPEED_ROTATING < 0)
        {
            throw new ItemObjectException("speed rotating as < 0");
        }
        if (item == null)
        {
            throw new ItemObjectException("item is null");
        }

        if (ControlManagerObject.Manager == null)
        {
            throw new ItemObjectException("control manager not found!");
        }
        Ini();


#if UNITY_EDITOR
        CheckValidResourceItem();

#endif
        dataItem = new ItemBaseData(item.data);
        dataItem.GenerateId();

    }


    private void CheckValidResourceItem()
    {
        if (item.data.typeItem == TypeItem.Resource)
        {
            ResourcesIDList resourcesIDList = Resources.Load<ResourcesIDList>(PATH_REQUEST_ID_LIST_RESOURCES);
            if (resourcesIDList == null)
            {
                throw new ItemObjectException("Request id list resources not found");
            }
            ResourceItem resourceItem = Resources.Load<ResourceItem>($"{PATH_FOLBER_ITEMS_RESOURCES}{item.data.idItem}");

            if (resourceItem == null)
            {
                throw new ItemObjectException("resource item not found");
            }
            if (!resourcesIDList.IDResourceRequest(resourceItem.dataResource.idResource))
            {
                throw new ItemObjectException("resource item not request in id resource list");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (rotating)
        {
        transform.Rotate(rotateVector * SPEED_ROTATING * Time.deltaTime);
        }

        CheckDistanceOffsetPlayer();

    }



    private void OnCollisionEnter(Collision collision)
    {
        CheckCollision(collision);
    }

    private bool TryAddItemToInventory()
    {
        return GameCacheManager.gameCache.inventory.TryAdd(dataItem);
    }

    public void AddItemToInventory()
    {
        throw new System.NotImplementedException();
    }

    public override void CheckDistanceOffsetPlayer()
    {
        if (playerEntered && enteredPlayer != null)
        {
            float distance = Vector3.Distance(enteredPlayer.transform.position, transform.position);
            if (distance >= 1f)
            {
                ShowOrDestroyHintKeyCode(false);
            }

            if (Input.GetKeyDown(keyCodeGetItem) && TryAddItemToInventory())
            {
                Destroy(gameObject);
            }
        }
    }

}