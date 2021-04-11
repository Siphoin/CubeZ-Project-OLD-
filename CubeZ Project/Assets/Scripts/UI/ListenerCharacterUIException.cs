[System.Serializable]
public class ListenerCharacterUIException : System.Exception
{
    public ListenerCharacterUIException() { }
    public ListenerCharacterUIException(string message) : base(message) { }
    public ListenerCharacterUIException(string message, System.Exception inner) : base(message, inner) { }
    protected ListenerCharacterUIException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}