[System.Serializable]
public class PythonEngineException : System.Exception
{
    public PythonEngineException() { }
    public PythonEngineException(string message) : base(message) { }
    public PythonEngineException(string message, System.Exception inner) : base(message, inner) { }
    protected PythonEngineException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}