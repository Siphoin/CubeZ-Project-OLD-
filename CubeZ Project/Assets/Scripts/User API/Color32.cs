namespace CBZ.API
{
    public struct Color32
    {
        public byte r;
        public byte g;
        public byte b;
        public byte a;

        public Color32(byte r, byte g, byte b)
        {
            this.r = r;
            this.g = g;
            this.b = b;
            this.a = 1;
        }

        public Color32(byte r, byte g, byte b, byte a)
        {
            this.r = r;
            this.g = g;
            this.b = b;
            this.a = a;
        }

        public UnityEngine.Color32 ToUnityColor32()
        {
            return new UnityEngine.Color32(r, g, b, a);
        }

        public override string ToString()
        {
            return $"RGBA32: {r}, {g}, {b}, {a},";
        }
    }
}