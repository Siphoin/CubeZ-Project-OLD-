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
    public Vector3 PointSleep { get => pointSleep.position; }

    public Quaternion QuaternionSleep {

        get => 
            
     Quaternion.Euler(
         
         new Vector3(-82.8f, -382, 20.3f)
         
         ); 
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

}