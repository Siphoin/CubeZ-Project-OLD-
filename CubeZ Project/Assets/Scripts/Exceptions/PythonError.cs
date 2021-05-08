[System.Serializable]
public class PythonError : System.Exception
{
    public PythonError() { }
    public PythonError(string message) : base(message) { }
    public PythonError(string message, System.Exception inner) : base(message, inner) { }
    protected PythonError(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}