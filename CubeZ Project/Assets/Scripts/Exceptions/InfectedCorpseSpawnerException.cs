[System.Serializable]
public class InfectedCorpseSpawnrtException : System.Exception
{
    public InfectedCorpseSpawnrtException() { }
    public InfectedCorpseSpawnrtException(string message) : base(message) { }
    public InfectedCorpseSpawnrtException(string message, System.Exception inner) : base(message, inner) { }
    protected InfectedCorpseSpawnrtException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}