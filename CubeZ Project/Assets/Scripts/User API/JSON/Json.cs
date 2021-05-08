using Newtonsoft.Json;

namespace CBZ.API.Json
{
    public static class Json
    {
        public static string Parse (object value)
        {
            try
            {
                return JsonConvert.SerializeObject(value);
            }
            catch (System.Exception e)
            {
                Debug.Debug.Print($"json parse error: {e.Message}");
                return "";
            }
        }

        public static object Deserialize (string value)
        {
            try
            {
                return JsonConvert.DeserializeObject<object>(value);
            }
            catch (System.Exception e)
            {
                Debug.Debug.Print($"json deserialize error: {e.Message}");
                return "";
            }
        }
    }
}