[System.Serializable]
public class WeaponStateUIException : System.Exception
{
    public WeaponStateUIException() { }
    public WeaponStateUIException(string message) : base(message) { }
    public WeaponStateUIException(string message, System.Exception inner) : base(message, inner) { }
    protected WeaponStateUIException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}