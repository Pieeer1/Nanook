﻿using SDL2;
namespace Nanook.App.Extensions
{
    public static class TextureExtension
    {
        public static IntPtr LoadTexture(string fileName)
        {
            IMG

            return SDL.SDL_CreateTextureFromSurface(Game.Instance.GetRendererReference());
        }
        public static void DrawTexture(IntPtr texture, SDL.SDL_Rect srcRect, SDL.SDL_Rect destRect)
        {
            SDL.SDL_RenderCopy(Game.Instance.GetRendererReference(),texture, ref srcRect, ref destRect);
        }
    }
}
