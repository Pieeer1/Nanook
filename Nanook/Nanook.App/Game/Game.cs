using Nanook.App.Components;
using Nanook.App.Components.ComponentModels;
using SDL2;

namespace Nanook.App
{
    public sealed class Game
    {
        public Game(string title, int xPosition, int yPosition, int width, int height, bool isFullScreen)
        {
            Title = title;
            XPosition = xPosition;
            YPosition = yPosition;
            Width = width;
            Height = height;
            IsFullScreen = isFullScreen;

            instance = this;
        }
        public Game(GameBuilderSettings settings)
        { 
            Title=  settings.Title;
            XPosition = settings.XPosition;
            YPosition = settings.YPosition;
            Width = settings.Width;
            Height = settings.Height;
            IsFullScreen = settings.IsFullScreen;

            instance = this;
        }
        private static Game? instance = null;
        public static Game Instance 
        {
            get 
            {
                if (instance is null)
                {
                    instance = new Game(new GameBuilderSettings());
                }
                return instance;
            }
        }

        public string Title { get; private set; } = "Default Title";
        public int XPosition { get; private set; } = 0;
        public int YPosition { get; private set; } = 0;
        public int Width { get; private set; } = 800;
        public int Height { get; private set; } = 640;
        public bool IsFullScreen { get; private set; } = false;
        public bool IsRunning { get; private set; }

        private EntityComponentManager entityComponentManager = new EntityComponentManager();
        private IntPtr window { get; set; }
        private IntPtr renderer { get; set; }
        public void Init()
        {

            SDL.SDL_SetHint(SDL.SDL_HINT_WINDOWS_DISABLE_THREAD_NAMING, "1");
            if (SDL.SDL_Init(SDL.SDL_INIT_EVERYTHING) == 0)
            {
                window = SDL.SDL_CreateWindow(Title, XPosition, YPosition, Width, Height, IsFullScreen ? SDL.SDL_WindowFlags.SDL_WINDOW_FULLSCREEN : 0);

                renderer = SDL.SDL_CreateRenderer(window, -1, 0);

                SDL.SDL_SetRenderDrawColor(renderer, 255, 255, 255, 255);

                IsRunning  = true;
            }
            else
            {
                throw new InvalidProgramException("Cannot Start SDL Window");
            }

            var player = entityComponentManager.AddEntity();
            player.AddComponent<TransformComponent>(new TransformComponent());
            player.GetComponent<TransformComponent>();
        }

        public void HandleEvents()
        {
            SDL.SDL_PollEvent(out SDL.SDL_Event eve);
            switch (eve.type)
            {
                case SDL.SDL_EventType.SDL_QUIT:
                    IsRunning = false;
                    break;
                default:
                    break;
            }

        }
        public IntPtr GetRendererReference() => renderer;
        public void Update()
        {
            entityComponentManager.Refresh();
            entityComponentManager.Update();
        }

        public void Render()
        {
            SDL.SDL_RenderClear(renderer);

            SDL.SDL_RenderPresent(renderer);
        }
        public void Clean()
        {
            SDL.SDL_DestroyWindow(window);
            SDL.SDL_DestroyRenderer(renderer);
            SDL.SDL_Quit();
        }

    }
}
