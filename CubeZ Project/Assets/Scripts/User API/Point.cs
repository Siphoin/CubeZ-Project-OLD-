using System.Collections;
using UnityEngine;

namespace CBZ.API
{
    /// <summary>
    /// Point in 3D space
    /// </summary>
    [System.Serializable]
   
    public struct Point
    {
        public float x;
        public float y;
        public float z;

        public Point (float x, float y)
        {
            this.x = x;
            this.y = y;
            z = 0;
        }

        public Point(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

       public Vector3 ToVector3 ()
        {
            return new Vector3(x, y, z);
        }

        public float Distance (Point a, Point b)
        {
            return Vector3.Distance(a.ToVector3(), b.ToVector3());
        }

        public Point Normalize ()
        {
            Vector3 vector = this.ToVector3();
            Vector3 normalizeVector = vector.normalized;
            return new Point(normalizeVector.x, normalizeVector.y, normalizeVector.z);
        }
    }

}