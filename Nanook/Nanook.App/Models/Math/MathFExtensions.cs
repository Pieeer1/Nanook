using Nanook.App.Models.Math.Collision;
using SDL2;
namespace Nanook.App.Models.Math
{
    public static class MathFExtensions
    {
        public static float Clamp(float value, float min, float max)
        {
            if (value < min)
            {
                return min;
            }
            if (value > max)
            {
                return max;
            }
            return value;
        }
        public static AABB GetAABBFromSDLRect(this SDL.SDL_Rect rect) =>
            new AABB(new Vector2(rect.x + (rect.w / 2), rect.y + (rect.h / 2)), new Vector2(rect.w / 2, rect.h / 2));
    }
}
