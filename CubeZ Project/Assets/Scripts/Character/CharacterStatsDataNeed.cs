
using System;

[System.Serializable]
public class CharacterStatsDataNeed
{
    public NeedCharacterType needType;
    public int value = 100;
    public int speedNeed = 1;

    public const int DEFAULT_VALUE = 100;

    public event Action<int> onValueChanged;
    public CharacterStatsDataNeed()
    {

    }

    public CharacterStatsDataNeed(NeedCharacterType needType)
    {
        this.needType = needType;
    }

    public CharacterStatsDataNeed(int value, NeedCharacterType needType)
    {
        CheckInvalidValueNeed(value);
        this.needType = needType;
        this.value = value;
    }

    public CharacterStatsDataNeed(NeedCharacterType needType, int value, int speedNeed)
    {
        CheckInvalidValueNeed(value);
        CheckInvalidValueNeed(speedNeed);
        this.needType = needType;
        this.value = value;
        this.speedNeed = speedNeed;
    }

    public CharacterStatsDataNeed(CharacterStatsDataNeed copyClass)
    {
        copyClass.CopyAll(this);
    }

    private void CheckInvalidValueNeed(int valueNeed)
    {
        if (valueNeed < 0)
        {
            throw new CharacterException("value need is invalid value: value not must be < 0!");
        }
    }

    public void CallOnValueChanged()
    {
        onValueChanged?.Invoke(value);
    }

    public int GetDefaultValue()
    {
        return DEFAULT_VALUE;
    }
}