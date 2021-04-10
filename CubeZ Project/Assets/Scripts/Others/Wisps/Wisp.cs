using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

[RequireComponent(typeof(NavMeshAgent))]
    public class Wisp : MonoBehaviour, IInvokerMono, IRemoveObject
    {
    [Header("Минимальная задержка нахождения нового пути")]
    [SerializeField] float minWaitNewPath = 1;

    [Header("Максимальная задержка нахождения нового пути")]
    [SerializeField] float maxWaitNewPath = 12;
    private Vector3 targetPoint;

    private NavMeshAgent agent;

    public event Action<Wisp> onRemove;



    // Use this for initialization
    void Start()
        {
        if (minWaitNewPath <= 0 || maxWaitNewPath <= 0)
        {
            throw new WispException("minWaitPath or maxWaitPath not must be <= 0!");
        }

        if (minWaitNewPath >= maxWaitNewPath)
        {
            throw new WispException("minWaitPath  not must be >= maxWaitNewPath");
        }


        if (!TryGetComponent(out agent))
        {
            throw new WispException("not found component Nav Mesh Agent");
        }


        NewTargetPoint();
    }

        // Update is called once per frame
        void Update()
        {
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
        agent.SetDestination(targetPoint);
        CallInvokingMethod(NewTargetPoint, Random.Range(minWaitNewPath, maxWaitNewPath + 1));
    }

    public void Remove ()
    {
        onRemove?.Invoke(this);
        Destroy(gameObject);
    }


}