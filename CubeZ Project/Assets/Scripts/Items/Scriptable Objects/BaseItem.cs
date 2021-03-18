using UnityEditor;
using UnityEngine;

    public class BaseItem : ScriptableObject
    {
    public ItemBaseData data = new ItemBaseData();
    public BaseItem ()
    {

    }

    public BaseItem (BaseItem copyClass)
    {
        copyClass.CopyAll(this);
    }


}