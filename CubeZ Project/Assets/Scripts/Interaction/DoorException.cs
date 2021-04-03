[System.Serializable]
public class DoorException : System.Exception
{
    public DoorException() { }
    public DoorException(string message) : base(message) { }
    public DoorException(string message, System.Exception inner) : base(message, inner) { }
    protected DoorException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}