[System.Serializable]
public class TextShowNewDayUIException : System.Exception
{
    public TextShowNewDayUIException() { }
    public TextShowNewDayUIException(string message) : base(message) { }
    public TextShowNewDayUIException(string message, System.Exception inner) : base(message, inner) { }
    protected TextShowNewDayUIException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}