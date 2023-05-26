using SDL2;
using System;

namespace Nanook.App.Models.Math.Collision
{
    public static class CollisionExtension // NOTE TO FUTURE MASON - MAY NOT NEED TO DIVIDE HEIGHT AND WITH BY TWO: https://noonat.github.io/intersect/#intersection-tests ASSUMING RN THAT HALF = H/2 AND W/2
    {
        /// <summary>
        /// If a point is behind all of the edges of the box, it’s colliding.
        /// The function returns a Hit object, or null if the two do not collide.
        /// Hit.Pos and Hit.Delta will be set to nearest edge of the box
        /// This code first finds the overlap on the X and Y axis. If the overlap is less than zero for either, a collision is not possible. 
        /// Otherwise, we find the axis with the smallest overlap and use that to create an intersection point on the edge of the box.
        /// </summary>
        public static Hit? IntersectionPoint(this AABB aabb, Vector2 point) 
        {
            float dx = point.X - aabb.Position.X;
            float px = aabb.Half.X - MathF.Abs(dx);
            if (px <= 0)
            {
                return null;
            }

            float dy = point.Y - aabb.Position.Y;
            float py = aabb.Half.Y - MathF.Abs(dy);
            if (py <= 0) 
            {
                return null;
            }

            Hit hit = new Hit(aabb);
            if (px < py)
            {
                int sx = MathF.Sign(dx);
                hit.Delta.X = px * sx;
                hit.Normal.X = sx;
                hit.Position.X = aabb.Position.X + ((aabb.Half.X) * sx);
                hit.Position.Y = point.Y;
            }
            else
            { 
                int sy = MathF.Sign(dy);
                hit.Delta.Y = py * sy;
                hit.Normal.Y = sy;
                hit.Position.X = point.X;
                hit.Position.Y = aabb.Position.Y + ((aabb.Half.Y) * sy);
            }
            return hit;
        }
        /// <summary>
        /// Calculates Collision times along the line for each edge of the box and returns a hit object with the time property if the two overlap
        /// Returns Null if the two do not overlap
        /// Padding x and y are added to the radius of the bounding box
        /// A segment from point A to point B can be expressed with the equation S(t)=A+t(B−A), for 0<=t<=1. 
        /// In this equation,t is the time along the line, or percentage distance from A to B.
        /// Instead of formalizing the concept of a segment, we use this equation and create a variable pos with the value of A, and a variable delta with the value of B−A.
        /// </summary>
        public static Hit? IntersectSegment(this AABB aabb, Vector2 pos, Vector2 delta, float paddingX, float paddingY)
        {
            float scaleX = 1.0f / delta.X;
            float scaleY = 1.0f / delta.Y;

            int signX = MathF.Sign(scaleX);
            int signY = MathF.Sign(scaleY);

            float nearTimeX = (aabb.Position.X - signX * ((aabb.Position.X/2) - paddingX ) - pos.X) * scaleX;
            float nearTimeY = (aabb.Position.Y - signY * ((aabb.Position.Y/2) - paddingY) - pos.Y) * scaleY;

            float farTimeX = (aabb.Position.X + signX * ((aabb.Half.X) + paddingX) - pos.X) * scaleX;
            float farTimeY = (aabb.Position.Y + signY * ((aabb.Half.Y) + paddingY) - pos.Y) * scaleY;

            if (nearTimeX > farTimeY || nearTimeY > farTimeX)
            {
                return null;
            }
            
            float nearTime = nearTimeX > nearTimeY ? nearTimeX : nearTimeY;
            float farTime = farTimeX < farTimeY ? farTimeX : farTimeY;

            if (nearTime >= 1 || farTime <= 0)
            {
                return null;
            }

            Hit hit = new Hit(aabb);
            hit.Time = MathFExtensions.Clamp(nearTime, 0, 1);
            if (nearTimeX > nearTimeY)
            {
                hit.Normal.X = -signX;
                hit.Normal.Y = 0.0f;
            }
            else
            {
                hit.Normal.X = 0.0f;
                hit.Normal.Y = -signY;
            }
            hit.Delta.X = (1.0f - hit.Time) * -delta.X;
            hit.Delta.Y = (1.0f - hit.Time) * -delta.Y;
            hit.Position.X = pos.X + delta.X * hit.Time;
            hit.Position.Y = pos.Y + delta.Y * hit.Time;
            return hit;
        }
        /// <summary>
        /// The function returns a Hit object, or null if the two static boxes do not overlap, and gives the axis of least overlap as the contact point. 
        /// That is, it sets hit.delta so that the colliding box will be pushed out of the nearest edge. This can cause weird behavior for moving boxes, so you should use sweepAABB instead for moving boxes.
        /// </summary>
        public static Hit? IntersectAABB(this AABB aabb, AABB aabb2)
        { 
            float dx = aabb2.Position.X - aabb.Position.X;
            float px = (aabb2.Half.X + aabb.Half.X - MathF.Abs(dx));
            if (px <= 0)
            {
                return null;
            }

            float dy = aabb2.Position.Y - aabb.Position.Y;
            float py = (aabb2.Half.Y + aabb.Half.Y) - MathF.Abs(dy);
            if (py <= 0)
            {
                return null;
            }

            Hit hit = new Hit(aabb);
            if (px < py)
            {
                int sx = MathF.Sign(dx);
                hit.Delta.X = px * sx;
                hit.Normal.X = sx;
                hit.Position.X = aabb.Position.X + (aabb.Half.X * sx);
                hit.Position.Y = aabb2.Position.Y;
            }
            else
            {
                int sy = MathF.Sign(dy);
                hit.Delta.Y = py * sy;
                hit.Normal.Y = sy;
                hit.Position.X = aabb2.Position.X;
                hit.Position.Y = aabb.Position.Y - (aabb.Half.Y * sy);
            }
            return hit;
        }
        /// <summary>
        /// sweepAABB finds the intersection of this box and another moving box, where the delta argument is a point describing the movement of the box. 
        /// It returns a Sweep object. sweep.hit will be a Hit object if the two collided, or null if they did not overlap.
        /// </summary>

