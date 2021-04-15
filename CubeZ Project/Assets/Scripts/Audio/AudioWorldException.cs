[System.Serializable]
public class AudioWorldException : System.Exception
{
    public AudioWorldException() { }
    public AudioWorldException(string message) : base(message) { }
    public AudioWorldException(string message, System.Exception inner) : base(message, inner) { }
    protected AudioWorldException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}