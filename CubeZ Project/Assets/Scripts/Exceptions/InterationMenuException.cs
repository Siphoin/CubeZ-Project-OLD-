[System.Serializable]
public class InteractionMenuException : System.Exception
{
    public InteractionMenuException() { }
    public InteractionMenuException(string message) : base(message) { }
    public InteractionMenuException(string message, System.Exception inner) : base(message, inner) { }
    protected InteractionMenuException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}