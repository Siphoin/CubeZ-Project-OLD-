[System.Serializable]
public class BuildObjectException : System.Exception
{
    public BuildObjectException() { }
    public BuildObjectException(string message) : base(message) { }
    public BuildObjectException(string message, System.Exception inner) : base(message, inner) { }
    protected BuildObjectException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}