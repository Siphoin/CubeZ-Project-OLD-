[System.Serializable]
public class WorldManagerException : System.Exception
{
    public WorldManagerException() { }
    public WorldManagerException(string message) : base(message) { }
    public WorldManagerException(string message, System.Exception inner) : base(message, inner) { }
    protected WorldManagerException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}