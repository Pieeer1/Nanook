using Nanook.App.Extensions;
using Nanook.App.Models;
using SDL2;

namespace Nanook.App.Components.ComponentModels
{
    public class SpriteComponent : Component
    {
        private TransformComponent? transform { get; set; }
        private IntPtr texture { get; set; }
        private SDL.SDL_Rect srcRect { get; set; }
        private SDL.SDL_Rect destRect { get; set; }

        private int speed;
        private int frames;
        private int animationIndex { get; set; }
        private Dictionary<string, Animation> animationDictionary { get; set; } = new Dictionary<string, Animation>();
        public bool IsAnimated { get; private set; }
        public SDL.SDL_RendererFlip FlipFlag { get; set; } = SDL.SDL_RendererFlip.SDL_FLIP_NONE;

        public SpriteComponent(string path) 
        {
            SetTexture(path);
        }
        public SpriteComponent(string path, Dictionary<string, Animation> animations)
        {
            SetTexture(path);

            foreach (KeyValuePair<string, Animation> kvp in animations)
            {
                animationDictionary.Add(kvp.Key, kvp.Value);
            }
            PlayAnimation(animations.Keys.First());
            IsAnimated = true;
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
            if (IsAnimated)
            {
                srcRect = new SDL.SDL_Rect()
                {
                    x = srcRect.w * (int)((SDL.SDL_GetTicks() / speed) % frames),
                    y = animationIndex * transform!.Height,
                    w = srcRect.w,
                    h = srcRect.h
                };
            }


            destRect = new SDL.SDL_Rect()
            {
                x = (int)transform!.Position.X - Game.Instance.GetCameraReference().Screen.x,
                y = (int)transform!.Position.Y - Game.Instance.GetCameraReference().Screen.y,
                w = transform.Width * transform.Scale,
                h = transform.Height * transform.Scale
            };

        }

        public override void Draw()
        {
            TextureExtension.DrawTexture(texture, srcRect, destRect, FlipFlag);
        }
        public void SetTexture(string path)
        { 
            texture = TextureExtension.LoadTexture(path);
        }
        public void PlayAnimation(string animationName)
        {
            frames = animationDictionary[animationName].Frames;
            animationIndex = animationDictionary[animationName].Index;
            speed = animationDictionary[animationName].Speed;
        }
    }
}
