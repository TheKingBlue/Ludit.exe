// Think of a roomtheme as a rulebook for how rooms should look and behave. 
// Think of it as a style guide or biome for rooms in our dungeons

using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Ludit.exe.Game.World
{
    public class RoomTheme
    {
        public string Name { get; set; }
        public string Description { get; set; }

        // Visual properties
        public TileType FloorType { get; set; }
        public TileType WallType { get; set; }

        public Color PrimaryColor { get; set; }
        public Color SecondaryColor { get; set; }

        


    }
}





