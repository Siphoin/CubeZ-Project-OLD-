using System.Collections;
using UnityEngine;

namespace CBZ.API
{
    /// <summary>
    /// Rotation in 3D space
    /// </summary>
    [System.Serializable]

    public struct Angle
    {
        public float x;
        public float y;
        public float z;

        public Angle(float x, float y)
        {
            this.x = x;
            this.y = y;
            z = 0;
        }

        public Angle(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public Quaternion ToQuaternion()
        {
            return Quaternion.Euler(new Vector3(x, y, z));
        }

        public override string ToString()
        {
            return $"x: {x} y: {y} z: {z}";
        }
    }

}