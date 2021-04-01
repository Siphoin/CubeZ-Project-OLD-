using System.Collections;
using UnityEngine;

    public class InteractionObjectContainerItems : MonoBehaviour, IHintKeyCodeDisplay
    {
        protected bool playerEntered = false;


        protected const string PATH_PREFAB_CANVAS_DISPLAY_KEY_CODE = "Prefabs/UI/CanvasDisplayKeyCode";
       protected const string PATH_FOLBER_ITEMS_RESOURCES = "Items/Resources/";
    private const string TRIGGER_NAME_KEY_CODE = "Interaction";
    private const string TAG_PLAYER = "MyPlayer";


    protected KeyCode keyCodeGetItem = KeyCode.None;

        protected CanvasDisplayKeyCode hintActiveKeyCode;
        protected CanvasDisplayKeyCode hintKeyCodePrefab;

        protected Character enteredPlayer;

    // Use this for initialization
    void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }


    protected void CheckCollision(Collision collision)
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

    protected void ShowOrDestroyHintKeyCode(bool state)
    {
        playerEntered = state;

        if (playerEntered && hintActiveKeyCode == null)
        {
            hintActiveKeyCode = ShowHintKeyCode(keyCodeGetItem, transform);
        }

        else if (!playerEntered && hintActiveKeyCode != null)
        {
            DestroyHintKeyCode();
        }
    }



    public void DestroyHintKeyCode()
    {
        if (hintActiveKeyCode != null)
        {
            Destroy(hintActiveKeyCode.gameObject);
        }
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
    protected void Ini()
    {
        keyCodeGetItem = ControlManagerObject.Manager.ControlManager.GetKeyCodeByFragment(TRIGGER_NAME_KEY_CODE);
        hintKeyCodePrefab = Resources.Load<CanvasDisplayKeyCode>(PATH_PREFAB_CANVAS_DISPLAY_KEY_CODE);

        if (hintKeyCodePrefab == null)
        {
            throw new ItemObjectException("prefab hint key code not found");
        }
    }

    public virtual void CheckDistanceOffsetPlayer ()
    {
        if (playerEntered && enteredPlayer != null)
        {
            float distance = Vector3.Distance(enteredPlayer.transform.position, transform.position);
            if (distance >= 1f)
            {
                ShowOrDestroyHintKeyCode(false);
            }

            if (Input.GetKeyDown(keyCodeGetItem))
            {
                Destroy(gameObject);
            }
        }
    }
}