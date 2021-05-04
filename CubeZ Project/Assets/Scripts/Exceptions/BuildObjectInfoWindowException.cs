[System.Serializable]
public class BuildObjectInfoWindowException : System.Exception
{
    public BuildObjectInfoWindowException() { }
    public BuildObjectInfoWindowException(string message) : base(message) { }
    public BuildObjectInfoWindowException(string message, System.Exception inner) : base(message, inner) { }
    protected BuildObjectInfoWindowException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}