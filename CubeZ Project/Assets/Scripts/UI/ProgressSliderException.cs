[System.Serializable]
public class ProgressSliderException : System.Exception
{
    public ProgressSliderException() { }
    public ProgressSliderException(string message) : base(message) { }
    public ProgressSliderException(string message, System.Exception inner) : base(message, inner) { }
    protected ProgressSliderException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}