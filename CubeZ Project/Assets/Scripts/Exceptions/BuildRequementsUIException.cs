[System.Serializable]
public class BuildRequementsUIException : System.Exception
{
    public BuildRequementsUIException() { }
    public BuildRequementsUIException(string message) : base(message) { }
    public BuildRequementsUIException(string message, System.Exception inner) : base(message, inner) { }
    protected BuildRequementsUIException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}