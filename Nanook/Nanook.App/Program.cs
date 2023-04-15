using SDL2;
namespace Nanook.App;
public class Program
{
    public static void Main(string[] args)
    {
        int fps = 60;
        int frameDelay = 1000 / fps;

        uint frameStart;
        int frameTime;

        Game game = new Game(new GameBuilderSettings()
        {
            Title = "Nanook",
            XPosition = SDL.SDL_WINDOWPOS_CENTERED,
            YPosition = SDL.SDL_WINDOWPOS_CENTERED,
            Width = 840,
            Height = 600,
            IsFullScreen = false
        });

        game.Init();

        while (game.IsRunning)
        {
            frameStart = SDL.SDL_GetTicks();

            game.HandleEvents();
            game.Update();
            game.Render();

            frameTime = (int)(SDL.SDL_GetTicks() - frameStart);

            if (frameDelay > frameTime)
            {
                SDL.SDL_Delay((uint)(frameDelay - frameTime));
            }

        }


        game.Clean();
    }
}