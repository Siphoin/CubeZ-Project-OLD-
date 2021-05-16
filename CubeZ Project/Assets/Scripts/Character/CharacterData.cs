using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CharacterData
{
    [Header("Параметры скорости персонажа")]
    public float speed = 3;
    public float rotateSpeed = 5;

    [Header("Базовый урон")]
    public int damage = 6;

    [Header("Восстает в секундах")]
    public float rebelTime = 5.0f;

    [Header("Параметры потребностей")]

    [SerializeField]
    private Dictionary<NeedCharacterType, CharacterStatsDataNeed> needs = new Dictionary<NeedCharacterType, CharacterStatsDataNeed>()
{
    {NeedCharacterType.Eat, new CharacterStatsDataNeed(NeedCharacterType.Eat, 100, 4) },
    {NeedCharacterType.Run, new CharacterStatsDataNeed(NeedCharacterType.Run, 100, 0) },
    {NeedCharacterType.Sleep, new CharacterStatsDataNeed(NeedCharacterType.Sleep, 100, 7) },
    {NeedCharacterType.Temperature, new CharacterStatsDataNeed(NeedCharacterType.Temperature, 100, 3) },
    {NeedCharacterType.Health, new CharacterStatsDataNeed(NeedCharacterType.Health, 100, 0) },
};

    [Header("Оптимальная температура тела")]
    public int optimalTemperatureBody = 25;

    [Header("Критическая температура мира для тела")]
    public int criticalTemperatureWorldForBody = 25;


    public CharacterData()
    {

    }

    public CharacterData(CharacterData copyClass)
    {
        copyClass.CopyAll(this);
    }

    public Dictionary<NeedCharacterType, CharacterStatsDataNeed> GetDictonaryNeeds()
    {
        return needs;
    }

}