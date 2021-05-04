[System.Serializable]
public class BedException : System.Exception
{
    public BedException() { }
    public BedException(string message) : base(message) { }
    public BedException(string message, System.Exception inner) : base(message, inner) { }
    protected BedException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}