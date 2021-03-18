[System.Serializable]
public class SkinMaterialException : System.Exception
{
    public SkinMaterialException() { }
    public SkinMaterialException(string message) : base(message) { }
    public SkinMaterialException(string message, System.Exception inner) : base(message, inner) { }
    protected SkinMaterialException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}