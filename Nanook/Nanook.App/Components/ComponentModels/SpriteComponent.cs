using SDL2;

namespace Nanook.App.Components.ComponentModels
{
    public class SpriteComponent : Component
    {
        public string Path { get; set; }
        private TransformComponent transform { get; set; }
        private IntPtr texture { get; set; }
        private SDL.SDL_Rect srcRect { get; set; }
        private SDL.SDL_Rect destRect { get; set; }
        public SpriteComponent(string path) 
        {
            Path = path;
            if (!Entity.HasComponent<TransformComponent>())
            {
                Entity.AddComponent<TransformComponent>(new TransformComponent());
            }
            transform = Entity.GetComponent<TransformComponent>();
        }

        public override void Init()
        {

        }

        public override void Update()
        {

        }

        public override void Draw()
        {

        }
    }
}
