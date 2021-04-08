[System.Serializable]
public class LightRippleException : System.Exception
{
    public LightRippleException() { }
    public LightRippleException(string message) : base(message) { }
    public LightRippleException(string message, System.Exception inner) : base(message, inner) { }
    protected LightRippleException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}