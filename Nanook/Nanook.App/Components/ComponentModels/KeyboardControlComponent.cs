using SDL2;
using System.Diagnostics;

namespace Nanook.App.Components.ComponentModels
{
    public class KeyboardControlComponent : Component
    {
        public TransformComponent Transform { get; set; } = null!;
        public SpriteComponent Sprite { get; set; } = null!;
        public Action<SDL.SDL_Event, TransformComponent, SpriteComponent> KeyCodeActions { get; set; }

        public KeyboardControlComponent(Action<SDL.SDL_Event, TransformComponent, SpriteComponent> keyCodeActions)
        {
            KeyCodeActions = keyCodeActions;
        }

        public override void Init()
        {
            if (!Entity.HasComponent<TransformComponent>())
            {
                Entity.AddComponent<TransformComponent>(new TransformComponent());
            }
            Transform = Entity.GetComponent<TransformComponent>();
            Sprite = Entity.GetComponent<SpriteComponent>();
        }

        public override void Update() 
        {
            SDL.SDL_Event @event = Game.Instance.GetEventReference();
            KeyCodeActions(@event, Transform, Sprite);
        }
        

    }
}
