[System.Serializable]
public class AudioHellcopterException : System.Exception
{
    public AudioHellcopterException() { }
    public AudioHellcopterException(string message) : base(message) { }
    public AudioHellcopterException(string message, System.Exception inner) : base(message, inner) { }
    protected AudioHellcopterException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}