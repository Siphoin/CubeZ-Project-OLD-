[System.Serializable]
    public class InteractionData
    {
    public string[] actionsNames = new string[0];


    public InteractionData ()
    {

    }

    public InteractionData (InteractionData copyClass)
    {
        copyClass.CopyAll(this);
    }

    public InteractionData (string[] actionsNames)
    {
        this.actionsNames = actionsNames;
    }

    }