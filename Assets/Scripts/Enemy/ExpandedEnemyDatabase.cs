using UnityEngine;
using System.Collections.Generic;

namespace HackSlash.Enemy
{
    /// <summary>
    /// Expanded enemy database with 20+ enemy types across 3 tiers.
    /// Manages enemy data, stats, and difficulty scaling.
    /// </summary>
    public class ExpandedEnemyDatabase : MonoBehaviour
    {
        public static ExpandedEnemyDatabase Instance { get; private set; }

        [System.Serializable]
        public class EnemyType
        {
            public int enemyId;
            public string enemyName;
            public int baseHP;
            public int baseDamage;
            public float baseArmor;
            public float baseSpeed;
            public float attackRange;
            public float aggroRange;
            public int baseXPReward;
            public int baseGoldReward;
            public List<int> lootTableIds = new();
            public string tier; // Tier 1, 2, 3
            public List<string> specialAbilities = new();
        }

        private Dictionary<int, EnemyType> enemies = new();

        void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeEnemies();
        }

        /// <summary>
        /// Initialize all enemy types.
        /// </summary>
        private void InitializeEnemies()
        {
            // TIER 1: Basic Enemies (Levels 1-15)
            
            // Enemy 1: Goblin
            enemies[1] = new EnemyType
            {
                enemyId = 1,
                enemyName = "Goblin",
                baseHP = 20,
                baseDamage = 5,
                baseArmor = 0.1f,
                baseSpeed = 4f,
                attackRange = 1.5f,
                aggroRange = 8f,
                baseXPReward = 50,
                baseGoldReward = 25,
                tier = "Tier 1",
                specialAbilities = new List<string> { "Quick Attack" }
            };

            // Enemy 2: Skeleton
            enemies[2] = new EnemyType
            {
                enemyId = 2,
                enemyName = "Skeleton",
                baseHP = 25,
                baseDamage = 6,
                baseArmor = 0.2f,
                baseSpeed = 3.5f,
                attackRange = 2f,
                aggroRange = 10f,
                baseXPReward = 60,
                baseGoldReward = 30,
                tier = "Tier 1",
                specialAbilities = new List<string> { "Bone Throw" }
            };

            // Enemy 3: Zombie
            enemies[3] = new EnemyType
            {
                enemyId = 3,
                enemyName = "Zombie",
                baseHP = 30,
                baseDamage = 7,
                baseArmor = 0.15f,
                baseSpeed = 2.5f,
                attackRange = 1f,
                aggroRange = 6f,
                baseXPReward = 55,
                baseGoldReward = 28,
                tier = "Tier 1",
                specialAbilities = new List<string> { "Bite" }
            };

            // Enemy 4: Orc
            enemies[4] = new EnemyType
            {
                enemyId = 4,
                enemyName = "Orc",
                baseHP = 35,
                baseDamage = 8,
                baseArmor = 0.25f,
                baseSpeed = 3f,
                attackRange = 2f,
                aggroRange = 10f,
                baseXPReward = 70,
                baseGoldReward = 35,
                tier = "Tier 1",
                specialAbilities = new List<string> { "Power Attack" }
            };

            // Enemy 5: Bat
            enemies[5] = new EnemyType
            {
                enemyId = 5,
                enemyName = "Bat",
                baseHP = 15,
                baseDamage = 4,
                baseArmor = 0.05f,
                baseSpeed = 5.5f,
                attackRange = 1.5f,
                aggroRange = 12f,
                baseXPReward = 40,
                baseGoldReward = 20,
                tier = "Tier 1",
                specialAbilities = new List<string> { "Swoop" }
            };

            // TIER 2: Advanced Enemies (Levels 15-35)

            // Enemy 6: Wraith
            enemies[6] = new EnemyType
            {
                enemyId = 6,
                enemyName = "Wraith",
                baseHP = 50,
                baseDamage = 12,
                baseArmor = 0.3f,
                baseSpeed = 4f,
                attackRange = 2.5f,
                aggroRange = 12f,
                baseXPReward = 150,
                baseGoldReward = 75,
                tier = "Tier 2",
                specialAbilities = new List<string> { "Spirit Drain", "Teleport" }
            };

            // Enemy 7: Troll
            enemies[7] = new EnemyType
            {
                enemyId = 7,
                enemyName = "Troll",
                baseHP = 80,
                baseDamage = 15,
                baseArmor = 0.4f,
                baseSpeed = 2.5f,
                attackRange = 2.5f,
                aggroRange = 10f,
                baseXPReward = 200,
                baseGoldReward = 100,
                tier = "Tier 2",
                specialAbilities = new List<string> { "Regenerate", "Smash" }
            };

            // Enemy 8: Demon
            enemies[8] = new EnemyType
            {
                enemyId = 8,
                enemyName = "Demon",
                baseHP = 70,
                baseDamage = 18,
                baseArmor = 0.35f,
                baseSpeed = 4.5f,
                attackRange = 2f,
                aggroRange = 14f,
                baseXPReward = 250,
                baseGoldReward = 125,
                tier = "Tier 2",
                specialAbilities = new List<string> { "Fireball", "Dark Aura" }
            };

            // Enemy 9: Lich
            enemies[9] = new EnemyType
            {
                enemyId = 9,
                enemyName = "Lich",
                baseHP = 60,
                baseDamage = 20,
                baseArmor = 0.25f,
                baseSpeed = 3.5f,
                attackRange = 3f,
                aggroRange = 15f,
                baseXPReward = 300,
                baseGoldReward = 150,
                tier = "Tier 2",
                specialAbilities = new List<string> { "Curse", "Summon Undead" }
            };

            // Enemy 10: Harpy
            enemies[10] = new EnemyType
            {
                enemyId = 10,
                enemyName = "Harpy",
                baseHP = 45,
                baseDamage = 14,
                baseArmor = 0.2f,
                baseSpeed = 5f,
                attackRange = 2.5f,
                aggroRange = 13f,
                baseXPReward = 180,
                baseGoldReward = 90,
                tier = "Tier 2",
                specialAbilities = new List<string> { "Aerial Attack", "Screech" }
            };

            // TIER 3: Elite Enemies (Levels 35-60)

            // Enemy 11: Dragon
            enemies[11] = new EnemyType
            {
                enemyId = 11,
                enemyName = "Dragon",
                baseHP = 200,
                baseDamage = 30,
                baseArmor = 0.6f,
                baseSpeed = 3.5f,
                attackRange = 3.5f,
                aggroRange = 20f,
                baseXPReward = 1000,
                baseGoldReward = 500,
                tier = "Tier 3",
                specialAbilities = new List<string> { "Fire Breath", "Wing Attack", "Tail Swipe" }
            };

            // Enemy 12: Titan
            enemies[12] = new EnemyType
            {
                enemyId = 12,
                enemyName = "Titan",
                baseHP = 250,
                baseDamage = 35,
                baseArmor = 0.7f,
                baseSpeed = 2.5f,
                attackRange = 3f,
                aggroRange = 18f,
                baseXPReward = 1200,
                baseGoldReward = 600,
                tier = "Tier 3",
                specialAbilities = new List<string> { "Earthquake", "Crush", "Roar" }
            };

            // Enemy 13: Lich King
            enemies[13] = new EnemyType
            {
                enemyId = 13,
                enemyName = "Lich King",
                baseHP = 180,
                baseDamage = 32,
                baseArmor = 0.5f,
                baseSpeed = 4f,
                attackRange = 3.5f,
                aggroRange = 20f,
                baseXPReward = 1500,
                baseGoldReward = 750,
                tier = "Tier 3",
                specialAbilities = new List<string> { "Death Curse", "Summon Army", "Time Stop" }
            };

            // Enemy 14: Abomination
            enemies[14] = new EnemyType
            {
                enemyId = 14,
                enemyName = "Abomination",
                baseHP = 220,
                baseDamage = 28,
                baseArmor = 0.65f,
                baseSpeed = 3f,
                attackRange = 2.5f,
                aggroRange = 16f,
                baseXPReward = 1100,
                baseGoldReward = 550,
                tier = "Tier 3",
                specialAbilities = new List<string> { "Mutation", "Poison Cloud", "Regenerate" }
            };

            // Enemy 15: Nightmare
            enemies[15] = new EnemyType
            {
                enemyId = 15,
                enemyName = "Nightmare",
                baseHP = 190,
                baseDamage = 33,
                baseArmor = 0.55f,
                baseSpeed = 5f,
                attackRange = 2.5f,
                aggroRange = 18f,
                baseXPReward = 1400,
                baseGoldReward = 700,
                tier = "Tier 3",
                specialAbilities = new List<string> { "Shadow Clone", "Nightmare Aura", "Dash Attack" }
            };

            // Enemy 16: Golem
            enemies[16] = new EnemyType
            {
                enemyId = 16,
                enemyName = "Golem",
                baseHP = 240,
                baseDamage = 25,
                baseArmor = 0.8f,
                baseSpeed = 2f,
                attackRange = 2f,
                aggroRange = 12f,
                baseXPReward = 900,
                baseGoldReward = 450,
                tier = "Tier 3",
                specialAbilities = new List<string> { "Stone Armor", "Ground Slam" }
            };

            // Enemy 17: Banshee
            enemies[17] = new EnemyType
            {
                enemyId = 17,
                enemyName = "Banshee",
                baseHP = 150,
                baseDamage = 29,
                baseArmor = 0.3f,
                baseSpeed = 5.5f,
                attackRange = 3f,
                aggroRange = 20f,
                baseXPReward = 1300,
                baseGoldReward = 650,
                tier = "Tier 3",
                specialAbilities = new List<string> { "Wail", "Life Drain", "Invisibility" }
            };

            // Enemy 18: Chimera
            enemies[18] = new EnemyType
            {
                enemyId = 18,
                enemyName = "Chimera",
                baseHP = 210,
                baseDamage = 31,
                baseArmor = 0.5f,
                baseSpeed = 4.5f,
                attackRange = 2.5f,
                aggroRange = 16f,
                baseXPReward = 1350,
                baseGoldReward = 675,
                tier = "Tier 3",
                specialAbilities = new List<string> { "Multi-Head Attack", "Fire Breath", "Poison Spit" }
            };

            // Enemy 19: Hydra
            enemies[19] = new EnemyType
            {
                enemyId = 19,
                enemyName = "Hydra",
                baseHP = 280,
                baseDamage = 36,
                baseArmor = 0.6f,
                baseSpeed = 3.5f,
                attackRange = 3f,
                aggroRange = 18f,
                baseXPReward = 1600,
                baseGoldReward = 800,
                tier = "Tier 3",
                specialAbilities = new List<string> { "Regenerate Heads", "Poison Spray", "Multi-Attack" }
            };

            // Enemy 20: Dark Lord
            enemies[20] = new EnemyType
            {
                enemyId = 20,
                enemyName = "Dark Lord",
                baseHP = 300,
                baseDamage = 40,
                baseArmor = 0.75f,
                baseSpeed = 4f,
                attackRange = 3.5f,
                aggroRange = 20f,
                baseXPReward = 2000,
                baseGoldReward = 1000,
                tier = "Tier 3",
                specialAbilities = new List<string> { "Dark Ritual", "Summon Minions", "Reality Tear", "Ultimate Attack" }
            };

            Debug.Log("Enemy database initialized with 20 enemy types");
        }

