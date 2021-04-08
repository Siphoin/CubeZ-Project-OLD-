using System;
using System.Collections;
using UnityEngine;

    public class Wisp : MonoBehaviour, IInvokerMono
    {
   
    private Vector3 targetPoint;



    // Use this for initialization
    void Start()
        {
        NewTargetPoint();
        targetPoint = transform.position;
        }

        // Update is called once per frame
        void Update()
        {
        float distance = Vector3.Distance(transform.position, targetPoint);
        if (distance > 1f)
        {
            try
            {
                transform.position += (transform.position - targetPoint).normalized * 2 * Time.deltaTime;
            }
            catch
            {
            }
        }
        }

    public void CallInvokingEveryMethod(Action method, float time)
    {
        InvokeRepeating(method.Method.Name, time, time);
    }

    public void CallInvokingMethod(Action method, float time)
    {
        Invoke(method.Method.Name, time);
    }

    private void NewTargetPoint ()
    {
        
        Vector3 newPoint = NavMeshManager.GenerateRandomPath(transform.position);
        targetPoint = newPoint;
        CallInvokingMethod(NewTargetPoint, 5);
    }

}