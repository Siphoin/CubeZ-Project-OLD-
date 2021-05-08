[System.Serializable]
public class DebugMessagePythonUIException : System.Exception
{
    public DebugMessagePythonUIException() { }
    public DebugMessagePythonUIException(string message) : base(message) { }
    public DebugMessagePythonUIException(string message, System.Exception inner) : base(message, inner) { }
    protected DebugMessagePythonUIException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}