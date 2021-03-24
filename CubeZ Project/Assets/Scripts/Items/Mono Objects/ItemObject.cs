using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class ItemObject : MonoBehaviour, IObjectAddingInventory, IHintKeyCodeDisplay
{
    [Header("Вращается ли объект")]
    [SerializeField] bool rotating = false;

    private bool playerEntered = false;


    private const float SPEED_ROTATING = 110.0f;

    private Vector3 rotateVector = new Vector3(0, 1, 0);

    private const string TAG_PLAYER = "MyPlayer";
    private const string TRIGGER_NAME_KEY_CODE = "Interaction";
    private const string PATH_PREFAB_CANVAS_DISPLAY_KEY_CODE = "Prefabs/UI/CanvasDisplayKeyCode";
    

    [Header("Этот ресурс содержит предмет:")]
    [SerializeField] BaseItem item;

    private ItemBaseData dataItem;

    private KeyCode keyCodeGetItem = KeyCode.None;

    private CanvasDisplayKeyCode hintActiveKeyCode;
    private CanvasDisplayKeyCode hintKeyCodePrefab;

    private Character enteredPlayer;




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
        keyCodeGetItem = ControlManagerObject.Manager.ControlManager.GetKeyCodeByFragment(TRIGGER_NAME_KEY_CODE);
        hintKeyCodePrefab = Resources.Load<CanvasDisplayKeyCode>(PATH_PREFAB_CANVAS_DISPLAY_KEY_CODE);

        if (hintKeyCodePrefab == null)
        {
            throw new ItemObjectException("prefab hint key code not found");
        }
        dataItem = new ItemBaseData(item.data);
        dataItem.GenerateId();

    }

    // Update is called once per frame
    void Update()
    {
        if (rotating)
        {
        transform.Rotate(rotateVector * SPEED_ROTATING * Time.deltaTime);
        }


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



    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == TAG_PLAYER)
        {
            if (!collision.gameObject.TryGetComponent(out enteredPlayer))
            {
                throw new ItemObjectException("entered player not have component Character");
            }
            ShowOrDestroyHintKeyCode(true);
        }
    }



    private void ShowOrDestroyHintKeyCode(bool state)
    {
        playerEntered =  state;

        if (playerEntered && hintActiveKeyCode == null)
        {
            hintActiveKeyCode = ShowHintKeyCode(keyCodeGetItem, transform);
        }

        else if (!playerEntered && hintActiveKeyCode != null)
        {
            DestroyHintKeyCode();
        }
    }

    private bool TryAddItemToInventory()
    {
        return GameCacheManager.gameCache.inventory.TryAdd(dataItem);
    }

    public void AddItemToInventory()
    {
        throw new System.NotImplementedException();
    }

    public CanvasDisplayKeyCode ShowHintKeyCode(KeyCode keyCode, Transform targetTransform)
    {
        CanvasDisplayKeyCode newHint = Instantiate(hintKeyCodePrefab);
        newHint.SetKeyCode(keyCode);
        CanvasLockAt canvasLockAt;
        if (!newHint.TryGetComponent(out canvasLockAt))
        {
            throw new ItemObjectException("not found component CanvasLockAt on hint key code");
        }
        canvasLockAt.SetTarget(targetTransform);
        return newHint;
    }

    public void DestroyHintKeyCode()
    {
        if (hintActiveKeyCode != null)
        {
            Destroy(hintActiveKeyCode.gameObject);
        }
    }
}