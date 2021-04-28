using UnityEngine;
using UnityEngine.AI;

public static class NavMeshManager
{
    public static Vector3 GenerateRandomPath(Vector3 center)
    {
        Vector3 randomPos = Random.insideUnitSphere * 5 + center;
       
        return randomPos;
    }
}