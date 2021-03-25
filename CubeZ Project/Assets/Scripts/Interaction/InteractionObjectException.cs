[System.Serializable]
public class InteractionObjectException : System.Exception
{
    public InteractionObjectException() { }
    public InteractionObjectException(string message) : base(message) { }
    public InteractionObjectException(string message, System.Exception inner) : base(message, inner) { }
    protected InteractionObjectException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}