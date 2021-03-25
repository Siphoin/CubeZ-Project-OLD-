[System.Serializable]
public class SleepWindowControllerException : System.Exception
{
    public SleepWindowControllerException() { }
    public SleepWindowControllerException(string message) : base(message) { }
    public SleepWindowControllerException(string message, System.Exception inner) : base(message, inner) { }
    protected SleepWindowControllerException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}