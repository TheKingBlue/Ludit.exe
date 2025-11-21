using System.Collections.Generic;
using Ludit.exe.Game.World;

namespace Ludit.exe.Services
{
    public class RoomGenerator
    {
        // Configuration - how rooms should look
        public int MinRoomSize { get; set; } = 4;
        public int MaxRoomSize { get; set; } = 10;
        public int RoomBuffer { get; set; } = 1; // Minimum space between rooms

        public Room CreateRandomRoom(int mapWidth, int mapHeight)
        {
            // Random dimensions
            int width = RNGService.Next(MinRoomSize, MaxRoomSize + 1);
            int height = RNGService.Next(MinRoomSize, MaxRoomSize + 1);

            // Random position (with bounds checking)
            int x = RNGService.Next(1, mapWidth - width - 1);
            int y = RNGService.Next(1, mapHeight - height - 1);

            return new Room(x, y, width, height);
        }

        public bool CanPlaceRoom(Room newRoom, List<Room> existingRooms)
        {
            // First room always succeeds
            if (existingRooms.Count == 0)
                return true;

            // Check against all existing rooms
            foreach (Room existingRoom in existingRooms)
            {
                if (newRoom.IntersectsWithBuffer(existingRoom, RoomBuffer))
                {
                    return false; // Overlap detected
                }
            }

            return true; // No overlaps
        }

        public Room TryCreateValidRoom(int mapWidth, int mapHeight, List<Room> existingRooms, int maxAttempts = 10)
        {
            for (int attempt = 0; attempt < maxAttempts; attempt++)
            {
                Room candidateRoom = CreateRandomRoom(mapWidth, mapHeight);

                if (CanPlaceRoom(candidateRoom, existingRooms))
                {
                    return candidateRoom;
                }
            }

            return null; // Failed to create valid room
        }
    }
}