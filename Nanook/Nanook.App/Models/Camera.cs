using SDL2;
namespace Nanook.App.Models
{
    public class Camera
    {
        public Camera(SDL.SDL_Rect screen)
        {
            Screen = screen;
        }
        public SDL.SDL_Rect Screen { get; set; }

        public void UpdateScreenLocation(int? x = null, int? y = null, int? w = null, int? h = null)
        {
            Screen = new SDL.SDL_Rect()
            {
                x = x ?? Screen.x,
                y = y ?? Screen.y,
                w = w ?? Screen.w,
                h = h ?? Screen.h
            };
        }
    }
}
