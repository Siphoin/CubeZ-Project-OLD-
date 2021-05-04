[System.Serializable]
public class SirenLightObjectException : System.Exception
{
    public SirenLightObjectException() { }
    public SirenLightObjectException(string message) : base(message) { }
    public SirenLightObjectException(string message, System.Exception inner) : base(message, inner) { }
    protected SirenLightObjectException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}