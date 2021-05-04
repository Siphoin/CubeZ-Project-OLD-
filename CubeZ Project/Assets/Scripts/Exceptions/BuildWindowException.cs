[System.Serializable]
public class BuildWindowException : System.Exception
{
    public BuildWindowException() { }
    public BuildWindowException(string message) : base(message) { }
    public BuildWindowException(string message, System.Exception inner) : base(message, inner) { }
    protected BuildWindowException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}