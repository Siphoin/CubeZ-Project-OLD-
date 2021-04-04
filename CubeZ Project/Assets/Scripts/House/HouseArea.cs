using System;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BoxCollider))]
public class HouseArea : MonoBehaviour
    {
    public event Action<bool> onPlayerEnteredHouse;
    public event Action<bool> onOtherPlayerEnteredHouse;
    public event Action<bool> onZombieEnteredHouse;


    private const string TAG_PLAYER = "MyPlayerArea";
    private const string TAG_PLAYER_AREA = "PlayerArea";
    private const string TAG_ZOMBIE = "ZombieArea";

    private BoxCollider boxCollider;

    // Use this for initialization
    void Start()
    {
        Ini();
    }

    private void Ini()
    {
        if (boxCollider != null)
        {
            return;
        }


        if (!TryGetComponent(out boxCollider))
        {
            throw new HouseAreaException("not found component BoxColider");
        }
    }

    // Update is called once per frame
    void Update()
        {

        }
    private void CallEventPlayerLocal(bool state)
    {
        onPlayerEnteredHouse?.Invoke(state);
    }


    private void CallEventPlayer(bool state)
    {
        onOtherPlayerEnteredHouse?.Invoke(state);
    }

    private void CallEventZombie(bool state)
    {
        onZombieEnteredHouse?.Invoke(state);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(TAG_ZOMBIE))
        {
            CallEventZombie(true);
            return;
        }
        if (other.CompareTag(TAG_PLAYER))
        {
            CallEventPlayerLocal(true);
            return;
        }

        if (other.tag.Contains(TAG_PLAYER_AREA))
        {
            CallEventPlayer(true);
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(TAG_ZOMBIE))
        {
            CallEventZombie(false);
            return;
        }


        if (other.CompareTag(TAG_PLAYER))
        {
            CallEventPlayerLocal(false);
            return;
        }

        if (other.tag.Contains(TAG_PLAYER_AREA))
        {
            CallEventPlayer(false);
        }
    }

    public Bounds GetBounds ()
    {
        Ini();
        return boxCollider.bounds;
    }


}