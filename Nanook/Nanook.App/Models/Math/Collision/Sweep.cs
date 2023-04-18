namespace Nanook.App.Models.Math.Collision
{
    /// <summary>
    /// sweep.hit is a Hit object if there was a collision, or null if not.
    /// sweep.pos is the furthest point the object reached along the swept path before it hit something.
    /// sweep.time is a copy of sweep.hit.time, offset by epsilon, or 1 if the object didn’t hit anything during the sweep.
    /// </summary>
    public class Sweep
    {
        public Hit? Hit { get; set; }
        public Vector2 Position { get; set; }
        public float Time { get; set; }
        public Sweep()
        {
            Position = Vector2.Zero;
            Time = 1.0f;
        }
    }
}
