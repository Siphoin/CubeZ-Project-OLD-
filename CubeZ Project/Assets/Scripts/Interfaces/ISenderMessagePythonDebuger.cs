using CBZ.API.Debug;
public interface ISenderMessagePythonDebuger
{
  void  NewMessage(string text, LogMessageType messageType = LogMessageType.Message);
    void Clear();
}