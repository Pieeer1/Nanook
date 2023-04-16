using Nanook.App.Extensions;
using SDL2;

namespace Nanook.App.Components.ComponentModels
{
    public class SpriteComponent : Component
    {
        private TransformComponent? transform { get; set; }
        private IntPtr texture { get; set; }
        private SDL.SDL_Rect srcRect { get; set; }
        private SDL.SDL_Rect destRect { get; set; }
        public SpriteComponent(string path) 
        {
            SetTexture(path);
        }

        public override void Init()
        {
            if (!Entity.HasComponent<TransformComponent>())
            {
                Entity.AddComponent<TransformComponent>(new TransformComponent());
            }
            transform = Entity.GetComponent<TransformComponent>();

            srcRect = new SDL.SDL_Rect()
            {
                x = 0,
                y = 0,
                w = transform.Width,
                h = transform.Height
            };
        }

        public override void Update()
        {
            destRect = new SDL.SDL_Rect()
            {
                x = (int)transform!.Position.X,
                y = (int)transform!.Position.Y,
                w = transform.Width * transform.Scale,
                h = transform.Height * transform.Scale
            };

        }

        public override void Draw()
        {
            TextureExtension.DrawTexture(texture, srcRect, destRect);
        }
        public void SetTexture(string path)
        { 
            texture = TextureExtension.LoadTexture(path);
        }
    }
}
