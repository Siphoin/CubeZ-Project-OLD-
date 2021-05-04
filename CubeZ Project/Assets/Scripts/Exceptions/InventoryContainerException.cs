[System.Serializable]
public class InventoryContainerException : System.Exception
{
    public InventoryContainerException() { }
    public InventoryContainerException(string message) : base(message) { }
    public InventoryContainerException(string message, System.Exception inner) : base(message, inner) { }
    protected InventoryContainerException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}