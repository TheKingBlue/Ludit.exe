// Tiles are the building blocks of our world, they represent a single grid cell. 
// This file stores tile types (for example) wall, floor, door etc.), 
// Holds visual information and contains properties  (such as walkable). 

using System.Xml.Serialization;
using Microsoft.Xna.Framework;

namespace Ludit.Game.World
{
    
    /// <summary>
    /// Represents a single tile in the game world.
    /// Think of it as a square in a grid :)
    /// </summary>
    public class Tile
    {
        
        public TileType Type { get; set; } // Enum, (Wall, floor, door, etc.)

        public bool IsWalkable { get; set; } // Can a player walk on this tile?

        public bool BlocksVision { get; set; } // Use for LOS, this tile is not see through.

        public int TextureIndex { get; set;  } // Which sprite/ image are we using?

        public Color TintColor { get; set; } // Used for variation between sprites.
        // Color is from MonoGame, used to put a color filer over a sprite. 

        public int X { get; set; } // Horizontal position in grid
        public int Y { get; set; } // Vertical position in grid
        // these are for grid positioning, not pixel positioning. 

        public Tile() // Constructor: DEFAULT
        {
            Type = TileType.Empty;

            IsWalkable = false;

            BlocksVision = false;

            TextureIndex = 0;

            TintColor = Color.White;
        }

        public Tile(int x, int y, TileType type) // Constructor: With parameters
        {
            X = x;
            Y = y; // position properties

            Type = type; // save type

            TintColor = Color.White; // Standard = no filter on sprite

            SetPropertiesFromType(type); // Helper Method:
            // This method automatically sets IsWalkable and BlocksVision based on type. 
            // Example: type = wall with IsWalkable = false (you cant walk through walls)
        }
        // var tile = new Tile(5, 10, TileType.Wall)

        private void SetPropertiesFromType(TileType type) // Every standard tile type and its properties.
        // Use: TileType.Wall for example
        {
            switch (type)
            {
                case TileType.Empty:
                    IsWalkable = false; // Can't walk through it
                    BlocksVision = true; // Can't see through it
                    TextureIndex = 0;
                    break;

                case TileType.Floor:
                    IsWalkable = true;
                    BlocksVision = false;
                    TextureIndex = 1;
                    break;

                case TileType.Wall:
                    IsWalkable = false;
                    BlocksVision = true;
                    TextureIndex = 2;
                    break;

                case TileType.ClosedDoor:
                    IsWalkable = false;
                    BlocksVision = true;
                    TextureIndex = 3;
                    break;

                case TileType.OpenDoor:
                    IsWalkable = true;
                    BlocksVision = false;
                    TextureIndex = 4;
                    break;

                case TileType.Water:
                    IsWalkable = false;
                    BlocksVision = false;
                    TextureIndex = 5;
                    break;

                case TileType.Dirt:
                    IsWalkable = true;
                    BlocksVision = false;
                    TextureIndex = 6;
                    break;

                default:
                    IsWalkable = false;
                    BlocksVision = true;
                    TextureIndex = 0;
                    break;
            }
        }

        public void SetType(TileType newType) // Change Type during runtime
        {
            Type = newType; // Change Type property

            SetPropertiesFromType(newType); // This line is here so IsWalkable and BlocksVision get updated
        }

        public override string ToString() // Debug Helper
        {
            return $"Tile({X},{Y}): {Type} [Walkable{IsWalkable}, BlocksVision:{BlocksVision}]";
        }
    }


    public enum TileType // List of constants
    {
        Empty, // 0 (Enums get automatic numbering!)
        Floor, // 1
        Wall, // 2
        ClosedDoor, // 3
        OpenDoor, // 4
        Water, // 5
        Dirt, // 6
    }
}

