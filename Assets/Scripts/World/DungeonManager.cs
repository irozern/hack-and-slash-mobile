using UnityEngine;
using System.Collections.Generic;
using System;

namespace HackSlash.World
{
    /// <summary>
    /// Manages dungeons with procedurally generated floors and bosses.
    /// Handles dungeon progression, rewards, and difficulty scaling.
    /// </summary>
    public class DungeonManager : MonoBehaviour
    {
        public static DungeonManager Instance { get; private set; }

        [System.Serializable]
        public class Dungeon
        {
            public int dungeonId;
            public string dungeonName;
            public string description;
            public int minLevel;
            public int maxLevel;
            public int totalFloors;
            public float difficulty;
            public List<DungeonFloor> floors = new();
            public Boss bossFinal;
            public DungeonReward reward;
            public bool completed;
        }

        [System.Serializable]
        public class DungeonFloor
        {
            public int floorNumber;
            public string floorName;
            public int enemyCount;
            public List<int> enemyIds = new();
            public float difficultyMultiplier;
            public bool isBossFloor;
            public Boss boss;
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

        [System.Serializable]
        public class DungeonReward
        {
            public int xpReward;
            public int goldReward;
            public List<int> itemRewards = new();
            public int premiumCurrencyReward;
        }

        private Dictionary<int, Dungeon> dungeons = new();
        private int currentDungeonId = -1;
        private int currentFloor = 1;
        private bool dungeonInProgress = false;

        void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeDungeons();
        }

