[System.Serializable]
public class LoaderMapException : System.Exception
{
    public LoaderMapException() { }
    public LoaderMapException(string message) : base(message) { }
    public LoaderMapException(string message, System.Exception inner) : base(message, inner) { }
    protected LoaderMapException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}