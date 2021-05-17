[System.Serializable]
public class BloodException : System.Exception
{
    public BloodException() { }
    public BloodException(string message) : base(message) { }
    public BloodException(string message, System.Exception inner) : base(message, inner) { }
    protected BloodException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}