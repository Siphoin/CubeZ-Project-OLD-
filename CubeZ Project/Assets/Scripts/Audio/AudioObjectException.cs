[System.Serializable]
public class AudioObjecException : System.Exception
{
    public AudioObjecException() { }
    public AudioObjecException(string message) : base(message) { }
    public AudioObjecException(string message, System.Exception inner) : base(message, inner) { }
    protected AudioObjecException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}