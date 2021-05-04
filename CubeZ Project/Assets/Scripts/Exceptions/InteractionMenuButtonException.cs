[System.Serializable]
public class InteractionMenuButtonException : System.Exception
{
    public InteractionMenuButtonException() { }
    public InteractionMenuButtonException(string message) : base(message) { }
    public InteractionMenuButtonException(string message, System.Exception inner) : base(message, inner) { }
    protected InteractionMenuButtonException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}