using System.Collections;
using UnityEngine;
[System.Serializable]
    public class CacheObjectData : ICacheObjectData
    {

        public string idObject;
    public bool isMaterial = false;

        public CacheObjectData ()
        {

        }

    public CacheObjectData (CacheObjectData copyClass)
    {
        copyClass.CopyAll(this);
    }

   
    }