using Nanook.App.Models.Math;
using System.Data;
using System.Diagnostics;

namespace Nanook.App.Components.ComponentModels
{
    public class TransformComponent : Component
    {

        public Vector2 Position { get; set; }
        public Vector2 Velocity { get; set; }
        public int Speed = 3;
        public int Height = 32;
        public int Width = 32;
        public int Scale = 1;

        public TransformComponent() 
        {
            Position = Vector2.Zero;
            Velocity = Vector2.Zero;
        }
        public TransformComponent(int scale)
        {
            Position = Vector2.Zero;
            Velocity = Vector2.Zero;
            Scale = scale;
        }
        public TransformComponent(float x, float y)
        { 
            Position = new Vector2(x, y);
            Velocity = Vector2.Zero;
        }
        public TransformComponent(float x, float y, int height, int width, int scale)
        {
            Position = new Vector2(x, y);
            Velocity = Vector2.Zero;
            Height = height;
            Width = width;
            Scale = scale;
        }
        public TransformComponent(Vector2 position, int height, int width, int scale)
        {
            Position = position;
            Velocity = Vector2.Zero;
            Height = height;
            Width = width;
            Scale = scale;
        }

        public override void Init()
        {
            Velocity = Vector2.Zero;
        }
        public override void Update()
        {
            Position.X += Velocity.X * Speed;
            Position.Y += Velocity.Y * Speed;
        }
        public override void Draw()
        {
            
        }
    }
}
