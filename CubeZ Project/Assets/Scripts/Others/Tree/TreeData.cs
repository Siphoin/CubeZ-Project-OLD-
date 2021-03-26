using UnityEngine;
[System.Serializable]
public class TreeData
    {
    [Header("Стартовое здоровье деревьев")]
    public int startHealth = 25;
    public TreeData ()
    {

    }

    public TreeData (TreeData copyClass)
    {
        copyClass.CopyAll(this);
    }
    }