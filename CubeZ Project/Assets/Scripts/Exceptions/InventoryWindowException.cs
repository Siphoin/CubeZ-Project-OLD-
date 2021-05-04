[System.Serializable]
public class InventoryWindowException : System.Exception
{
    public InventoryWindowException() { }
    public InventoryWindowException(string message) : base(message) { }
    public InventoryWindowException(string message, System.Exception inner) : base(message, inner) { }
    protected InventoryWindowException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}