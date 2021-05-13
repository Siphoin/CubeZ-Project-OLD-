using UnityEngine;
[System.Serializable]
public class TreeData
    {
    [Header("Стартовое здоровье деревьев")]
    public int startHealth = 25;

    [Header("Повышение опыта персонажа после срубки дерева")]
    public int xpBonus = 5;
    public TreeData ()
    {

    }

    public TreeData (TreeData copyClass)
    {
        copyClass.CopyAll(this);
    }
    }