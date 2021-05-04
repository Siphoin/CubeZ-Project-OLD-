[System.Serializable]
public class CollectorCacheObjectDataException : System.Exception
{
    public CollectorCacheObjectDataException() { }
    public CollectorCacheObjectDataException(string message) : base(message) { }
    public CollectorCacheObjectDataException(string message, System.Exception inner) : base(message, inner) { }
    protected CollectorCacheObjectDataException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}