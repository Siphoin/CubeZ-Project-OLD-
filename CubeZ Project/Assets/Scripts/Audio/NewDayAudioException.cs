[System.Serializable]
public class NewDayAudioException : System.Exception
{
    public NewDayAudioException() { }
    public NewDayAudioException(string message) : base(message) { }
    public NewDayAudioException(string message, System.Exception inner) : base(message, inner) { }
    protected NewDayAudioException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}