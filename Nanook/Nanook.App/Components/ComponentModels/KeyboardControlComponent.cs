using SDL2;
using System.Diagnostics;

namespace Nanook.App.Components.ComponentModels
{
    public class KeyboardControlComponent : Component
    {
        public TransformComponent? Transform { get; set; }
        public override void Init()
        {
            if (!Entity.HasComponent<TransformComponent>())
            {
                Entity.AddComponent<TransformComponent>(new TransformComponent());
            }
            Transform = Entity.GetComponent<TransformComponent>();
        }

        public override void Update() 
        {
            SDL.SDL_Event @event = Game.Instance.GetEventReference();
           
            if (@event.type == SDL.SDL_EventType.SDL_KEYDOWN)
            {
                switch (@event.key.keysym.sym) 
                {
                    case SDL.SDL_Keycode.SDLK_w:
                        Transform.Velocity.Y = -1;
                        break;
                    case SDL.SDL_Keycode.SDLK_a:
                        Transform.Velocity.X = -1;
                        break;
                    case SDL.SDL_Keycode.SDLK_s:
                        Transform.Velocity.Y = 1;
                        break;
                    case SDL.SDL_Keycode.SDLK_d:
                        Transform.Velocity.X = 1;
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
                        Transform.Velocity.Y = 0;
                        break;
                    case SDL.SDL_Keycode.SDLK_a:
                        Transform.Velocity.X = 0;
                        break;
                    case SDL.SDL_Keycode.SDLK_s:
                        Transform.Velocity.Y = 0;
                        break;
                    case SDL.SDL_Keycode.SDLK_d:
                        Transform.Velocity.X = 0;
                        break;
                    default:
                        break;
                }
            }

        }
        

    }
}
