[System.Serializable]
public class APIObjectException : System.Exception
{
    public APIObjectException() { }
    public APIObjectException(string message) : base(message) { }
    public APIObjectException(string message, System.Exception inner) : base(message, inner) { }
    protected APIObjectException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}