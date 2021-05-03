[System.Serializable]
public class CollectorLoaderException : System.Exception
{
    public CollectorLoaderException() { }
    public CollectorLoaderException(string message) : base(message) { }
    public CollectorLoaderException(string message, System.Exception inner) : base(message, inner) { }
    protected CollectorLoaderException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}