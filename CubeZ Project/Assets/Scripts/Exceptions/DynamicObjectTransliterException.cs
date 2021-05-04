[System.Serializable]
public class DynamicObjectTransliterException : System.Exception
{
    public DynamicObjectTransliterException() { }
    public DynamicObjectTransliterException(string message) : base(message) { }
    public DynamicObjectTransliterException(string message, System.Exception inner) : base(message, inner) { }
    protected DynamicObjectTransliterException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}