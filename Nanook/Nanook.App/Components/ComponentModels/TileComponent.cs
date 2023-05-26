using Nanook.App.Extensions;
using Nanook.App.Models.Math;
using SDL2;
namespace Nanook.App.Components.ComponentModels
{
    public class TileComponent : Component
    {
        public IntPtr texture;
        public SDL.SDL_Rect srcRect;
        public SDL.SDL_Rect destRect;
        public Vector2 Position { get; set; } = Vector2.Zero;
        public TileComponent(int srcX, int srcY, int xPos, int yPos, string path)
        {
            texture = TextureExtension.LoadTexture(path);

            Position = new Vector2(xPos, yPos);

            srcRect = new SDL.SDL_Rect()
            {
                x = srcX,
                y = srcY,
                w = 32,
                h = 32,
            };
            destRect = new SDL.SDL_Rect()
            {
                x = xPos,
                y = yPos,
                w = 32,
                h = 32,
            };
        }
        public override void Update()
        {
            destRect = new SDL.SDL_Rect()
            {
                x = (int)(Position.X - (Game.Instance.GetCameraReference()?.Screen.x ?? 0)),
                y = (int)(Position.Y - (Game.Instance.GetCameraReference()?.Screen.y ?? 0)),
                w = destRect.w,
                h = destRect.h
            };
        }
        public override void Draw()
        {
            TextureExtension.DrawTexture(texture, srcRect, destRect, SDL.SDL_RendererFlip.SDL_FLIP_NONE);
        }
    }
}
