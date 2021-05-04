[System.Serializable]
public class SkinSettingsException : System.Exception
{
    public SkinSettingsException() { }
    public SkinSettingsException(string message) : base(message) { }
    public SkinSettingsException(string message, System.Exception inner) : base(message, inner) { }
    protected SkinSettingsException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}