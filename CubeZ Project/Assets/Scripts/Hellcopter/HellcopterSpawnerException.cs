[System.Serializable]
public class HellcopterSpawnerException : System.Exception
{
    public HellcopterSpawnerException() { }
    public HellcopterSpawnerException(string message) : base(message) { }
    public HellcopterSpawnerException(string message, System.Exception inner) : base(message, inner) { }
    protected HellcopterSpawnerException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}