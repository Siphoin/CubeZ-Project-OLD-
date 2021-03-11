[System.Serializable]
public class UIStatsWorldControllerException : System.Exception
{
    public UIStatsWorldControllerException() { }
    public UIStatsWorldControllerException(string message) : base(message) { }
    public UIStatsWorldControllerException(string message, System.Exception inner) : base(message, inner) { }
    protected UIStatsWorldControllerException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}