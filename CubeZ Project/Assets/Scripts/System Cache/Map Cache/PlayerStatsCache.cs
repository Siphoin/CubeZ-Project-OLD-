using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
    public class PlayerStatsCache
    {
    public Dictionary<NeedCharacterType, int> statsNeeds = new Dictionary<NeedCharacterType, int>();


    public void AddStatsNeed (NeedCharacterType typeStsts, int value)
    {
        if (statsNeeds.ContainsKey(typeStsts))
        {
            throw new PlayerStstsCacheException($"{typeStsts} contains in list stats");
        }

        value = Mathf.Clamp(value, 0, 100);

        statsNeeds.Add(typeStsts, value);
    }

    }