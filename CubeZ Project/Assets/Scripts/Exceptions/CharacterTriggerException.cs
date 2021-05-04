[System.Serializable]
public class CharacterTriggerException : System.Exception
{
    public CharacterTriggerException() { }
    public CharacterTriggerException(string message) : base(message) { }
    public CharacterTriggerException(string message, System.Exception inner) : base(message, inner) { }
    protected CharacterTriggerException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}