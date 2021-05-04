[System.Serializable]
public class GameCacheInventoryPlayerException : System.Exception
{
    public GameCacheInventoryPlayerException() { }
    public GameCacheInventoryPlayerException(string message) : base(message) { }
    public GameCacheInventoryPlayerException(string message, System.Exception inner) : base(message, inner) { }
    protected GameCacheInventoryPlayerException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}