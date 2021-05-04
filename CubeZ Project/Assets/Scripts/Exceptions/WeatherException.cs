[System.Serializable]
public class WeatherException : System.Exception
{
    public WeatherException() { }
    public WeatherException(string message) : base(message) { }
    public WeatherException(string message, System.Exception inner) : base(message, inner) { }
    protected WeatherException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}