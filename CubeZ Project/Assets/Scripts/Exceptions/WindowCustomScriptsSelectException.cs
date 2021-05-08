[System.Serializable]
public class WindowCustomScriptsSelectException : System.Exception
{
    public WindowCustomScriptsSelectException() { }
    public WindowCustomScriptsSelectException(string message) : base(message) { }
    public WindowCustomScriptsSelectException(string message, System.Exception inner) : base(message, inner) { }
    protected WindowCustomScriptsSelectException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}