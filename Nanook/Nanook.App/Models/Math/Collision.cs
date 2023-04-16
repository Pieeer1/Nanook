using Nanook.App.Components.ComponentModels;
using SDL2;
using System.Diagnostics;

namespace Nanook.App.Models.Math
{
    public static class Collision
    {
        public static bool AABBIsColliding(SDL.SDL_Rect rect1, SDL.SDL_Rect rect2) => rect1.x + rect1.w >= rect2.x && rect2.x + rect2.w >= rect1.x && rect1.y + rect1.h >= rect2.y && rect2.y + rect2.h >= rect1.y;


        //public static bool AABBIsColliding(ColliderComponent colA, ColliderComponent colB) => AABBIsColliding(colA.Collider, colB.Collider);
        public static bool AABBIsColliding(ColliderComponent colA, ColliderComponent colB)
        {
            bool outy = AABBIsColliding(colA.Collider, colB.Collider);
            if (outy)
            {
                Debug.WriteLine($"{colA.Tag} HIT {colB.Tag}");
            }
            return outy;
        } 
    }
}
