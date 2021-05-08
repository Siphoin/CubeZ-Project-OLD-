
namespace CBZ.API.Debug
{
    public static class Debug
    {
        public static void Print(object message, LogMessageType messageType = LogMessageType.Message)
        {
            DebugerPython.ActiveDebuger.NewMessage(message.ToString(), messageType);
        }

        public static void ClearDeveloperConsole ()
        {
            DebugerPython.ActiveDebuger.Clear();
        }

        }
    }

