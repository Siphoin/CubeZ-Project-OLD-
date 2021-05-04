[System.Serializable]
public class ButtonCategoryBuildException : System.Exception
{
    public ButtonCategoryBuildException() { }
    public ButtonCategoryBuildException(string message) : base(message) { }
    public ButtonCategoryBuildException(string message, System.Exception inner) : base(message, inner) { }
    protected ButtonCategoryBuildException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}