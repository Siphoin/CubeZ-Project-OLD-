[System.Serializable]
public class ShowAutoSaveUIException : System.Exception
{
    public ShowAutoSaveUIException() { }
    public ShowAutoSaveUIException(string message) : base(message) { }
    public ShowAutoSaveUIException(string message, System.Exception inner) : base(message, inner) { }
    protected ShowAutoSaveUIException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}