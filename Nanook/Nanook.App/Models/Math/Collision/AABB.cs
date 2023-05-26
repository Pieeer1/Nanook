namespace Nanook.App.Models.Math.Collision
{
    public class AABB
    {
        public AABB(Vector2 position, Vector2 half)
        {
            Position = position;
            Half = half;
        }

        //center of the box
        public Vector2 Position { get; set; }
        //'radius' or half size
        public Vector2 Half { get; set; }

    }
}
