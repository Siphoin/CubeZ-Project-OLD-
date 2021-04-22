public struct SerializeColor
    {
        public float r;
        public float g;
        public float b;
        public float a;

        public SerializeColor (float r, float g, float b, float a)
        {
            this.r = r;
            this.b = b;
            this.g = g;
            this.a = a;
        }

    public SerializeColor(UnityEngine.Color color)
    {
        this.r = color.r;
        this.b = color.b;
        this.g = color.g;
        this.a = color.a;
    }
}
