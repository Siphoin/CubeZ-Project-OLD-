
using UnityEngine;

    public static class VectorExtension
    {

       public static bool NotValid (this Vector3 vector)
    {
        return float.IsNaN(vector.x) || float.IsNaN(vector.y) || float.IsNaN(vector.z);
    }

    public static bool NotValid(this Vector2 vector)
    {
        return float.IsNaN(vector.x) || float.IsNaN(vector.y);
    }
}