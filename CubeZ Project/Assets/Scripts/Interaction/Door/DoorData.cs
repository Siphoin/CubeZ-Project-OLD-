using UnityEngine;

[System.Serializable]
public class DoorData
{
    [Header("Дверь открыта")]
    [ReadOnlyField]
    public bool isOpened = false;
    [Header("Дверь заблокирована")]
    [ReadOnlyField]
    public bool isBlocked = false;


    public HealthData healthData = new HealthData();

    public DoorData ()
    {

    }

    public DoorData (DoorData copyClass)
    {
        copyClass.CopyAll(this);
    }

    public DoorData (HealthData healthData)
    {
        this.healthData = healthData;
    }
}