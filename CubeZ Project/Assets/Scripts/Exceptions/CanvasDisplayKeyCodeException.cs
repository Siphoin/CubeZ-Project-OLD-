[System.Serializable]
public class CanvasDisplayKeyCodeException : System.Exception
{
    public CanvasDisplayKeyCodeException() { }
    public CanvasDisplayKeyCodeException(string message) : base(message) { }
    public CanvasDisplayKeyCodeException(string message, System.Exception inner) : base(message, inner) { }
    protected CanvasDisplayKeyCodeException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}