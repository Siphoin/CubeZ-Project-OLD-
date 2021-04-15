[System.Serializable]
public class AudioZombieException : System.Exception
{
    public AudioZombieException() { }
    public AudioZombieException(string message) : base(message) { }
    public AudioZombieException(string message, System.Exception inner) : base(message, inner) { }
    protected AudioZombieException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}