[System.Serializable]
public class ShowAutoSaveUIControllerException : System.Exception
{
    public ShowAutoSaveUIControllerException() { }
    public ShowAutoSaveUIControllerException(string message) : base(message) { }
    public ShowAutoSaveUIControllerException(string message, System.Exception inner) : base(message, inner) { }
    protected ShowAutoSaveUIControllerException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}