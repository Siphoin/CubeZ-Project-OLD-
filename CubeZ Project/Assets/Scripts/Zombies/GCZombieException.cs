[System.Serializable]
public class GCZombieException : System.Exception
{
    public GCZombieException() { }
    public GCZombieException(string message) : base(message) { }
    public GCZombieException(string message, System.Exception inner) : base(message, inner) { }
    protected GCZombieException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}