[System.Serializable]
public class TimeGameSessionManagerException : System.Exception
{
    public TimeGameSessionManagerException() { }
    public TimeGameSessionManagerException(string message) : base(message) { }
    public TimeGameSessionManagerException(string message, System.Exception inner) : base(message, inner) { }
    protected TimeGameSessionManagerException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}