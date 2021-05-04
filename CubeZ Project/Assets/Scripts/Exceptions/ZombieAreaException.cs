[System.Serializable]
public class ZombieAreaException : System.Exception
{
    public ZombieAreaException() { }
    public ZombieAreaException(string message) : base(message) { }
    public ZombieAreaException(string message, System.Exception inner) : base(message, inner) { }
    protected ZombieAreaException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}