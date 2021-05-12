[System.Serializable]
public class UIStatsLevelControllerException : System.Exception
{
    public UIStatsLevelControllerException() { }
    public UIStatsLevelControllerException(string message) : base(message) { }
    public UIStatsLevelControllerException(string message, System.Exception inner) : base(message, inner) { }
    protected UIStatsLevelControllerException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}