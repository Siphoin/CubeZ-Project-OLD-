[System.Serializable]
public class SleepWindowException : System.Exception
{
    public SleepWindowException() { }
    public SleepWindowException(string message) : base(message) { }
    public SleepWindowException(string message, System.Exception inner) : base(message, inner) { }
    protected SleepWindowException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}