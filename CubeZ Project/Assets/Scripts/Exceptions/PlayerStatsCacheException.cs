[System.Serializable]
public class PlayerStstsCacheException : System.Exception
{
    public PlayerStstsCacheException() { }
    public PlayerStstsCacheException(string message) : base(message) { }
    public PlayerStstsCacheException(string message, System.Exception inner) : base(message, inner) { }
    protected PlayerStstsCacheException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}