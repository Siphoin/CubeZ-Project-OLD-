using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public static class NavMeshManager
    {
    public static Vector3 GenerateRandomPath (Vector3 center)
    {
        Vector3 randomPos = Random.insideUnitSphere * 5 + center;

        NavMeshHit hit;

        NavMesh.SamplePosition(randomPos, out hit, 5, NavMesh.AllAreas);
        return hit.position;
    }
    }