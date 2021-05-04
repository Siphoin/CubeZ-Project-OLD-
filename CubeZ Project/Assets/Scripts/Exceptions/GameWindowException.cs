[System.Serializable]
public class GameWindowException : System.Exception
{
    public GameWindowException() { }
    public GameWindowException(string message) : base(message) { }
    public GameWindowException(string message, System.Exception inner) : base(message, inner) { }
    protected GameWindowException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}