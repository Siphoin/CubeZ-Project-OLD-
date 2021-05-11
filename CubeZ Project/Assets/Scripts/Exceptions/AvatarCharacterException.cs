[System.Serializable]
public class AvatarCharacterException : System.Exception
{
    public AvatarCharacterException() { }
    public AvatarCharacterException(string message) : base(message) { }
    public AvatarCharacterException(string message, System.Exception inner) : base(message, inner) { }
    protected AvatarCharacterException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}