[System.Serializable]
public class ResourcesWindowException : System.Exception
{
    public ResourcesWindowException() { }
    public ResourcesWindowException(string message) : base(message) { }
    public ResourcesWindowException(string message, System.Exception inner) : base(message, inner) { }
    protected ResourcesWindowException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}