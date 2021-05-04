[System.Serializable]
public class SkinDBManagerException : System.Exception
{
    public SkinDBManagerException() { }
    public SkinDBManagerException(string message) : base(message) { }
    public SkinDBManagerException(string message, System.Exception inner) : base(message, inner) { }
    protected SkinDBManagerException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}