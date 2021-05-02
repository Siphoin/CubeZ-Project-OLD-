[System.Serializable]
public class LoadingMapUIException : System.Exception
{
    public LoadingMapUIException() { }
    public LoadingMapUIException(string message) : base(message) { }
    public LoadingMapUIException(string message, System.Exception inner) : base(message, inner) { }
    protected LoadingMapUIException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}