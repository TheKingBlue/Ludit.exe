// DungeonGenerator orchestrates the entire dungeon creation process
// It uses RoomGenerator as a tool, but controls the overall layout

using System;
using System.Collections.Generic;
using Ludit.exe.Services;

namespace Ludit.exe.Game.World
{
    public class DungeonGenerator
    {
        private Tilemap tilemap;
        private RoomGenerator roomGenerator;
        private List<Room> rooms;
        public int MaxRooms { get; set; } = 8;

        public DungeonGenerator(Tilemap tilemap)
        {
            this.tilemap = tilemap;
            this.roomGenerator = new RoomGenerator();
            this.rooms = new List<Room>();
        }

        public void Generate()
        {
            rooms.Clear();
            tilemap.Fill(TileType.Wall); // Start with solid walls

            PlaceRooms();
            CarveRooms();
            CreateCorridors();
        }

        private void PlaceRooms()
        {
            for (int i = 0; i < MaxRooms; i++)
            {
                Room newRoom = roomGenerator.CreateRandomRoom(tilemap.Width, tilemap.Height);

                if (roomGenerator.CanPlaceRoom(newRoom, rooms))
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

            // Randomly choose direction
            if (RNGService.NextBool())
            {
                // Horizontal first, then vertical
                CreateHorizontalTunnel(centerA_X, centerB_X, centerA_Y);
                CreateVerticalTunnel(centerA_Y, centerB_Y, centerB_X);
            }
            else
            {
                // Vertical first, then horizontal
                CreateVerticalTunnel(centerA_Y, centerB_Y, centerA_X);
                CreateHorizontalTunnel(centerA_X, centerB_X, centerB_Y);
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

        public List<Room> GetRooms()
        {
            return new List<Room>(rooms); // Return copy for safety
        }
    }
}