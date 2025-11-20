// Dungeon generation is a procedural algorithm.
// Instead of hand-designing every room, you write logic that creates infinite unique dungeons. 

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;

namespace Ludit.exe.Game.World
{
    public class RoomGenerator
    {
        private Tilemap tilemap;
        private Random random;
        private List<Room> rooms;

        // Configuration aka dungeon feel
        public int MaxRooms { get; set; } = 8; // Attempts to create rooms,
        // so attempts 8 times but actual rooms that come out of it could be 5 for example
        public int MinRoomSize { get; set; } = 4; // prevents 2x2 rooms
        public int MaxRoomSize { get; set; } = 10; // prevents huge rooms
        public int RoomBuffer { get; set; } = 1; // Spacing between rooms

        public RoomGenerator(Tilemap tilemap, int? seed = null) // the ? behind int makes in nullable
        {
            this.tilemap = tilemap;
            this.random = seed.HasValue ? new Random(seed.Value) : new Random();
            this.rooms = new List<Room>();
        }

        public void Generate()
        {
            rooms.Clear(); // reset state
            tilemap.Fill(TileType.Wall); // start with walls so there's no gaps in the dungeon
            PlaceRooms(); // Plan where rooms go
            CarveRooms(); // Excecute plan
            CreateCorridors(); // Connect every room
        }

        private void PlaceRooms()
        {
            for (int i = 0; i < MaxRooms; i++)
            {
                // Generate random dimensions
                int width = random.Next(MinRoomSize, MaxRoomSize + 1);
                int height = random.Next(MinRoomSize, MaxRoomSize + 1);

                // Generate random positions
                int x = random.Next(1, tilemap.Width - width - 1);
                int y = random.Next(1, tilemap.Height - height - 1);

                Room newRoom = new Room(x, y, width, height);

                // First room always succeeds
                if (rooms.Count == 0)
                {
                    rooms.Add(newRoom);
                    continue;
                }

                // Check against existing rooms
                bool canPlace = true;
                foreach (Room existingRoom in rooms)
                {
                    if (newRoom.IntersectsWithBuffer(existingRoom, RoomBuffer))
                    {
                        canPlace = false;
                        break; // no point in checking anymore
                    }
                }

                if (canPlace)
                {
                    rooms.Add(newRoom);
                }
            }
        }

        private void CarveRooms()
        {
            foreach (Room room in rooms)
            {
                tilemap.FillRectangle(room.X, room.Y, room.Width, room.Height, TileType.Floor);
            }
        }

        // Connecting Rooms
        private void CreateCorridors()
        {
            for (int i = 0; i < rooms.Count - 1; i++)
            {
                Room roomA = rooms[i];
                Room roomB = rooms[i + 1];

                CreateLShapedCorridor(roomA, roomB);
            }
        }

        private void CreateLShapedCorridor(Room roomA, Room roomB)
        {
            int centerA_X = roomA.CenterX();
            int centerA_Y = roomA.CenterY();
            int centerB_X = roomB.CenterX();
            int centerB_Y = roomB.CenterY();

            // Randomly choose horizontal of vertical first 
            if (random.Next(0, 2) == 0) // without randomizer every corridor would be the same
            {
                // Horizontal first
                CreateHorizontalTunnel(centerA_X, centerB_X, centerA_Y);
                CreateVerticalTunnel(centerA_Y, centerB_Y, centerB_X);
            }
            else
            {
                // Vertical first
                CreateVerticalTunnel(centerA_X, centerB_X, centerA_Y);
                CreateHorizontalTunnel(centerA_Y, centerB_Y, centerB_X);
            }
        }

        private void CreateHorizontalTunnel(int x1, int x2, int y)
        {
            int startX = Math.Min(x1, x2);
            int endX = Math.Max(x1, x2);

            for (int x = startX; x <= endX; x++)
            {
                tilemap.SetTile(x, y, TileType.Floor);
            }
        }

        private void CreateVerticalTunnel(int y1, int y2, int x)
        {
            int startY = Math.Min(y1, y2);
            int endY = Math.Max(y1, y2);

            for (int y = startY; y <= endY; y++)
            {
                tilemap.SetTile(x, y, TileType.Floor);
            }
        }
    }
}