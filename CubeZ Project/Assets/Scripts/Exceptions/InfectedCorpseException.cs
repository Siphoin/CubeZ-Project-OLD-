[System.Serializable]
public class InfectedCorpseException : System.Exception
{
    public InfectedCorpseException() { }
    public InfectedCorpseException(string message) : base(message) { }
    public InfectedCorpseException(string message, System.Exception inner) : base(message, inner) { }
    protected InfectedCorpseException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}