[System.Serializable]
public class AudioNightMusicException : System.Exception
{
    public AudioNightMusicException() { }
    public AudioNightMusicException(string message) : base(message) { }
    public AudioNightMusicException(string message, System.Exception inner) : base(message, inner) { }
    protected AudioNightMusicException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}