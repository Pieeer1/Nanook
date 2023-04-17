using SDL2;
namespace Nanook.App.Extensions
{
    public static class TextureExtension
    {
        public static IntPtr LoadTexture(string fileName)
        {
            return SDL_image.IMG_LoadTexture(Game.Instance.GetRendererReference(), fileName);
        }
        public static void DrawTexture(IntPtr texture, SDL.SDL_Rect srcRect, SDL.SDL_Rect destRect, SDL.SDL_RendererFlip flip)
        {
            SDL.SDL_Point refPoint = new SDL.SDL_Point();
            SDL.SDL_RenderCopyEx(Game.Instance.GetRendererReference(),texture, ref srcRect, ref destRect, 0.0d, ref refPoint, flip);
        }
    }
}
