[System.Serializable]
public class CharacterDataException : System.Exception
{
    public CharacterDataException() { }
    public CharacterDataException(string message) : base(message) { }
    public CharacterDataException(string message, System.Exception inner) : base(message, inner) { }
    protected CharacterDataException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}