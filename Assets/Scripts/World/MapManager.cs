using UnityEngine;
using System.Collections.Generic;
using System;

namespace HackSlash.World
{
    /// <summary>
    /// Manages game maps, enemy spawning, and map progression.
    /// Handles map data, difficulty scaling, and loot generation per map.
    /// </summary>
    public class MapManager : MonoBehaviour
    {
        public static MapManager Instance { get; private set; }

        [System.Serializable]
        public class Map
        {
            public string mapId;
            public string mapName;
            public string description;
            public int minLevel;
            public int maxLevel;
            public float difficulty;
            public List<EnemySpawn> enemySpawns = new();
            public List<Vector3> lootSpawnPoints = new();
            public Boss bossEnemy;
            public int recommendedPartySize;
            public float lootDropRate;
        }

        [System.Serializable]
        public class EnemySpawn
        {
            public int enemyId;
            public Vector3 spawnPosition;
            public int spawnCount;
            public float spawnInterval;
            public bool isElite;
        }

        [System.Serializable]
        public class Boss
        {
            public int bossId;
            public string bossName;
            public int hp;
            public int damage;
            public float armor;
            public List<string> specialAttacks = new();
            public List<int> lootDropIds = new();
        }

        private Dictionary<string, Map> maps = new();
        private string currentMapId = "map_1";
        private int currentWave = 1;
        private int enemiesSpawned = 0;
        private int enemiesDefeated = 0;

        void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeMaps();
        }

        /// <summary>
        /// Initialize all maps.
        /// </summary>
        private void InitializeMaps()
        {
            // Map 1: Starting Area
            maps["map_1"] = new Map
            {
                mapId = "map_1",
                mapName = "Village",
                description = "Your home village under attack",
                minLevel = 1,
                maxLevel = 5,
                difficulty = 1.0f,
                recommendedPartySize = 1,
                lootDropRate = 0.3f
            };

            // Map 2: Forest Ruins
            maps["map_2"] = new Map
            {
                mapId = "map_2",
                mapName = "Forest Ruins",
                description = "Ancient ruins hidden in the forest",
                minLevel = 5,
                maxLevel = 15,
                difficulty = 1.5f,
                recommendedPartySize = 1,
                lootDropRate = 0.4f
            };

            // Map 3: Dark Caverns
            maps["map_3"] = new Map
            {
                mapId = "map_3",
                mapName = "Dark Caverns",
                description = "Deep caverns filled with darkness",
                minLevel = 15,
                maxLevel = 25,
                difficulty = 2.0f,
                recommendedPartySize = 1,
                lootDropRate = 0.5f
            };

            // Map 4: Volcanic Wasteland
            maps["map_4"] = new Map
            {
                mapId = "map_4",
                mapName = "Volcanic Wasteland",
                description = "A barren wasteland of lava and ash",
                minLevel = 25,
                maxLevel = 35,
                difficulty = 2.5f,
                recommendedPartySize = 1,
                lootDropRate = 0.6f
            };

            // Map 5: Frozen Tundra
            maps["map_5"] = new Map
            {
                mapId = "map_5",
                mapName = "Frozen Tundra",
                description = "An icy frozen wasteland",
                minLevel = 35,
                maxLevel = 45,
                difficulty = 3.0f,
                recommendedPartySize = 1,
                lootDropRate = 0.7f
            };

            // Map 6: Sky Temple
            maps["map_6"] = new Map
            {
                mapId = "map_6",
                mapName = "Sky Temple",
                description = "A mystical temple floating in the sky",
                minLevel = 45,
                maxLevel = 60,
                difficulty = 3.5f,
                recommendedPartySize = 1,
                lootDropRate = 0.8f
            };

            Debug.Log("Maps initialized: 6 maps created");
        }

