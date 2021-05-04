[System.Serializable]
public class CacheObjectException : System.Exception
{
    public CacheObjectException() { }
    public CacheObjectException(string message) : base(message) { }
    public CacheObjectException(string message, System.Exception inner) : base(message, inner) { }
    protected CacheObjectException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}