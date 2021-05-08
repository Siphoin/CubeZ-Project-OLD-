[System.Serializable]
public class DebugerPythonException : System.Exception
{
    public DebugerPythonException() { }
    public DebugerPythonException(string message) : base(message) { }
    public DebugerPythonException(string message, System.Exception inner) : base(message, inner) { }
    protected DebugerPythonException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}