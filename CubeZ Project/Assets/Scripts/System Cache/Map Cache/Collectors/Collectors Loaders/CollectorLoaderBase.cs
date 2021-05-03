using System;
using UnityEngine;

public class CollectorLoaderBase : MonoBehaviour
    {
    public event Action onOperationFinish;
    public event Action onFinish;


    protected void CallEventOperationFinish ()
    {
        onOperationFinish?.Invoke();
    }

    protected void CallEventFinish()
    {
       onFinish?.Invoke();
    }
}