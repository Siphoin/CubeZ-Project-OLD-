[System.Serializable]
public class ItemCellException : System.Exception
{
    public ItemCellException() { }
    public ItemCellException(string message) : base(message) { }
    public ItemCellException(string message, System.Exception inner) : base(message, inner) { }
    protected ItemCellException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}