        /// <summary>
        /// Initialize all dungeons.
        /// </summary>
        private void InitializeDungeons()
        {
            // DUNGEON 1: Forest Crypt
            dungeons[1] = new Dungeon
            {
                dungeonId = 1,
                dungeonName = "Forest Crypt",
                description = "An ancient crypt hidden in the forest",
                minLevel = 10,
                maxLevel = 20,
                totalFloors = 5,
                difficulty = 1.5f,
                reward = new DungeonReward { xpReward = 1000, goldReward = 500, premiumCurrencyReward = 50 }
            };

            // Add floors to Forest Crypt
            for (int i = 1; i <= 5; i++)
            {
                DungeonFloor floor = new DungeonFloor
                {
                    floorNumber = i,
                    floorName = $"Floor {i}",
                    enemyCount = 5 + (i * 2),
                    difficultyMultiplier = 1.0f + (i * 0.2f),
                    isBossFloor = i == 5
                };

                if (i == 5)
                {
                    floor.boss = new Boss
                    {
                        bossId = 101,
                        bossName = "Forest Guardian",
                        hp = 200,
                        damage = 20,
                        armor = 0.3f,
                        specialAttacks = new List<string> { "Root Attack", "Summon Minions" },
                        lootDropIds = new List<int> { 1, 2, 3 }
                    };
                }

                dungeons[1].floors.Add(floor);
            }

            // DUNGEON 2: Cavern of Shadows
            dungeons[2] = new Dungeon
            {
                dungeonId = 2,
                dungeonName = "Cavern of Shadows",
                description = "A dark cavern filled with shadows",
                minLevel = 20,
                maxLevel = 30,
                totalFloors = 7,
                difficulty = 2.0f,
                reward = new DungeonReward { xpReward = 2000, goldReward = 1000, premiumCurrencyReward = 100 }
            };

            // Add floors to Cavern of Shadows
            for (int i = 1; i <= 7; i++)
            {
                DungeonFloor floor = new DungeonFloor
                {
                    floorNumber = i,
                    floorName = $"Floor {i}",
                    enemyCount = 8 + (i * 2),
                    difficultyMultiplier = 1.0f + (i * 0.15f),
                    isBossFloor = i == 7
                };

                if (i == 7)
                {
                    floor.boss = new Boss
                    {
                        bossId = 102,
                        bossName = "Shadow Lord",
                        hp = 300,
                        damage = 30,
                        armor = 0.4f,
                        specialAttacks = new List<string> { "Shadow Bolt", "Darkness", "Clone" },
                        lootDropIds = new List<int> { 5, 6, 7 }
                    };
                }

                dungeons[2].floors.Add(floor);
            }

            // DUNGEON 3: Volcanic Depths
            dungeons[3] = new Dungeon
            {
                dungeonId = 3,
                dungeonName = "Volcanic Depths",
                description = "Deep volcanic caverns with lava",
                minLevel = 30,
                maxLevel = 40,
                totalFloors = 10,
                difficulty = 2.5f,
                reward = new DungeonReward { xpReward = 3000, goldReward = 1500, premiumCurrencyReward = 150 }
            };

            // Add floors to Volcanic Depths
            for (int i = 1; i <= 10; i++)
            {
                DungeonFloor floor = new DungeonFloor
                {
                    floorNumber = i,
                    floorName = $"Floor {i}",
                    enemyCount = 10 + (i * 2),
                    difficultyMultiplier = 1.0f + (i * 0.1f),
                    isBossFloor = i == 10
                };

                if (i == 10)
                {
                    floor.boss = new Boss
                    {
                        bossId = 103,
                        bossName = "Magma Lord",
                        hp = 400,
                        damage = 40,
                        armor = 0.5f,
                        specialAttacks = new List<string> { "Lava Burst", "Inferno", "Earthquake" },
                        lootDropIds = new List<int> { 10, 11, 12 }
                    };
                }

                dungeons[3].floors.Add(floor);
            }

            // DUNGEON 4: Frozen Abyss
            dungeons[4] = new Dungeon
            {
                dungeonId = 4,
                dungeonName = "Frozen Abyss",
                description = "A frozen abyss of ice and cold",
                minLevel = 40,
                maxLevel = 50,
                totalFloors = 12,
                difficulty = 3.0f,
                reward = new DungeonReward { xpReward = 4000, goldReward = 2000, premiumCurrencyReward = 200 }
            };

            // Add floors to Frozen Abyss
            for (int i = 1; i <= 12; i++)
            {
                DungeonFloor floor = new DungeonFloor
                {
                    floorNumber = i,
                    floorName = $"Floor {i}",
                    enemyCount = 12 + (i * 2),
                    difficultyMultiplier = 1.0f + (i * 0.08f),
                    isBossFloor = i == 12
                };

                if (i == 12)
                {
                    floor.boss = new Boss
                    {
                        bossId = 104,
                        bossName = "Frost King",
                        hp = 500,
                        damage = 45,
                        armor = 0.6f,
                        specialAttacks = new List<string> { "Blizzard", "Freeze", "Ice Storm" },
                        lootDropIds = new List<int> { 15, 16, 17 }
                    };
                }

                dungeons[4].floors.Add(floor);
            }

            // DUNGEON 5: Divine Tower
            dungeons[5] = new Dungeon
            {
                dungeonId = 5,
                dungeonName = "Divine Tower",
                description = "A tower reaching to the heavens",
                minLevel = 50,
                maxLevel = 60,
                totalFloors = 15,
                difficulty = 3.5f,
                reward = new DungeonReward { xpReward = 5000, goldReward = 2500, premiumCurrencyReward = 250 }
            };

            // Add floors to Divine Tower
            for (int i = 1; i <= 15; i++)
            {
                DungeonFloor floor = new DungeonFloor
                {
                    floorNumber = i,
                    floorName = $"Floor {i}",
                    enemyCount = 15 + (i * 2),
                    difficultyMultiplier = 1.0f + (i * 0.07f),
                    isBossFloor = i == 15
                };

                if (i == 15)
                {
                    floor.boss = new Boss
                    {
                        bossId = 105,
                        bossName = "Sky Deity",
                        hp = 600,
                        damage = 50,
                        armor = 0.7f,
                        specialAttacks = new List<string> { "Divine Wrath", "Celestial Beam", "Heaven's Judgment" },
                        lootDropIds = new List<int> { 20, 21, 22 }
                    };
                }

                dungeons[5].floors.Add(floor);
            }

            Debug.Log("Dungeons initialized with 5 dungeons");
        }

