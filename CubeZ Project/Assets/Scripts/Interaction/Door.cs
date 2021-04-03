using System;
using System.Collections;
using UnityEngine;

    public class Door : InteractionObjectContainerItems
    {
    private bool isOpened = false;
    private Vector3 offsetKeyHint = new Vector3(-0.8F, 1, 1);

    public event Action<bool> onDoorInteraction;

    [SerializeField] Animator doorAnimator;

    public bool IsOpened { get => isOpened;}

    // Use this for initialization
    void Start()
        {
        if (doorAnimator == null)
        {
            throw new DoorException("door animator not seted");
        }
        Ini();
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
}