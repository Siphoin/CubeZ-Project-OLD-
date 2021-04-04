using System;
using System.Collections;
using UnityEngine;


[RequireComponent(typeof(Collider))]
    public class Door : InteractionObjectContainerItems, IGeterHit
    {
    private bool isOpened = false;
    private Vector3 offsetKeyHint = new Vector3(-0.8F, 1, 1);

    public event Action<bool> onDoorInteraction;

    public event Action<string> onDead;

    [SerializeField] Animator doorAnimator;

    private DoorSettings doorSettings;

    private Collider colliderDoor;

    private const string PATH_DOOR_SETTINGS = "Props/Door/DoorSettings";
    private const string TAG_DEAD_DOOR = "DeadDoor";

  [SerializeField, ReadOnlyField]  private HealthData healthData = new HealthData();

    public int Health { get => healthData.Value; }

    public bool IsOpened { get => isOpened; }

    // Use this for initialization
    void Start()
        {
        if (doorAnimator == null)
        {
            throw new DoorException("door animator not seted");
        }
        Ini();

        doorSettings = Resources.Load<DoorSettings>(PATH_DOOR_SETTINGS);

        if (doorSettings == null)
        {
            throw new DoorException("door settings not found");
        }

        colliderDoor = GetComponent<Collider>();

        healthData = new HealthData(doorSettings.StartHealth);
        healthData.onHealthChanged += NewHealthValue;

        Debug.Log($"New Door ini: ({name}) Start Health: {healthData.StartValue}");
        }

    private void NewHealthValue()
    {
        if (healthData.Value <= 0)
        {
            Dead();
        }
    }

    private void Dead()
    {
        colliderDoor.enabled = false;
        PlayAnimationDoor(DoorAnimationType.DoorDead);
        tag = TAG_DEAD_DOOR;
        healthData.onHealthChanged -= NewHealthValue;
        DestroyHintKeyCode();
        Destroy(this);
    }

    // Update is called once per frame
    void Update()
        {
        CheckDistanceOffsetPlayer();
        }

    private void OnCollisionEnter(Collision collision)
    {
        if (CheckCollision(collision))
        {
            hintActiveKeyCode.GetComponent<CanvasLockAt>().SetOffset(offsetKeyHint);

            if (isOpened)
            {
                Destroy(hintActiveKeyCode.gameObject);
            }
        }   
        
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

            if (Input.GetKeyDown(keyCodeGetItem))
            {
                isOpened = !isOpened;
                PlayAnimationDoor(isOpened == true ? DoorAnimationType.DoorOpen : DoorAnimationType.DoorExit);
                DestroyHintKeyCode();
                onDoorInteraction?.Invoke(enteredPlayer.transform.position.x < transform.position.x);

            }
        }
    }

    private void PlayAnimationDoor (DoorAnimationType animationType)
    {
        doorAnimator.Play(animationType.ToString());
    }

    public void Hit(int hitValue, bool playHitAnim = true)
    {
        healthData.Damage(hitValue);
    }
}