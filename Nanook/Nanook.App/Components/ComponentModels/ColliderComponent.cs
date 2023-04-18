using Nanook.App.Enums;
using SDL2;
using System.Numerics;

namespace Nanook.App.Components.ComponentModels
{
    public class ColliderComponent : Component
    {
        public SDL.SDL_Rect Collider { get; set; }
        public string Tag { get; set; } = null!;
        public TransformComponent Transform { get; set; } = null!;
        private Entity? debugColliderReference = null;
        public ColliderComponent(string tag)
        {
            Tag = tag;
        }
        public override void Init()
        {
            if (!Entity.HasComponent<TransformComponent>())
            {
                Entity.AddComponent<TransformComponent>(new TransformComponent());
            }
            Transform = Entity.GetComponent<TransformComponent>();

            if (Game.Instance.ShowColliders)
            {
                debugColliderReference = Game.Instance.GetEntityComponenteManagerReference().AddEntity();
                debugColliderReference.AddComponent<TransformComponent>(Entity.GetComponent<TransformComponent>());
                debugColliderReference.AddComponent<SpriteComponent>(new SpriteComponent("../../../Sprites/debug_collider.png"));
                Game.Instance.GetEntityComponenteManagerReference().AddEntityToGroup(debugColliderReference, new Group(1, GroupNames.GroupColliders.ToString()));
            }

            Game.Instance.GetColliderComponentsReference().Add(this);
        }
        public override void Update() 
        {
            Collider = new SDL.SDL_Rect()
            {
                x = (int)Transform.Position.X,
                y = (int)Transform.Position.Y,
                w = Transform.Width * Transform.Scale,
                h = Transform.Height * Transform.Scale,
            };
        }
    }
}
