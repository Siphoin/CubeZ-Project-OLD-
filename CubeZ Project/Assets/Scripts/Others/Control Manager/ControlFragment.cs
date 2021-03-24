using System.Collections;
using UnityEngine;
[System.Serializable]
    public class ControlFragment
    {
    public string nameTrigger = "NAME_TRIGGER";
    public KeyCode keyCode = KeyCode.A;

    public ControlFragment ()
    {

    }

    public ControlFragment (ControlFragment copyClass)
    {
        copyClass.CopyAll(this);
    }
    }