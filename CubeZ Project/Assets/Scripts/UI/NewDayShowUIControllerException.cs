[System.Serializable]
public class NewDayShowUIControllerException : System.Exception
{
    public NewDayShowUIControllerException() { }
    public NewDayShowUIControllerException(string message) : base(message) { }
    public NewDayShowUIControllerException(string message, System.Exception inner) : base(message, inner) { }
    protected NewDayShowUIControllerException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}