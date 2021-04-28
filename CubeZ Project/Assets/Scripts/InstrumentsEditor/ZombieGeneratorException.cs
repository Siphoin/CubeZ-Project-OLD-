[System.Serializable]
public class ZombieGeneratorException : System.Exception
{
    public ZombieGeneratorException() { }
    public ZombieGeneratorException(string message) : base(message) { }
    public ZombieGeneratorException(string message, System.Exception inner) : base(message, inner) { }
    protected ZombieGeneratorException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}