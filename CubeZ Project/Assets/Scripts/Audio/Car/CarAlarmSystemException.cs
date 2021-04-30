[System.Serializable]
public class CarAlarmSystemException : System.Exception
{
    public CarAlarmSystemException() { }
    public CarAlarmSystemException(string message) : base(message) { }
    public CarAlarmSystemException(string message, System.Exception inner) : base(message, inner) { }
    protected CarAlarmSystemException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}