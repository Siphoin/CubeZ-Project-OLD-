[System.Serializable]
public class TrainSpawnerException : System.Exception
{
    public TrainSpawnerException() { }
    public TrainSpawnerException(string message) : base(message) { }
    public TrainSpawnerException(string message, System.Exception inner) : base(message, inner) { }
    protected TrainSpawnerException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}