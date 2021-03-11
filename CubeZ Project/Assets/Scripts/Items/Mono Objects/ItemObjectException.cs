[System.Serializable]
public class ItemObjectException : System.Exception
{
    public ItemObjectException() { }
    public ItemObjectException(string message) : base(message) { }
    public ItemObjectException(string message, System.Exception inner) : base(message, inner) { }
    protected ItemObjectException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}