using System;
using System.Collections;
using UnityEngine;

    public class AnimatorObserver : MonoBehaviour
    {

        public event Action onAttackEvent;
        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void CallAttackEvent()
        {
            onAttackEvent?.Invoke();
        }
    }