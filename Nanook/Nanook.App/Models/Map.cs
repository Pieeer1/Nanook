namespace Nanook.App.Models
{
    public class Map
    {

        public static void LoadMap(string path, int sizeX, int sizeY)
        { 
            string[] map = File.ReadAllLines(path);

            for (int y = 0; y < map.Length; y++)
            {
                string[] values = map[y].Split(',');
                for (int x = 0; x < values.Length; x++)
                {
                    Game.Instance.AddTile(int.Parse(values[x]), x * 32, y * 32);
                }
            }
            
        }

    }
}
