[System.Serializable]
public class GameOverListenerException : System.Exception
{
    public GameOverListenerException() { }
    public GameOverListenerException(string message) : base(message) { }
    public GameOverListenerException(string message, System.Exception inner) : base(message, inner) { }
    protected GameOverListenerException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}