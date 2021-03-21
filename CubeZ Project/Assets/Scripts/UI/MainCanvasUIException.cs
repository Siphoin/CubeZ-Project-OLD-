[System.Serializable]
public class MainCanvasUIException : System.Exception
{
    public MainCanvasUIException() { }
    public MainCanvasUIException(string message) : base(message) { }
    public MainCanvasUIException(string message, System.Exception inner) : base(message, inner) { }
    protected MainCanvasUIException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}