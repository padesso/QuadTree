namespace QuadTreeTDD
{
    public class Vector
    {
        private float x;
        private float y;

        public Vector(float x, float y)
        {
            this.X = x;
            this.Y = y;
        }

        public float X { get => x; set => x = value; }
        public float Y { get => y; set => y = value; }
    }
}