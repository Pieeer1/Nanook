﻿using Nanook.App.Components;
using Nanook.App.Components.ComponentModels;
using Nanook.App.Enums;
using Nanook.App.Models;
using Nanook.App.Models.Math;
using SDL2;
using System.Diagnostics;
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
            Title = settings.Title;
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

        private CameraComponent? camera = null;
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

                IsRunning = true;
            }
            else
            {
                throw new InvalidProgramException("Cannot Start SDL Window");
            }

            Map map = new Map();
            map.GenerateNewRandomDistributionMap(16, 16);
            map.LoadMap();

            player = entityComponentManager.AddEntity();
            player.AddComponent<PlayerComponent>(new PlayerComponent(4, "../../../Sprites/player_idle.png", new Dictionary<string, Animation>()
            {
                { "idle", new Animation(0, 4, 150)}
            }, "player", Width, Height));
            entityComponentManager.AddEntityToGroup(player, new Group(2, GroupNames.GroupPlayers.ToString()));
            camera = player.GetComponent<CameraComponent>();

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
        public CameraComponent? GetCameraReference() => camera;
        public void StopGame() => IsRunning = false;
        public void Update()
        {
            entityComponentManager.Refresh();
            entityComponentManager.Update();

            HandleCollisions();

        }
        private void HandleCollisions()
        {
            foreach (ColliderComponent cc in colliders)
            {
                if (cc.Tag != player?.GetComponent<ColliderComponent>().Tag)
                {
                    if (Collision.AABBIsColliding(player?.GetComponent<ColliderComponent>() ?? throw new NullReferenceException("Player Does not Exist"), cc) && cc.Tag == "ground")
                    {
                        player.GetComponent<PlayerComponent>().IsGrounded = true;
                    }
                }
            }
        }

        public void AddTile(Tile tileObj)
        {
            Entity tile = entityComponentManager.AddEntity();
            tile.AddComponent<TileComponent>(new TileComponent((int)tileObj.TileSheetIndex * 32, 0, (int)tileObj.Position.X * 32, (int)tileObj.Position.Y * 32,tileObj.TileSheetName));
            if (tileObj.HasCollision)
            {
                tile.AddComponent<TransformComponent>(new TransformComponent(tileObj.Position.X*32, tileObj.Position.Y*32));
                tile.AddComponent<ColliderComponent>(new ColliderComponent("ground"));
            }
            entityComponentManager.AddEntityToGroup(tile, new Group(0, GroupNames.GroupMap.ToString()));
        }

        public void Render()
        {
            SDL.SDL_RenderClear(renderer);

            foreach (var group in entityComponentManager.GetGroups().OrderBy(x => x.Index))
            {
                group.Entities.ForEach(entity =>
                {
                    entity.Draw();
                });
            }

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
