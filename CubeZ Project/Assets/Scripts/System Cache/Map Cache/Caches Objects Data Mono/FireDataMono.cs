using System.Collections;
using UnityEngine;

    public class FireDataMono : CacheObjectDataMono
{
    private Fire fire;
        // Use this for initialization
        void Start()
        {
        if (!TryGetComponent(out fire))
        {
            throw new FireException($"({name}) not found component Fire");
        }

        Ini();
        }

public FireCacheData GetData ()
    {
        return new FireCacheData(Id, fire.CountParticles, fire.intensity);
    }
    }