using SDL2;
namespace Nanook.App.Components.ComponentModels
{
    public class ColliderComponent : Component
    {
        public SDL.SDL_Rect Collider { get; set; }
        public string Tag { get; set; } = null!;
        public TransformComponent Transform { get; set; } = null!;

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
