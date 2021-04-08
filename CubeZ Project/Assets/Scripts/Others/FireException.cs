[System.Serializable]
public class FireException : System.Exception
{
    public FireException() { }
    public FireException(string message) : base(message) { }
    public FireException(string message, System.Exception inner) : base(message, inner) { }
    protected FireException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}