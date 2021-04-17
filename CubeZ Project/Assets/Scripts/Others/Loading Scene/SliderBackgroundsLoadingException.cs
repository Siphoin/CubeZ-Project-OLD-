[System.Serializable]
public class SliderBackgroundsLoadingException : System.Exception
{
    public SliderBackgroundsLoadingException() { }
    public SliderBackgroundsLoadingException(string message) : base(message) { }
    public SliderBackgroundsLoadingException(string message, System.Exception inner) : base(message, inner) { }
    protected SliderBackgroundsLoadingException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}