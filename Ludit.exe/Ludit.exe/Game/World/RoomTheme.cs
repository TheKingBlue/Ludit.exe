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

        public int GetRandomFloorTexture()
        {
            if (FloorTextureVariants.Count == 0) return 1; // if list empty return default texture

            int index = Services.RNGService.Next(0, FloorTextureVariants.Count);
            return FloorTextureVariants[index];
        }

        public int GetRandomWallTexture()
        {
            if (WallTextureVariants.Count == 0) return 1; // if list empty return default texture

            int index = Services.RNGService.Next(0, WallTextureVariants.Count);
            return WallTextureVariants[index];
        }

        public string GetRandomEnemyType()
        {
            if (EnemyTypes.Count == 0)
                return null;

            int index = Services.RNGService.Next(0, EnemyTypes.Count);
            return EnemyTypes[index];
        }

        public string GetRandomLootType()
        {
            if (LootTypes.Count == 0)
                return null;

            int index = Services.RNGService.Next(0, LootTypes.Count);
            return LootTypes[index];
        }

        public bool ShouldSpawnEnemies()
        {
            return Services.RNGService.NextDouble() < EnemySpawnChance; // Rolls the dice to see if enemies should spawn
        }

        public bool ShouldSpawnLoot()
        {
            return Services.RNGService.NextDouble() < LootSpawnChance; // Rolls the dice to see if loot should spawn
        }

        public bool ShouldSpawnHazard()
        {
            return Services.RNGService.NextDouble() < HazardSpawnChance; // Rolls the dice to see if hazard should spawn
        }

        public int GetEnemyCount()
        {
            return Services.RNGService.Next(MinEnemies, MaxEnemies + 1); // Gets a random number of enemies to spawn, between min- and maxenemies.
        }

        public int GetLootCount()
        {
            return Services.RNGService.Next(MinLootItems, MaxLootItems + 1); // Gets a random number of loot items to spawn, between min- and maxlootitems.
        }

        public override string ToString()
        {
            return $"Theme: {Name} ({Description})"; // Debug homie
        }
    }

    // Theme presets
    // This is a "factory" class that creates pre-made themes for us
    // Instead of manually setting every property, we can just call a theme.
    public static class RoomThemePresets
    {
        // EXAMPLE: CREATES A DARK STONE DUNGEON THEME
        public static RoomTheme CreateDungeonTheme()
        {
            return new RoomTheme
            {
                Name = "Dungeon",
                Description = "A dark stone dungeon filled with dangers",

                // Visual properties
                FloorType = TileType.Floor,
                WallType = TileType.Wall,
                PrimaryColor = new Color(80, 80, 90),      // Dark gray-blue
                SecondaryColor = new Color(60, 60, 70),    // Even darker
                AmbientLight = new Color(200, 200, 220),   // Cool, dim lighting
                LightIntensity = 0.7f,                     // Somewhat dark

                FloorTextureVariants = new List<int> { 1 },
                WallTextureVariants = new List<int> { 2 },

                // Enemy properties
                EnemyTypes = new List<string> {"Skeleton", "Rat", "Spider"},
                MinEnemies = 2,
                MaxEnemies = 5,
                EnemySpawnChance = 0.8f, // 80% chance to spawn enemies

                // Loot properties
                LootTypes = new List<string> {"Gold", "HealthPotion", "Key"},
                MinLootItems = 1,
                MaxLootItems = 3,
                LootSpawnChance = 0.4f, // 40% chance to spawn loot

                // Hazards
                HazardTypes = new List<string> {"Spike", "PoisonTrap"},
                HazardSpawnChance = 0.2f,

                DifficultyMultiplier = 1.0f, // Normal difficulty

                // Decorations
                DecorationTypes = new List<string> { "Torch", "Chain", "BrokenArmor" },
                DecorationDensity = 0.3f,

                // Audio
                AmbientSoundId = "dungeon_ambient",
                MusicTrackId = "dungeon_theme"
            };
        }
    }
}





