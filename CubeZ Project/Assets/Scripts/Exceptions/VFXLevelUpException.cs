[System.Serializable]
public class VFXLevelUpException : System.Exception
{
    public VFXLevelUpException() { }
    public VFXLevelUpException(string message) : base(message) { }
    public VFXLevelUpException(string message, System.Exception inner) : base(message, inner) { }
    protected VFXLevelUpException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}