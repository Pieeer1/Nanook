using SDL2;
namespace Nanook.App.Components.ComponentModels
{
    public class TileComponent : Component
    {
        public TransformComponent Transform { get; set; } = null!;
        public SpriteComponent Sprite { get; set; } = null!;

        public SDL.SDL_Rect TileRect { get; set; }
        public int TileId { get; set; }
        public string Path { get; set; }

        public TileComponent(int x, int y, int w, int h, int id)
        {
            TileRect = new SDL.SDL_Rect()
            {
                x = x,
                y = y,
                w = w,
                h = h
            };
            TileId = id;

            Path = PathFromId(id);

        }
        public override void Init()
        {
            Transform = Entity.AddComponent<TransformComponent>(new TransformComponent(TileRect.x, TileRect.y, TileRect.h, TileRect.w, 1));
            Sprite = Entity.AddComponent<SpriteComponent>(new SpriteComponent(Path));
        }



        private string PathFromId(int id) => id switch
        {
            0 => "../../../Sprites/Water.png",
            1 => "../../../Sprites/Dirt.png",
            2 => "../../../Sprites/Grass.png",
            _ => throw new InvalidOperationException($"Cannot find Sprite with ID {id}")
        };
    }
}
