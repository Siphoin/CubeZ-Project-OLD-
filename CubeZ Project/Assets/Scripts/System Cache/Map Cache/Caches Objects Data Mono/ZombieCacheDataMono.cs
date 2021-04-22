using System.Collections;
using UnityEngine;

    public class ZombieCacheDataMono : CacheObjectDataMono
{
    private BaseZombie baseZombie;
        // Use this for initialization
        void Start()
        {
        if (!TryGetComponent(out baseZombie))
        {
            throw new CacheObjectException($"({name}) not have component Zombie");
        }
        Ini();
        }

        // Update is called once per frame
        void Update()
        {

        }

    public ZombieStats GetStats ()
    {
        return baseZombie.ZombieStats;
    }
    }