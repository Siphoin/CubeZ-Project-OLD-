namespace CBZ.API.Random
{
    /// <summary>
    /// Generation Random numbers
    /// </summary>
    public static class Random
    {
        public static int RandomValueInt ()
        {
          return  UnityEngine.Random.Range(int.MinValue, int.MaxValue);
        }

        public static float RandomValueFloat()
        {
          return  UnityEngine.Random.Range(float.MinValue, float.MaxValue);
        }

        public static int RandomValueInt(int min, int max)
        {
            return UnityEngine.Random.Range(min, max + 1);
        }

        public static float RandomValueFloat(float min, float max)
        {
            return UnityEngine.Random.Range(min, max + 1.0f);
        }

    }
}