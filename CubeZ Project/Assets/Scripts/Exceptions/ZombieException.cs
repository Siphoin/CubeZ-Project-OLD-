[System.Serializable]
public class ZombieException : System.Exception
{
    public ZombieException() { }
    public ZombieException(string message) : base(message) { }
    public ZombieException(string message, System.Exception inner) : base(message, inner) { }
    protected ZombieException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}