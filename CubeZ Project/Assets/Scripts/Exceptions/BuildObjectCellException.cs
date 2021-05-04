[System.Serializable]
public class BuildObjectCellException : System.Exception
{
    public BuildObjectCellException() { }
    public BuildObjectCellException(string message) : base(message) { }
    public BuildObjectCellException(string message, System.Exception inner) : base(message, inner) { }
    protected BuildObjectCellException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}