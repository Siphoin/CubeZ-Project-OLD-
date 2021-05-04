[System.Serializable]
public class WallException : System.Exception
{
    public WallException() { }
    public WallException(string message) : base(message) { }
    public WallException(string message, System.Exception inner) : base(message, inner) { }
    protected WallException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}