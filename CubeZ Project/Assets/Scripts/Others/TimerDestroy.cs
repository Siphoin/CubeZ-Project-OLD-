using System.Collections;
using UnityEngine;

    public class TimerDestroy : MonoBehaviour
    {
        [HideInInspector] public float timeDestroy;
        // Use this for initialization
        void Start()
        {
            Destroy(gameObject, timeDestroy);
        }

        // Update is called once per frame
        void Update()
        {

        }
    }