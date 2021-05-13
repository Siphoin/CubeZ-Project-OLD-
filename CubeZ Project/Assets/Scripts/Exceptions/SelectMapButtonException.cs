[System.Serializable]
public class SelectMapButtonException : System.Exception
{
    public SelectMapButtonException() { }
    public SelectMapButtonException(string message) : base(message) { }
    public SelectMapButtonException(string message, System.Exception inner) : base(message, inner) { }
    protected SelectMapButtonException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}