using System.Collections;
using UnityEngine;

    public class EmitterBlood : MonoBehaviour, IBloodCreator
    {
   private BloodSpawner bloodSpawner;

    private void Ini()
    {
        if (bloodSpawner == null)
        {
            bloodSpawner = FindObjectOfType<BloodSpawner>();
        }
    }

    public Blood CreateBlood (Vector3 center)
    {
        Ini();
        Blood blood = bloodSpawner.CreateBlood(center);
        blood.RemoveWithTime();
        return blood;
    }

    }