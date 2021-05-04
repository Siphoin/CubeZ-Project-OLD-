[System.Serializable]
public class HouseAreaException : System.Exception
{
    public HouseAreaException() { }
    public HouseAreaException(string message) : base(message) { }
    public HouseAreaException(string message, System.Exception inner) : base(message, inner) { }
    protected HouseAreaException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}