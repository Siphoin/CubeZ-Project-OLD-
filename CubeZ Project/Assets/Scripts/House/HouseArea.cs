using System;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BoxCollider))]
public class HouseArea : MonoBehaviour
    {
    public event Action<bool> onPlayerEnteredHouse;

    private const string TAG_PLAYER = "MyPlayerArea";
        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(TAG_PLAYER))
        {
            CallEvent(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(TAG_PLAYER))
        {
            CallEvent(false);
        }
    }

    private void CallEvent (bool state)
    {
        onPlayerEnteredHouse?.Invoke(state);
    }

}