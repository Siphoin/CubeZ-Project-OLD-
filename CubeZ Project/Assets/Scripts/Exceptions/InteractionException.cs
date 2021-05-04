[System.Serializable]
public class InteractionException : System.Exception
{
    public InteractionException() { }
    public InteractionException(string message) : base(message) { }
    public InteractionException(string message, System.Exception inner) : base(message, inner) { }
    protected InteractionException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}