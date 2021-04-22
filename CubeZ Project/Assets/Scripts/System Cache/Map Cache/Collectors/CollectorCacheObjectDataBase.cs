using System;
using System.Collections;
using UnityEngine;

    public class CollectorCacheObjectDataBase : MonoBehaviour
    {
    [Header("Индентификатор коллекции данных")]
    [SerializeField] protected string idCollection = null;


    public string IdCollection { get => idCollection; }
        // Use this for initialization
        void Start()
        {

        }

    protected void Ini ()
    {
        if (string.IsNullOrEmpty(idCollection))
        {
            throw new CollectorCacheObjectDataException($"collector {name} have null or emtry id");
        }
    }



    }