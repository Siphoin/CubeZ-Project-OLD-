[System.Serializable]
public class HistoryException : System.Exception
{
    public HistoryException() { }
    public HistoryException(string message) : base(message) { }
    public HistoryException(string message, System.Exception inner) : base(message, inner) { }
    protected HistoryException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}