        /// <summary>
        /// Load a specific map.
        /// </summary>
        public void LoadMap(string mapId)
        {
            if (!maps.ContainsKey(mapId))
            {
                Debug.LogError($"Map {mapId} not found");
                return;
            }

            Map map = maps[mapId];
            
            // Check player level
            if (PlayerStats.Instance.Level < map.minLevel)
            {
                Debug.LogWarning($"Player level {PlayerStats.Instance.Level} is below minimum {map.minLevel}");
                return;
            }

            currentMapId = mapId;
            currentWave = 1;
            enemiesSpawned = 0;
            enemiesDefeated = 0;

            Debug.Log($"Loading map: {map.mapName} (Difficulty: {map.difficulty})");
            OnMapLoaded?.Invoke(map);
        }

        /// <summary>
        /// Get current map.
        /// </summary>
        public Map GetCurrentMap()
        {
            if (maps.ContainsKey(currentMapId))
                return maps[currentMapId];
            return null;
        }

        /// <summary>
        /// Get map by ID.
        /// </summary>
        public Map GetMap(string mapId)
        {
            if (maps.ContainsKey(mapId))
                return maps[mapId];
            return null;
        }

        /// <summary>
        /// Get all maps.
        /// </summary>
        public List<Map> GetAllMaps()
        {
            return new List<Map>(maps.Values);
        }

        /// <summary>
        /// Generate enemies for current wave.
        /// </summary>
        public void GenerateWave()
        {
            Map map = GetCurrentMap();
            if (map == null)
                return;

            // Scale difficulty based on wave
            float waveDifficulty = map.difficulty * (1 + (currentWave * 0.1f));
            
            Debug.Log($"Generating wave {currentWave} with difficulty {waveDifficulty}");
            OnWaveStarted?.Invoke(currentWave, waveDifficulty);
        }

        /// <summary>
        /// Complete current wave.
        /// </summary>
        public void CompleteWave()
        {
            Map map = GetCurrentMap();
            if (map == null)
                return;

            // Award rewards
            int waveXP = 100 * currentWave;
            int waveGold = 50 * currentWave;

            PlayerStats.Instance.AddExperience(waveXP);
            PlayerStats.Instance.AddGold(waveGold);

            currentWave++;
            Debug.Log($"Wave {currentWave - 1} completed! Next wave starting...");
            OnWaveCompleted?.Invoke(currentWave - 1);
        }

        /// <summary>
        /// Track enemy spawn.
        /// </summary>
        public void OnEnemySpawned()
        {
            enemiesSpawned++;
        }

        /// <summary>
        /// Track enemy defeat.
        /// </summary>
        public void OnEnemyDefeated()
        {
            enemiesDefeated++;
            
            // Check if wave is complete
            if (enemiesDefeated >= enemiesSpawned && enemiesSpawned > 0)
            {
                CompleteWave();
            }
        }

        /// <summary>
        /// Get map progression (0-100%).
        /// </summary>
        public float GetMapProgress()
        {
            if (enemiesSpawned == 0)
                return 0;
            return (enemiesDefeated / (float)enemiesSpawned) * 100f;
        }

        /// <summary>
        /// Get current wave number.
        /// </summary>
        public int GetCurrentWave()
        {
            return currentWave;
        }

        /// <summary>
        /// Get difficulty multiplier.
        /// </summary>
        public float GetDifficultyMultiplier()
        {
            Map map = GetCurrentMap();
            if (map == null)
                return 1.0f;
            
            return map.difficulty * (1 + (currentWave * 0.1f));
        }

        /// <summary>
        /// Get loot drop rate for current map.
        /// </summary>
        public float GetLootDropRate()
        {
            Map map = GetCurrentMap();
            if (map == null)
                return 0.3f;
            
            return map.lootDropRate;
        }

        /// <summary>
        /// Get available maps for player level.
        /// </summary>
        public List<Map> GetAvailableMaps()
        {
            List<Map> available = new();
            int playerLevel = PlayerStats.Instance.Level;

            foreach (var map in maps.Values)
            {
                if (playerLevel >= map.minLevel)
                    available.Add(map);
            }

            return available;
        }

        // Events
        public event Action<Map> OnMapLoaded;
        public event Action<int, float> OnWaveStarted;
        public event Action<int> OnWaveCompleted;
    }
}