        /// <summary>
        /// Get enemy by ID.
        /// </summary>
        public EnemyType GetEnemy(int enemyId)
        {
            if (enemies.ContainsKey(enemyId))
                return enemies[enemyId];
            return null;
        }

        /// <summary>
        /// Get random enemy for tier.
        /// </summary>
        public EnemyType GetRandomEnemyForTier(string tier)
        {
            List<EnemyType> tierEnemies = new();
            foreach (var enemy in enemies.Values)
            {
                if (enemy.tier == tier)
                    tierEnemies.Add(enemy);
            }

            if (tierEnemies.Count == 0)
                return null;

            return tierEnemies[Random.Range(0, tierEnemies.Count)];
        }

        /// <summary>
        /// Get all enemies.
        /// </summary>
        public List<EnemyType> GetAllEnemies()
        {
            return new List<EnemyType>(enemies.Values);
        }

        /// <summary>
        /// Get enemies by tier.
        /// </summary>
        public List<EnemyType> GetEnemiesByTier(string tier)
        {
            List<EnemyType> result = new();
            foreach (var enemy in enemies.Values)
            {
                if (enemy.tier == tier)
                    result.Add(enemy);
            }
            return result;
        }

        /// <summary>
        /// Scale enemy stats by difficulty.
        /// </summary>
        public EnemyType ScaleEnemyStats(EnemyType enemy, float difficultyMultiplier)
        {
            EnemyType scaled = new EnemyType
            {
                enemyId = enemy.enemyId,
                enemyName = enemy.enemyName,
                baseHP = (int)(enemy.baseHP * difficultyMultiplier),
                baseDamage = (int)(enemy.baseDamage * difficultyMultiplier),
                baseArmor = enemy.baseArmor * difficultyMultiplier,
                baseSpeed = enemy.baseSpeed,
                attackRange = enemy.attackRange,
                aggroRange = enemy.aggroRange,
                baseXPReward = (int)(enemy.baseXPReward * difficultyMultiplier),
                baseGoldReward = (int)(enemy.baseGoldReward * difficultyMultiplier),
                tier = enemy.tier,
                specialAbilities = new List<string>(enemy.specialAbilities)
            };
            return scaled;
        }
    }
}
