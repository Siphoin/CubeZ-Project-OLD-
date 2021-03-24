[System.Serializable]
public class ControlManagerObjectException : System.Exception
{
    public ControlManagerObjectException() { }
    public ControlManagerObjectException(string message) : base(message) { }
    public ControlManagerObjectException(string message, System.Exception inner) : base(message, inner) { }
    protected ControlManagerObjectException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}