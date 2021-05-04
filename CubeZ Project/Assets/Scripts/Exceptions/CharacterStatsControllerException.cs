[System.Serializable]
public class CharacterStatsControllerException : System.Exception
{
    public CharacterStatsControllerException() { }
    public CharacterStatsControllerException(string message) : base(message) { }
    public CharacterStatsControllerException(string message, System.Exception inner) : base(message, inner) { }
    protected CharacterStatsControllerException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}