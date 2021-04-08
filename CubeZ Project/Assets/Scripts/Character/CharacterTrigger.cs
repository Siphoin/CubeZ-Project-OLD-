using System;
using UnityEngine;
[RequireComponent(typeof(BoxCollider))]
    public class CharacterTrigger : MonoBehaviour
    {
    public event Action<string> onEnter;
    public event Action<string> onExit;

  private  Fire enteredFire = null;

    private const string FIRE_TAG = "FireArea";

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
        if (other.tag == FIRE_TAG)
        {
            if (!other.gameObject.transform.parent.TryGetComponent(out enteredFire))
            {
                throw new CharacterTriggerException("entered fire not have component Fire");
            }

            enteredFire.onRemove += FireRemove;
        }
        CallEvent(onEnter, other);
    }

    private void FireRemove()
    {
        CallEvent(onExit, FIRE_TAG);
    }

    private void OnTriggerExit(Collider other)
    {

        if (other.tag == FIRE_TAG)
        {
            if (!other.TryGetComponent(out enteredFire))
            {
                throw new CharacterTriggerException("entered fire not have component Fire");
            }

            enteredFire.onRemove -= FireRemove;
        }


        CallEvent(onExit, other);
    }

    private void CallEvent (Action<string> action, Collider collider)
    {
        action?.Invoke(collider.gameObject.tag);

        
    }

    private void CallEvent(Action<string> action, string tag)
    {
        action?.Invoke(tag);


    }
}