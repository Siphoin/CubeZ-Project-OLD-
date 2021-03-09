[System.Serializable]
public class UIStatsControllerException : System.Exception
{
    public UIStatsControllerException() { }
    public UIStatsControllerException(string message) : base(message) { }
    public UIStatsControllerException(string message, System.Exception inner) : base(message, inner) { }
    protected UIStatsControllerException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}