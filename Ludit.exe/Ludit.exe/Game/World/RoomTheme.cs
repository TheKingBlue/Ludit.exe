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

        // Texture variations
        public List<int> FloorTextureVariants { get; set; }
        public List<int> WallTextureVariants { get; set; }

        // Lighting and atmosphere
        public Color AmbientLight { get; set; }
        public Color LightIntensity { get; set; }

        // Gamplay properties: ENEMIES AKA OPPS
        public List<string> EnemyTypes { get; set; } // which enemies can spawn where
        public int MinEnemies { get; set; }
        public int MaxEnemies { get; set; }
        public float EnemySpawnChance { get; set; }

        // Gameplay properties: LOOT AKA THAT BLINGBLING
        public List<string> LootTypes { get; set; }
        public int MinLootItems { get; set; }
        public int MaxLootItems { get; set; }
        public float LootSpawnChance { get; set; }

        // Gameplay properties: HAZARDS NOT HAZARD FROM OW
        public List<string> HazardTypes { get; set; } // spikes, poison, etc. 
        public float HazardSpawnChance { get; set; }

        // Decorations, these are visual elements that add flavor (pillars, statues, etc.)
        public List<string> DecorationTypes { get; set; }
        public float DecorationDensity { get; set; }

        // Audio
        public string AmbientSoundId { get; set; } // background sounds such as water dripping
        public string MusicTrackId { get; set; }

        public float DifficultyMultiplier { get; set; }

        // CONSTRUCTOR
        public RoomTheme()
        {
            Name = "Default";
            Description = "A basic room with no special properties";

            FloorType = TileType.Floor;
            WallType = TileType.Wall;
            PrimaryColor = Color.White; // No tint
            SecondaryColor = Color.Gray;
            AmbientLight = PrimaryColor.White; // normal lighting
            LightIntensity = 1.0f; // full brightness

            FloorTextureVariants = new List<int> { 1 };
            WallTextureVariants = new List<int> { 2 };
            EnemyTypes = new List<string>();
            LootTypes = new List<string>();
            HazardTypes = new List<string>();
            DecorationTypes = new List<string>();

            // Default gameplay values, can change
            MinEnemies = 0;
            MaxEnemies = 3;
            EnemySpawnChance = 0.5f; // 50% chance
            LootSpawnChance = 0.3f; // 30% chance
            MinLootItems = 0;
            MaxLootItems = 2;
            HazardSpawnChance = 0.1f; // 10% chance
            DifficultyMultiplier = 1.0f; // Normal difficulty
            DecorationDensity = 0.2f; // 20% decoration coverage

            // for now no audio by default
            AmbientSoundId = null;
            MusicTrackId = null;
        }


    }


}





