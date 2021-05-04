[System.Serializable]
public class CharacterRebelException : System.Exception
{
    public CharacterRebelException() { }
    public CharacterRebelException(string message) : base(message) { }
    public CharacterRebelException(string message, System.Exception inner) : base(message, inner) { }
    protected CharacterRebelException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}