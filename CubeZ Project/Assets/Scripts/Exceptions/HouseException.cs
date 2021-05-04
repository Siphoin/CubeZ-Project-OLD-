[System.Serializable]
public class HouseException : System.Exception
{
    public HouseException() { }
    public HouseException(string message) : base(message) { }
    public HouseException(string message, System.Exception inner) : base(message, inner) { }
    protected HouseException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}