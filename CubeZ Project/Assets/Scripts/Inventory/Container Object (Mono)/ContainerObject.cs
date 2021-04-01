using Boo.Lang;
using System;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class ContainerObject : InteractionObjectContainerItems
    {
    private ContainerItemsWindow activeContainerItemsWindow;
    private ContainerItemsWindow containerItemsWindowPrefab;

    private ContainerObjectsSettings containerObjectsSettings;

    private const string PATH_PREFAB_CONTAINER_WINDOW_ITEMS = "Prefabs/UI/ContainerItemsCanvas";
    private const string PATH_SETTINGS_CONTAINER_OBJECT = "Items/ContainerObjectSettings";
    private const string PATH_ITEMS_FOLBER = "Items/";
    private const string PREFIX_FOLBER_CATEGORY_ITEMS = "s";
  [SerializeField]  private TypeItem typeItem;

    BaseInventoryContainer containerItems = new BaseInventoryContainer();
    [Header("Генерировать только определенный тип предмета")]
    [SerializeField] bool useSpecialTypeItems = false;

    // Use this for initialization
    void Start()
        {
        Ini();

        containerObjectsSettings = Resources.Load<ContainerObjectsSettings>(PATH_SETTINGS_CONTAINER_OBJECT);

        if (containerObjectsSettings == null)
        {
            throw new ContainerObjectException("container object settings not found");
        }


        containerItemsWindowPrefab = Resources.Load<ContainerItemsWindow>(PATH_PREFAB_CONTAINER_WINDOW_ITEMS);

        if (containerItemsWindowPrefab == null)
        {
            throw new ContainerObjectException("container window items not found");
        }

        GenerateItems();



        }

        // Update is called once per frame
        void Update()
        {
        CheckDistanceOffsetPlayer();
        }

    private void OnCollisionEnter (Collision collision)
    {
        CheckCollision(collision);
    }

    public override void CheckDistanceOffsetPlayer()
    {
        if (playerEntered && enteredPlayer != null)
        {
            float distance = Vector3.Distance(enteredPlayer.transform.position, transform.position);
            if (distance >= 1f)
            {
                ShowOrDestroyHintKeyCode(false);

                if (activeContainerItemsWindow != null)
                {
                    Destroy(activeContainerItemsWindow.gameObject);
                }
            }

            if (Input.GetKeyDown(keyCodeGetItem))
            {
                if (activeContainerItemsWindow == null)
                {
                    activeContainerItemsWindow = Instantiate(containerItemsWindowPrefab);
                    activeContainerItemsWindow.SetContainer(containerItems);
                    DestroyHintKeyCode();
                }
            }
        }
    }

    private void GenerateItems ()
    {
        int countItems = Random.Range(0, containerObjectsSettings.MaxCountItemsOfContainer + 1);
        if (countItems <= 0)
        {
            return;
        }


        if (useSpecialTypeItems)
        {

           BaseInventoryContainer containerItemsTypes = ItemsManager.Manager.GetListItemsOfType(typeItem);
            for (int i = 0; i < countItems; i++)
            {
                containerItems.Add(containerItemsTypes.Get(Random.Range(0, containerItemsTypes.Length)));
            }
            Debug.Log($"New container object {name}: Count items: {countItems}: Type items: {typeItem}");
        }

        else
        {
            for (int i = 0; i < countItems; i++)
            {
                BaseItem item = ItemsManager.Manager.GetRandomItem();
                containerItems.Add(item.data);
            }

            Debug.Log($"New container object {name}: Count items: {countItems}");


        }
        

       

    }
}