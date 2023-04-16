﻿using Nanook.App.Components;
using Nanook.App.Components.ComponentModels;
using Nanook.App.Models.Math;
using SDL2;
using System.Numerics;

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
        private List<ColliderComponent> colliders { get; set; } = new List<ColliderComponent>();
        private IntPtr window { get; set; }
        private IntPtr renderer { get; set; }
        private SDL.SDL_Event @event;

        Entity? player = null;
        Entity? wall = null;
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

            player = entityComponentManager.AddEntity();
            player.AddComponent<TransformComponent>(new TransformComponent(2));
            player.AddComponent<SpriteComponent>(new SpriteComponent("../../../Sprites/BaseCharacter.png"));
            player.AddComponent<KeyboardControlComponent>(new KeyboardControlComponent());
            player.AddComponent<ColliderComponent>(new ColliderComponent("player"));

            wall = entityComponentManager.AddEntity();
            wall.AddComponent<TransformComponent>(new TransformComponent(300.0f, 300.0f, 300, 20, 1));
            wall.AddComponent<SpriteComponent>(new SpriteComponent("../../../Sprites/Dirt.png"));
            wall.AddComponent<ColliderComponent>(new ColliderComponent("wall"));

        }

        public void HandleEvents()
        {
            SDL.SDL_PollEvent(out @event);
            switch (@event.type)
            {
                case SDL.SDL_EventType.SDL_QUIT:
                    IsRunning = false;
                    break;
                default:
                    break;
            }

        }
        public IntPtr GetRendererReference() => renderer;
        public SDL.SDL_Event GetEventReference() => @event;
        public List<ColliderComponent> GetColliderComponentsReference() => colliders;
        public void Update()
        {
            entityComponentManager.Refresh();
            entityComponentManager.Update();

            foreach (ColliderComponent cc in colliders)
            {
                if (cc.Tag != player?.GetComponent<ColliderComponent>().Tag)
                {
                    Collision.AABBIsColliding(player?.GetComponent<ColliderComponent>() ?? throw new NullReferenceException("Player Does not Exist"), cc);
                }                
            }
        }

        public void AddTile(int id, int x, int y)
        {
            Entity tile = entityComponentManager.AddEntity();
            tile.AddComponent<TileComponent>(new TileComponent(x, y, 32, 32, id));
            
        }

        public void Render()
        {
            SDL.SDL_RenderClear(renderer);

            player?.Draw();
            wall?.Draw();

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
