using UnityEngine;

public class TimerDestroy : MonoBehaviour
{
     public float timeDestroy;
    // Use this for initialization
    void Start()
    {
        Destroy(gameObject, timeDestroy);
    }

}