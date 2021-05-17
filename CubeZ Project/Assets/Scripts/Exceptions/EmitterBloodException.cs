[System.Serializable]
public class EmitterBloodException : System.Exception
{
    public EmitterBloodException() { }
    public EmitterBloodException(string message) : base(message) { }
    public EmitterBloodException(string message, System.Exception inner) : base(message, inner) { }
    protected EmitterBloodException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}