[System.Serializable]
public class ZombieEyeControllerException : System.Exception
{
    public ZombieEyeControllerException() { }
    public ZombieEyeControllerException(string message) : base(message) { }
    public ZombieEyeControllerException(string message, System.Exception inner) : base(message, inner) { }
    protected ZombieEyeControllerException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}