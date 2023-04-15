namespace Nanook.App.Models.Math
{
    public class Vector2
    {
        public float X { get; set; }
        public float Y { get; set; }

        public Vector2()
        {
            
        }
        public Vector2(float x, float y) 
        {
        
        }

        public Vector2 Add(Vector2 v) 
        {
        
        }
        public Vector2 Subtract(Vector2 v)
        { 
        
        }
        public Vector2 Multiply(Vector2 v) 
        {
        
        }
        public Vector2 Divide(Vector2 v) 
        {
        
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
