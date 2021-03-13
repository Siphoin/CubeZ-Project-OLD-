[System.Serializable]
public class PlayerManagerException : System.Exception
{
    public PlayerManagerException() { }
    public PlayerManagerException(string message) : base(message) { }
    public PlayerManagerException(string message, System.Exception inner) : base(message, inner) { }
    protected PlayerManagerException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}