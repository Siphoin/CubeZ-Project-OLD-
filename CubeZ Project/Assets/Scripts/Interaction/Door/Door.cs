using System;
using System.Collections;
using UnityEngine;


[RequireComponent(typeof(Collider))]
    public class Door : InteractionObjectContainerItems, IGeterHit
    {
    private Vector3 offsetKeyHint = new Vector3(-0.8F, 1, 1);

    public event Action<bool> onDoorInteraction;

    public event Action<string> onDead;
    [Header("Аниматор двери")]
    [SerializeField] Animator doorAnimator;

    [Header("Звук закрытия двери")]
    [SerializeField] AudioClip doorSoundExit;

    [Header("Звук открытия двери")]
    [SerializeField] AudioClip doorSoundOpen;

    private DoorSettings doorSettings;

    private Collider colliderDoor;

    private const string PATH_DOOR_SETTINGS = "Props/Door/DoorSettings";
    private const string TAG_DEAD_DOOR = "DeadDoor";

    [SerializeField, ReadOnlyField] DoorData doorData = new DoorData();

    private AudioObject activeAudioObject;


    public int Health { get => doorData.healthData.Value; }

    public bool IsOpened { get => doorData.isOpened; }

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

        doorData.healthData = new HealthData(doorSettings.StartHealth);
        doorData.healthData.onHealthChanged += NewHealthValue;


#if UNITY_EDITOR
        Debug.Log($"New Door ini: ({name}) Start Health: {doorData.healthData.StartValue}");
#endif

        int probality = ProbabilityUtility.GenerateProbalityInt();

        doorData.isBlocked = probality >= doorSettings.ProbabilityDoorBlocked;

        }

    private void NewHealthValue()
    {
        if (doorData.healthData.Value <= 0)
        {
            Dead();
        }
    }

    private void Dead()
    {
        colliderDoor.enabled = false;
        PlayAnimationDoor(DoorAnimationType.DoorDead);
        tag = TAG_DEAD_DOOR;
        doorData.healthData.onHealthChanged -= NewHealthValue;
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

            if (doorData.isOpened || doorData.isBlocked)
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
                doorData.isOpened = !doorData.isOpened;
                PlaySoundDoor();
                PlayAnimationDoor(doorData.isOpened == true ? DoorAnimationType.DoorOpen : DoorAnimationType.DoorExit);
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
        doorData.healthData.Damage(hitValue);
    }

    private void PlaySoundDoor ()
    {
        if (activeAudioObject == null)
        {
    activeAudioObject = AudioDataManager.Manager.CreateAudioObject(transform.position, IsOpened == true ? doorSoundOpen : doorSoundExit);
        activeAudioObject.GetAudioSource().Play();
        activeAudioObject.RemoveIfNotPlaying = true;
        }

    }
}