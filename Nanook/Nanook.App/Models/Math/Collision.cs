using Nanook.App.Components.ComponentModels;
using SDL2;
using System.Diagnostics;

namespace Nanook.App.Models.Math
{
    public static class Collision
    {
        public static bool AABBIsColliding(SDL.SDL_Rect rect1, SDL.SDL_Rect rect2) => rect1.x + rect1.w >= rect2.x && rect2.x + rect2.w >= rect1.x && rect1.y + rect1.h >= rect2.y && rect2.y + rect2.h >= rect1.y;


        /// <summary>
        /// Returns Colliding Wall on Rect1 Rect 2 is most Colliding With
        /// </summary>
        /// <param name="rect1"></param>
        /// <param name="rect2"></param>
        /// <returns>boolean is colliding, as well as an index
        ///   1
        ///   -
        /// 0| |2
        ///   - 
        ///   3
        /// </returns>
        public static int GetCollidingWall(SDL.SDL_Rect rect1, SDL.SDL_Rect rect2)
        {
            if (AABBIsColliding(rect1, rect2))
            {
                if (rect2.y - rect1.y > 0 && (MathF.Abs(rect2.y - rect1.y) > MathF.Abs(rect2.x - rect1.x)))
                {
                    return 1;
                }
                else if (rect2.y - rect1.y < 0 && (MathF.Abs(rect2.y - rect1.y) > MathF.Abs(rect2.x - rect1.x)))
                {
                    return 3;
                }
                else if (rect2.x - rect1.x > 0 && (MathF.Abs(rect2.x - rect1.x) > MathF.Abs(rect2.y - rect1.y)))
                {
                    return 0;
                }
                else if (rect2.x - rect1.x < 0 && (MathF.Abs(rect2.x - rect1.x) > MathF.Abs(rect2.y - rect1.y)))
                {
                    return 2;
                }
                return -1;
            }
            else
            {
                return -1;
            }
        }

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

        public static void JoinColliders(this ColliderComponent colA, ColliderComponent colB)
        {
            colA.Collider = new SDL.SDL_Rect()
            {
                x = colA.Collider.x,
                y = colA.Collider.y,
                w = colA.Collider.w + colB.Collider.w,
                h = colA.Collider.h + colB.Collider.h
            };
        }

    }
}
