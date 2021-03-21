[System.Serializable]
public class GameCameraException : System.Exception
{
    public GameCameraException() { }
    public GameCameraException(string message) : base(message) { }
    public GameCameraException(string message, System.Exception inner) : base(message, inner) { }
    protected GameCameraException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}