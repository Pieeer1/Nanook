namespace Nanook.App.Models.Math
{
    public class Vector2
    {
        public float X { get; set; }
        public float Y { get; set; }

        public Vector2()
        {
            X = 0.0f;
            Y = 0.0f;
        }
        public Vector2(float x, float y) 
        {
            X = x;
            Y = y;
        }

        public static Vector2 Zero => new Vector2(0.0f, 0.0f);

        public Vector2 Add(Vector2 v) 
        {
            X += v.X;
            Y += v.Y;
            return this;
        }
        public Vector2 Subtract(Vector2 v)
        {
            X -= v.X;
            Y -= v.Y;
            return this;
        }
        public Vector2 Multiply(Vector2 v) 
        {
            X *= v.X;
            Y *= v.Y;
            return this;
        }
        public Vector2 Divide(Vector2 v) 
        {
            X /= v.X;
            Y /= v.Y;
            return this;
        }
        public static Vector2 operator +(Vector2 v1, Vector2 v2)
        {
            return v1.Add(v2);
        }
        public static Vector2 operator -(Vector2 v1, Vector2 v2)
        {
            return v1.Subtract(v2);
        }
        public static Vector2 operator *(Vector2 v1, Vector2 v2)
        {
            return v1.Multiply(v2);
        }
        public static Vector2 operator /(Vector2 v1, Vector2 v2)
        {
            return v1.Divide(v2);
        }
    }
}
