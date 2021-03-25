using UnityEngine.Events;

[System.Serializable]
    public class InteractionFragment
    {
    public string actionName;
    public UnityAction action;

    public InteractionFragment ()
    {

    }

    public InteractionFragment (string nameAction, UnityAction action)
    {
        actionName = nameAction;
        this.action = action;
    }

    public InteractionFragment (InteractionFragment copyClass)
    {
        copyClass.CopyAll(this);
    }
    }