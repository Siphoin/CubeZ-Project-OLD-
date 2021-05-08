[System.Serializable]
public class PatternScriptElementUIException : System.Exception
{
    public PatternScriptElementUIException() { }
    public PatternScriptElementUIException(string message) : base(message) { }
    public PatternScriptElementUIException(string message, System.Exception inner) : base(message, inner) { }
    protected PatternScriptElementUIException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}