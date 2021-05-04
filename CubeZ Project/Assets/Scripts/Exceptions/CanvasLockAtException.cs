[System.Serializable]
public class CanvasLockAtException : System.Exception
{
    public CanvasLockAtException() { }
    public CanvasLockAtException(string message) : base(message) { }
    public CanvasLockAtException(string message, System.Exception inner) : base(message, inner) { }
    protected CanvasLockAtException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}