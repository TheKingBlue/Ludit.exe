// Tilemap is the container that manages all tiles in a 2D grit
// It is a 'canvas' to draw the level on

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Ludit.exe.Game.World
{
    // manages a 2D grid of tiles
    public class Tilemap
    {
        public int Width { get; private set; } // Grid width
        public int Height { get; private set; } // Grid height

        // 2D array that saves all tiles
        private Tile[,] tiles;

        // Constructor: Tilemap with specified width and height
        public Tilemap(int width, int height)
        {
            Width = width;
            Height = height;
            tiles = new Tile[width, height];
            initializeTiles(); // Prevents null references
        }

        // This function fills the tilemap with default empty tiles
        private void initializeTiles()
        {
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    tiles[x, y] = new Tile(x, y, TileType.Empty);
                }
            }
        }

        // Check for valid coordinates
        private bool IsInBounds(int x, int y)
        {
            return x >= 0 && x < Width && y >= 0 && y < Height;
        }

        // Get tile at specific grid position
        public Tile GetTile(int x, int y)
        {
            if (IsInBounds(x, y))
            {
                return tiles[x, y];
            }
            return null; // Out of bounds
        }

        // Change tile type at specified position
        public void SetTile(int x, int y, TileType type)
        {
            if (IsInBounds(x, y))
            {
                tiles[x, y].SetType(type); // Update tile type and properties
            }
        }

        // Fill the entire tilemap with a specific tile type
        public void Fill(TileType type)
        {
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    tiles[x, y].SetType(type);
                }
            }
        }

        // render the tilemap
        public void Draw(SpriteBatch spriteBatch, Dictionary<TileType, Texture2D> tileTextures, int tileSize)
        {
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    Tile tile = tiles[x, y];
                    // convert grid position to pixel position
                    Rectangle destinationRect = new Rectangle(x * tileSize, y * tileSize, tileSize, tileSize);
                    // select sprite from texture based on TextureIndex
                    if (tileTextures.ContainsKey(tile.Type))
                    {
                        Texture2D currentTexture = tileTextures[tile.Type];
                        spriteBatch.Draw(currentTexture, destinationRect, Color.White);
                    }
                }
            }
        }

        // fill rectangle area with specific tile type, for room generation if we need it.
        public void FillRectangle(int startX, int startY, int width, int height, TileType type)
        {
            for (int x = startX; x < startX + width; x++)
            {
                for (int y = startY; y < startY + height; y++)
                {
                    tiles[x, y].SetType(type);
                }
            }
        }
    } 
}