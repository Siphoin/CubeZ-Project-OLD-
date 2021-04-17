[System.Serializable]
public class FastButtonOpenWindowException : System.Exception
{
    public FastButtonOpenWindowException() { }
    public FastButtonOpenWindowException(string message) : base(message) { }
    public FastButtonOpenWindowException(string message, System.Exception inner) : base(message, inner) { }
    protected FastButtonOpenWindowException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}