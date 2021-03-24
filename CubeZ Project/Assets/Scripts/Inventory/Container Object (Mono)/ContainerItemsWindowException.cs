[System.Serializable]
public class ContainerItemsWindowException : System.Exception
{
    public ContainerItemsWindowException() { }
    public ContainerItemsWindowException(string message) : base(message) { }
    public ContainerItemsWindowException(string message, System.Exception inner) : base(message, inner) { }
    protected ContainerItemsWindowException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}