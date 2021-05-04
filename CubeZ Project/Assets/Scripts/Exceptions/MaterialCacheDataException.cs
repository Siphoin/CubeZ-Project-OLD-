[System.Serializable]
public class MaterialCacheDataException : System.Exception
{
    public MaterialCacheDataException() { }
    public MaterialCacheDataException(string message) : base(message) { }
    public MaterialCacheDataException(string message, System.Exception inner) : base(message, inner) { }
    protected MaterialCacheDataException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}