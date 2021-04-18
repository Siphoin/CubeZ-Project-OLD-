[System.Serializable]
public class AudioTreeException : System.Exception
{
    public AudioTreeException() { }
    public AudioTreeException(string message) : base(message) { }
    public AudioTreeException(string message, System.Exception inner) : base(message, inner) { }
    protected AudioTreeException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}