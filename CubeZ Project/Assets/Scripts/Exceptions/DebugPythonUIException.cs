[System.Serializable]
public class DebugPythonUIException : System.Exception
{
    public DebugPythonUIException() { }
    public DebugPythonUIException(string message) : base(message) { }
    public DebugPythonUIException(string message, System.Exception inner) : base(message, inner) { }
    protected DebugPythonUIException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}