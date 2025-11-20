// A room is like a blueprint that describes a rectangular space in our dungeon

using System.Runtime.InteropServices;

namespace Ludit.exe.Game.World
{
    public class Room
    {
        public int X { get; set; } // Think of our tilemap as graph paper, 
        // X,Y is the top-left corner of the room. 
        public int Y { get; set; } 
        public int Width { get; set; } // How wide is the room? 
        public int Height { get; set; } // How tall is the room? 

        public Room(int x, int y, int width, int height) // Initialize new room
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        // Calculate center points
        public int CenterX()
        {
            return X + Width / 2;
        }

        public int CenterY()
        {
            return Y + Height / 2;
        }

        // Checks for overlap between rooms
        // If there is overlap: reject and try again. 
        public bool Intersects(Room other)
        {
            return (X < other.X + other.Width) && // My left edge is left of the other's right edge
            (X + Width > other.X) && // My right edge is right of the other's left edge
            (Y < other.Y + other.Height) && // My top edge is above the other's bottom edge
            (Y + Height > other.Y); // My bottom edge is below the other's top edge
        }

        // We pretend this room is slightly larger so rooms have breathing space
        public bool IntersectsWithBuffer(Room other, int buffer)
        {
            return (X - buffer < other.X + other.Width) &&
            (X + Width + buffer > other.X) &&
            (Y - buffer < other.Y + other.Height) &&
            (Y + Height + buffer > other.Y);
        }

        // for Debugging
        public override string ToString()
        {
            return $"Room(X:{X}, Y:{Y}, W:{Width}, H:{Height})";
        }
    }
}