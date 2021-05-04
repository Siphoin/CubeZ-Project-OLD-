[System.Serializable]
public class ZombieSpawnerException : System.Exception
{
    public ZombieSpawnerException() { }
    public ZombieSpawnerException(string message) : base(message) { }
    public ZombieSpawnerException(string message, System.Exception inner) : base(message, inner) { }
    protected ZombieSpawnerException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}