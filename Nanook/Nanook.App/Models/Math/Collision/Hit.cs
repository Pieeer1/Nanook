using SDL2;

namespace Nanook.App.Models.Math.Collision
{
    /// <summary>
    /// hit.pos is the point of contact between the two objects (or an estimation of it, in some sweep tests).
    /// hit.normal is the surface normal at the point of contact.
    /// hit.delta is the overlap between the two objects, and is a vector that can be added to the colliding object’s position to move it back to a non-colliding state.
    /// hit.time is only defined for segment and sweep intersections, and is a fraction from 0 to 1 indicating how far along the line the collision occurred. (This is the t value for the line equation L(t)=A+t(B−A))
    /// </summary>
    public class Hit
    {
        public Hit(AABB collider)
        {
            Collider = collider;
            Position = Vector2.Zero;
            Delta = Vector2.Zero;
            Normal = Vector2.Zero;
            Time = 0.0f;
        }

        public AABB Collider { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 Delta { get; set; }
        public Vector2 Normal { get; set; }
        public float Time { get; set; }
    }
}
