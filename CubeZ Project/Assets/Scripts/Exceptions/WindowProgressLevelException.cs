[System.Serializable]
public class WindowProgressLevelException : System.Exception
{
    public WindowProgressLevelException() { }
    public WindowProgressLevelException(string message) : base(message) { }
    public WindowProgressLevelException(string message, System.Exception inner) : base(message, inner) { }
    protected WindowProgressLevelException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}