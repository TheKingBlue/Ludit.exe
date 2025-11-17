// Tilemap is the container that manages all tiles in a 2D grit
// It is a 'canvas' to draw the level on

using System;

namespace Ludit.exe.Game.World
{
    // manages a 2D grid of tiles
    public class Tilemap
    {
        public int Width { get; private set; } // Grid width
        public int Height { get; private set; } // Grid height

        // 2D array that saves all tiles
        private Tile[,] tiles;
    }

    
}