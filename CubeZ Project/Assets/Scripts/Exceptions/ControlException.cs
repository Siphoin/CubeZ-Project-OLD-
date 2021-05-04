[System.Serializable]
public class ControlManagerException : System.Exception
{
    public ControlManagerException() { }
    public ControlManagerException(string message) : base(message) { }
    public ControlManagerException(string message, System.Exception inner) : base(message, inner) { }
    protected ControlManagerException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}