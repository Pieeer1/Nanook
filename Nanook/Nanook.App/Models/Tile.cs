using Nanook.App.Models.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nanook.App.Models
{
    public class Tile
    {

        public Tile(uint tileSheetIndex, string tileSheetName, string friendlyName, Vector2 position, bool hasCollision)
        {
            TileSheetIndex = tileSheetIndex;
            TileSheetName = tileSheetName;
            FriendlyName = friendlyName;
            Position = position;
            HasCollision = hasCollision;
        }
        public uint TileSheetIndex { get; set; }
        public string TileSheetName { get; set; }
        public string FriendlyName { get; set; }
        public Vector2 Position { get; set; }
        public bool HasCollision { get; set; }
    }
}
