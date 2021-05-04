[System.Serializable]
public class AudioDataManagerEException : System.Exception
{
    public AudioDataManagerEException() { }
    public AudioDataManagerEException(string message) : base(message) { }
    public AudioDataManagerEException(string message, System.Exception inner) : base(message, inner) { }
    protected AudioDataManagerEException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}