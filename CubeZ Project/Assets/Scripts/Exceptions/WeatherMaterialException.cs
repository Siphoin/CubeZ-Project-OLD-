[System.Serializable]
public class WeatherMaterialException : System.Exception
{
    public WeatherMaterialException() { }
    public WeatherMaterialException(string message) : base(message) { }
    public WeatherMaterialException(string message, System.Exception inner) : base(message, inner) { }
    protected WeatherMaterialException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}