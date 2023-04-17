using System.Text.Json;
using System.Text.Json.Serialization;

namespace Nanook.App.Models
{
    public class Map
    {
        public HashSet<Tile> Tiles { get; set; } = new HashSet<Tile>();

        public Map()
        { 
            
        }


        public void CreateNewMapFromTiles(HashSet<Tile> tiles)
        { 
            Tiles = tiles;
        }
        public void ReadMapFromTiles(string path)
        {
            Tiles = JsonSerializer.Deserialize<HashSet<Tile>>(File.ReadAllText(path)) ?? throw new NullReferenceException("Failed to read map from file"); 
        }
        public void GenerateNewRandomDistributionMap(uint xSize, uint ySize)
        {
            Random rand = new Random();
            for (int x = 0; x < xSize; x++)
            {
                for (int y = 0; y < ySize; y++)
                {
                    if(y > 5)
                    {
                        Tiles.Add(GetTileFromTileSheetIndex((uint)rand.Next(0, 2), x, y));
                    }
                }
            }
        }
        public void LoadMap()
        {
            foreach (var tile in Tiles)
            {
                Game.Instance.AddTile(tile);
            }
        }
        public Tile GetTileFromTileSheetIndex(uint index, int xpos, int ypos) => index switch
        {
            0 => new Tile(index, "../../../Sprites/basic_tilesheet.png", "Grass", new Math.Vector2(xpos, ypos), true),
            1 => new Tile(index, "../../../Sprites/basic_tilesheet.png", "Grass", new Math.Vector2(xpos, ypos), true),
            _ => throw new NullReferenceException("Invalid index")
        };

    }
}
