[System.Serializable]
public class BloodSpawnerException : System.Exception
{
    public BloodSpawnerException() { }
    public BloodSpawnerException(string message) : base(message) { }
    public BloodSpawnerException(string message, System.Exception inner) : base(message, inner) { }
    protected BloodSpawnerException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}