        public static Sweep SweepAABB(this AABB aabb, AABB aabb2, Vector2 delta)
        {
            Sweep sweep = new Sweep();
            if (delta.X == 0 && delta.Y == 0) // if sweep is not moving, just do static test
            {
                sweep.Position.X = aabb.Position.X;
                sweep.Position.Y = aabb.Position.Y;
                sweep.Hit = aabb.IntersectAABB(aabb2);
                sweep.Time = sweep.Hit != null ? (sweep.Hit.Time = 0) : 1;
                return sweep;
            }
            sweep.Hit = aabb.IntersectSegment(new Vector2(aabb2.Position.X, aabb2.Position.Y), delta, aabb2.Half.X, aabb2.Half.Y);
            if (sweep.Hit is not null)
            {
                sweep.Time = MathFExtensions.Clamp(sweep.Hit.Time - (1e-8f), 0, 1);
                sweep.Position.X = aabb2.Position.X + delta.X * sweep.Time;
                sweep.Position.Y = aabb2.Position.Y + delta.Y * sweep.Time;
                Vector2 direction = new Vector2(delta.X, delta.Y);
                direction.Normalize();
                sweep.Hit.Position.X = MathFExtensions.Clamp(sweep.Hit.Position.X + direction.X * aabb2.Half.X, aabb.Position.X - aabb.Half.X, aabb.Position.X + aabb.Half.X);
                sweep.Hit.Position.Y = MathFExtensions.Clamp(sweep.Hit.Position.Y + direction.Y * aabb.Half.Y, aabb2.Position.Y - aabb.Half.Y, aabb.Position.Y + aabb.Half.Y);
            }
            else
            {
                sweep.Position.X = aabb2.Position.X + delta.X;
                sweep.Position.Y = aabb2.Position.Y + delta.Y;
                sweep.Time = 1;
            }
            return sweep;
        }
        /// <summary>
        /// AABB we want to move from one point to another, without allowing it to collide with a list of static AABBs
        /// </summary>

        public static Sweep SweepInto(this AABB aabb, AABB[] colliders, Vector2 delta)
        {
            Sweep nearest = new Sweep();
            nearest.Time = 1.0f;
            nearest.Position.X = aabb.Position.X + delta.X;
            nearest.Position.Y = aabb.Position.Y + delta.Y;
            for (int i = 0; i < colliders.Length; i++)
            {
                Sweep sweep = colliders[i].SweepAABB(aabb, delta);
                if (sweep.Time < nearest.Time)
                {
                    nearest = sweep;
                }
            }
            return nearest;
        }
    }
}
