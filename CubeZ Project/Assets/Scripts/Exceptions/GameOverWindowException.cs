[System.Serializable]
public class GameOverWindowException : System.Exception
{
    public GameOverWindowException() { }
    public GameOverWindowException(string message) : base(message) { }
    public GameOverWindowException(string message, System.Exception inner) : base(message, inner) { }
    protected GameOverWindowException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}