﻿namespace CBZ.API
{
    public struct Color
    {
        public float r;
        public float g;
        public float b;
        public float a;

        public Color (float r, float g, float b)
        {
            this.r = r;
            this.g = g;
            this.b = b;
            this.a = 1f;
        }

        public Color(float r, float g, float b, float a)
        {
            this.r = r;
            this.g = g;
            this.b = b;
            this.a = a;
        }

        public UnityEngine.Color ToUnityColor ()
        {
            return new UnityEngine.Color(r, g, b, a);
        }

        public override string ToString()
        {
            return $"RGBA: {r}, {g}, {b}, {a},";
        }
    }
}