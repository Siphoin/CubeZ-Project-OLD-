
namespace CBZ.API.Debug
{
    public static class Debug
    {
        public static void Print(object message, LogMessageType messageType = LogMessageType.Message)
        {
            try
            {
            DebugerPython.ActiveDebuger.NewMessage(message.ToString(), messageType);
            }
            catch
            {

            }

        }

        public static void ClearDeveloperConsole ()
        {
            try
            {
                DebugerPython.ActiveDebuger.Clear();
            }
            catch
            {

            }
        }

        }
    }

