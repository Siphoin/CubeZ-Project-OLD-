using System.Collections;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
    public class ItemObject : MonoBehaviour, IObjectAddingInventory
    {
    [Header("Вращается ли объект")]
    [SerializeField] bool rotating = false;
    private const float SPEED_ROTATING = 110.0f;

    private Vector3 rotateVector = new Vector3(0, 1, 0);

    private const string TAG_PLAYER = "Player";
    [Header("Этот ресурс содержит предмет:")]
    [SerializeField] BaseItem item;

    private ItemBaseData dataItem;


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
       dataItem = new ItemBaseData(item.data);
        }

        // Update is called once per frame
        void Update()
        {
        if (!rotating)
        {
            return;
        }
        transform.Rotate(rotateVector * SPEED_ROTATING * Time.deltaTime);
    }



    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == TAG_PLAYER)
        {
            if (TryAddItemToInventory())
            {
                Destroy(gameObject);
            }
        }
    }

    private bool TryAddItemToInventory()
    {
      return  GameCacheManager.gameCache.inventory.TryAdd(dataItem);
    }

    public void AddItemToInventory()
    {
        throw new System.NotImplementedException();
    }
}