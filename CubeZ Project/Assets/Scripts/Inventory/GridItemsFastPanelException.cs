[System.Serializable]
public class GridItemsFastPanelException : System.Exception
{
    public GridItemsFastPanelException() { }
    public GridItemsFastPanelException(string message) : base(message) { }
    public GridItemsFastPanelException(string message, System.Exception inner) : base(message, inner) { }
    protected GridItemsFastPanelException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}