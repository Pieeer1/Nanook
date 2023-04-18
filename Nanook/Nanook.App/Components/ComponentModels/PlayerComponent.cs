using Nanook.App.Models;
using SDL2;

namespace Nanook.App.Components.ComponentModels
{
    public class PlayerComponent : Component
    {
        public PlayerComponent(int scale, string spriteSheetPath, Dictionary<string, Animation> animations, string colliderTag, int cameraWidth, int cameraHeight)
        {
            _scale = scale;
            _spriteSheetPath = spriteSheetPath;
            _animations = animations;
            _colliderTag = colliderTag;
            _cameraWidth = cameraWidth;
            _cameraHeight = cameraHeight;

        }
        public override void Init()
        {
            Entity.AddComponent<TransformComponent>(new TransformComponent(_scale));
            Entity.AddComponent<ColliderComponent>(new ColliderComponent(_colliderTag));
            Entity.AddComponent<CameraComponent>(new CameraComponent(_cameraWidth, _cameraHeight));
            Entity.AddComponent<SpriteComponent>(new SpriteComponent(_spriteSheetPath, _animations));
            Entity.AddComponent<KeyboardControlComponent>(new KeyboardControlComponent((@event, Transform, Sprite) =>
            {
                if (@event.type == SDL.SDL_EventType.SDL_KEYDOWN)
                {
                    switch (@event.key.keysym.sym)
                    {
                        case SDL.SDL_Keycode.SDLK_w:
                            Transform.Velocity.Y = -1;
                            break;
                        case SDL.SDL_Keycode.SDLK_a:
                            Transform.Velocity.X = -1;
                            Sprite.FlipFlag = SDL.SDL_RendererFlip.SDL_FLIP_HORIZONTAL;
                            break;
                        case SDL.SDL_Keycode.SDLK_s:
                            Transform.Velocity.Y = 1;
                            break;
                        case SDL.SDL_Keycode.SDLK_d:
                            Transform.Velocity.X = 1;
                            Sprite.FlipFlag = SDL.SDL_RendererFlip.SDL_FLIP_NONE;
                            break;
                        default:
                            break;
                    }
                }
                if (@event.type == SDL.SDL_EventType.SDL_KEYUP)
                {
                    switch (@event.key.keysym.sym)
                    {
                        case SDL.SDL_Keycode.SDLK_w:
                            if (Transform.Velocity.Y < 0)
                            {
                                Transform.Velocity.Y = 0;
                            }
                            break;
                        case SDL.SDL_Keycode.SDLK_a:
                            if (Transform.Velocity.X < 0)
                            {
                                Transform.Velocity.X = 0;
                            }
                            break;
                        case SDL.SDL_Keycode.SDLK_s:
                            if (Transform.Velocity.Y > 0)
                            {
                                Transform.Velocity.Y = 0;
                            }
                            break;
                        case SDL.SDL_Keycode.SDLK_d:
                            if (Transform.Velocity.X > 0)
                            {
                                Transform.Velocity.X = 0;
                            }
                            break;
                        case SDL.SDL_Keycode.SDLK_ESCAPE:
                            Game.Instance.StopGame();
                            break;
                        default:
                            break;
                    }
                }
            }));
            Transform = Entity.GetComponent<TransformComponent>();
            Sprite = Entity.GetComponent<SpriteComponent>();
            KeyboardControl = Entity.GetComponent<KeyboardControlComponent>();
            Collider = Entity.GetComponent<ColliderComponent>();
            Camera = Entity.GetComponent<CameraComponent>();
        }
        public override void Update()
        {
            if (!IsGrounded)
            {
                Entity.GetComponent<TransformComponent>().Position += new Models.Math.Vector2(0, 3f);
            }
        }

        public TransformComponent Transform { get; set; } = null!;
        public SpriteComponent Sprite { get; set; } = null!;
        public KeyboardControlComponent KeyboardControl { get; set; } = null!;
        public ColliderComponent Collider { get; set; } = null!;
        public CameraComponent Camera { get; set; } = null!;

        private int _scale { get; set; }
        private string _spriteSheetPath { get; set; }
        private Dictionary<string, Animation> _animations { get; set; }
        private string _colliderTag { get; set; }
        private int _cameraWidth { get; set; }
        private int _cameraHeight { get; set; }

        public bool IsGrounded { get; set; } = false;
    }
}
