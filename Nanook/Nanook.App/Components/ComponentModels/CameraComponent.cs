using SDL2;

namespace Nanook.App.Components.ComponentModels
{
    public class CameraComponent : Component
    {
        public TransformComponent Transform { get; set; } = null!;
        public int ScreenWidth { get; set; }
        public int ScreenHeight { get; set; }


        public CameraComponent(int screenWidth, int screenHeight)
        {
            ScreenWidth = screenWidth;
            ScreenHeight = screenHeight;
            Screen = new SDL.SDL_Rect()
            {
                x = 0,
                y = 0,
                w = screenWidth,
                h = screenHeight
            };
        }
        public SDL.SDL_Rect Screen { get; set; }
        public override void Init()
        {
            Transform = Entity.GetComponent<TransformComponent>();
        }
        public override void Update()
        {
            UpdateScreenLocation((int)Transform.Position.X - ScreenWidth / 2, (int)Transform.Position.Y - ScreenHeight / 2);
            if (Screen.x < 0)
            {
                UpdateScreenLocation(x: 0);
            }
            if (Screen.y < 0)
            {
                UpdateScreenLocation(y: 0);
            }
            if (Screen.x > Screen.w)
            {
                UpdateScreenLocation(x: Screen.w);
            }
            if (Screen.y > Screen.h)
            {
                UpdateScreenLocation(y: Screen.h);
            }
        }
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
