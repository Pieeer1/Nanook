namespace Nanook.App
{
    public class GameBuilderSettings
    {
        public string Title { get; set; } = "Default Title";
        public int XPosition { get; set; } = 0;
        public int YPosition { get; set; } = 0;
        public int Width { get; set; } = 800;
        public int Height { get; set; } = 640;
        public bool IsFullScreen { get; set; } = false;
    }
}