        /// <summary>
        /// Start a dungeon.
        /// </summary>
        public void StartDungeon(int dungeonId)
        {
            if (!dungeons.ContainsKey(dungeonId))
            {
                Debug.LogError($"Dungeon {dungeonId} not found");
                return;
            }

            Dungeon dungeon = dungeons[dungeonId];

            // Check level requirement
            if (PlayerStats.Instance.Level < dungeon.minLevel)
            {
                Debug.LogWarning($"Player level {PlayerStats.Instance.Level} is below minimum {dungeon.minLevel}");
                return;
            }

            currentDungeonId = dungeonId;
            currentFloor = 1;
            dungeonInProgress = true;

            Debug.Log($"Starting dungeon: {dungeon.dungeonName}");
            OnDungeonStarted?.Invoke(dungeon);
        }

        /// <summary>
        /// Complete current floor.
        /// </summary>
        public void CompleteFloor()
        {
            if (!dungeonInProgress || currentDungeonId == -1)
                return;

            Dungeon dungeon = dungeons[currentDungeonId];

            if (currentFloor < dungeon.totalFloors)
            {
                currentFloor++;
                Debug.Log($"Floor {currentFloor - 1} completed. Moving to floor {currentFloor}");
                OnFloorCompleted?.Invoke(currentFloor - 1);
            }
            else
            {
                CompleteDungeon();
            }
        }

        /// <summary>
        /// Complete the dungeon.
        /// </summary>
        public void CompleteDungeon()
        {
            if (!dungeonInProgress || currentDungeonId == -1)
                return;

            Dungeon dungeon = dungeons[currentDungeonId];
            dungeon.completed = true;
            dungeonInProgress = false;

            // Award rewards
            PlayerStats.Instance.AddExperience(dungeon.reward.xpReward);
            PlayerStats.Instance.AddGold(dungeon.reward.goldReward);

            Debug.Log($"Dungeon completed: {dungeon.dungeonName}");
            OnDungeonCompleted?.Invoke(dungeon);
        }

        /// <summary>
        /// Get current dungeon.
        /// </summary>
        public Dungeon GetCurrentDungeon()
        {
            if (currentDungeonId != -1 && dungeons.ContainsKey(currentDungeonId))
                return dungeons[currentDungeonId];
            return null;
        }

        /// <summary>
        /// Get dungeon by ID.
        /// </summary>
        public Dungeon GetDungeon(int dungeonId)
        {
            if (dungeons.ContainsKey(dungeonId))
                return dungeons[dungeonId];
            return null;
        }

        /// <summary>
        /// Get all dungeons.
        /// </summary>
        public List<Dungeon> GetAllDungeons()
        {
            return new List<Dungeon>(dungeons.Values);
        }

        /// <summary>
        /// Get available dungeons for player level.
        /// </summary>
        public List<Dungeon> GetAvailableDungeons()
        {
            List<Dungeon> available = new();
            int playerLevel = PlayerStats.Instance.Level;

            foreach (var dungeon in dungeons.Values)
            {
                if (playerLevel >= dungeon.minLevel)
                    available.Add(dungeon);
            }

            return available;
        }

        /// <summary>
        /// Get current floor.
        /// </summary>
        public int GetCurrentFloor()
        {
            return currentFloor;
        }

        /// <summary>
        /// Check if dungeon is in progress.
        /// </summary>
        public bool IsDungeonInProgress()
        {
            return dungeonInProgress;
        }

        // Events
        public event Action<Dungeon> OnDungeonStarted;
        public event Action<Dungeon> OnDungeonCompleted;
        public event Action<int> OnFloorCompleted;
    }
}
