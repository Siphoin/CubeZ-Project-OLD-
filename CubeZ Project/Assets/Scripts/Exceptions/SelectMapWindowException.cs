[System.Serializable]
public class SelectMapWindowException : System.Exception
{
    public SelectMapWindowException() { }
    public SelectMapWindowException(string message) : base(message) { }
    public SelectMapWindowException(string message, System.Exception inner) : base(message, inner) { }
    protected SelectMapWindowException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}