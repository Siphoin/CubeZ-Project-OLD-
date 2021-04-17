using System.Collections;
using UnityEngine;
[RequireComponent(typeof(InteractionObject))]
    public class Bed : InteractionObject, IInteractionObject
    {
    [Header("Точка для сна игрока")]
    [SerializeField] private Transform pointSleep;
    

    /// <summary>
    /// Точка для сна игрока
    /// </summary>
    public Vector3 PointSleep { get => new Vector3(transform.position.x, transform.position.y, transform.position.z + 0.5f) ; }

    public Quaternion QuaternionSleep {

        get => Quaternion.Euler(0, transform.rotation.y, -90);
    }


    // Use this for initialization
    void Start()
        {
        Ini();


        if (pointSleep == null)
        {
            throw new BedException("point sleep is null");
        }

        if (PlayerManager.Manager == null)
        {
            throw new BedException("player manager not found");
        }


        if (PlayerManager.Manager.Player == null)
        {
            throw new BedException("player not found");
        }
    }

        // Update is called once per frame
        void Update()
        {
        if (enteredPlayer != null)
        {
            float distance = Vector3.Distance(enteredPlayer.transform.position, transform.position);
            if (distance >= 1.5f)
            {
                enteredPlayer = null;
                DestroyInteractionMenu();
            }

        }
        }

    public void CreateInteractionMenu()
    {
        activeIntegrationMenu = Instantiate(interactionMenuPrefab);
        CanvasLockAt canvasLockAt;
        if (!activeIntegrationMenu.TryGetComponent(out canvasLockAt))
        {
            throw new BedException("not found component CanvasLockAt on Interaction Menu");
        }
        canvasLockAt.SetTarget(transform);

        InteractionFragment fragmentSleep = new InteractionFragment(interactionSettings.GetActionNames()[0], SleepPlayer);
        activeIntegrationMenu.AddInterationFragment(fragmentSleep);
    }

    public void DestroyInteractionMenu()
    {
        if (activeIntegrationMenu != null)
        {
            Destroy(activeIntegrationMenu.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.tag == TAG_PLAYER)
        {
            if (enteredPlayer == null)
            {
                if (!collision.gameObject.TryGetComponent(out enteredPlayer))
                {
                    throw new BedException("player not have component Character");
                }
                CreateInteractionMenu();
                

            }
          
        }
    }

    private void SleepPlayer ()
    {
        PlayerManager.Manager.Player.Sleep(this);
        DestroyInteractionMenu();
    }
    #region NOT_USED_CODE

    /*
    private float GetAngleOnTarget ()
    {
        if (enteredPlayer)
        {
            Vector3 targetPos = enteredPlayer.transform.position;
            targetPos.y = transform.position.y;
            Vector3 forward = transform.forward;
            Vector3 targetDir = transform.position - targetPos;
            float angle = Vector3.SignedAngle(targetDir, forward, Vector3.up) * -1;
            return angle;
        }

        return 0;
    }

    */
    #endregion
}