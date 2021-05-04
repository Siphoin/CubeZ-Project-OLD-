[System.Serializable]
public class ContainerObjectException : System.Exception
{
    public ContainerObjectException() { }
    public ContainerObjectException(string message) : base(message) { }
    public ContainerObjectException(string message, System.Exception inner) : base(message, inner) { }
    protected ContainerObjectException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}