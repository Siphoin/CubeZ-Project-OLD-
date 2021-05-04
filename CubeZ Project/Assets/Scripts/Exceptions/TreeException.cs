[System.Serializable]
public class TreeException : System.Exception
{
    public TreeException() { }
    public TreeException(string message) : base(message) { }
    public TreeException(string message, System.Exception inner) : base(message, inner) { }
    protected TreeException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}