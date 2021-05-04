[System.Serializable]
public class SkyboxException : System.Exception
{
    public SkyboxException() { }
    public SkyboxException(string message) : base(message) { }
    public SkyboxException(string message, System.Exception inner) : base(message, inner) { }
    protected SkyboxException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}