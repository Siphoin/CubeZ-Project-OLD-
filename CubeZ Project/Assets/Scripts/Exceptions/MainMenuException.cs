[System.Serializable]
public class MainMenuException : System.Exception
{
    public MainMenuException() { }
    public MainMenuException(string message) : base(message) { }
    public MainMenuException(string message, System.Exception inner) : base(message, inner) { }
    protected MainMenuException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}