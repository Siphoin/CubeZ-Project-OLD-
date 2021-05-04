[System.Serializable]
public class WispSpawnerException : System.Exception
{
    public WispSpawnerException() { }
    public WispSpawnerException(string message) : base(message) { }
    public WispSpawnerException(string message, System.Exception inner) : base(message, inner) { }
    protected WispSpawnerException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}