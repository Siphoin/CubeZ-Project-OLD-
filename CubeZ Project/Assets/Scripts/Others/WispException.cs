[System.Serializable]
public class WispException : System.Exception
{
    public WispException() { }
    public WispException(string message) : base(message) { }
    public WispException(string message, System.Exception inner) : base(message, inner) { }
    protected WispException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}