using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Loads enemy database from JSON file.
/// Converts JSON data to EnemyData objects.
/// </summary>
public class EnemyDatabaseLoader
{
    [System.Serializable]
    private class EnemyDataJSON
    {
        public List<EnemyDataEntry> enemies;
    }

    [System.Serializable]
    private class EnemyDataEntry
    {
        public int id;
        public string name;
        public string description;
        public int level;
        public float maxHealth;
        public float damage;
        public float armor;
        public float moveSpeed;
        public float attackSpeed;
        public float attackRange;
        public float aggroRange;
        public float patrolRadius;
        public int experience;
        public int goldReward;
        public float lootDropRate;
    }

    /// <summary>
    /// Represents enemy configuration data.
    /// </summary>
    [System.Serializable]
    public class EnemyData
    {
        public int id;
        public string name;
        public string description;
        public int level;
        public float maxHealth;
        public float damage;
        public float armor;
        public float moveSpeed;
        public float attackSpeed;
        public float attackRange;
        public float aggroRange;
        public float patrolRadius;
        public int experience;
        public int goldReward;
        public float lootDropRate;
    }

    /// <summary>
    /// Loads enemies from JSON resource file.
    /// </summary>
    public static List<EnemyData> LoadEnemiesFromJSON()
    {
        TextAsset jsonFile = Resources.Load<TextAsset>("Data/enemies");
        if (jsonFile == null)
        {
            Debug.LogError("Could not load enemies.json from Resources/Data/");
            return new List<EnemyData>();
        }

        EnemyDataJSON jsonData = JsonUtility.FromJson<EnemyDataJSON>(jsonFile.text);
        List<EnemyData> enemies = new List<EnemyData>();

        foreach (var entry in jsonData.enemies)
        {
            EnemyData enemy = new EnemyData
            {
                id = entry.id,
                name = entry.name,
                description = entry.description,
                level = entry.level,
                maxHealth = entry.maxHealth,
                damage = entry.damage,
                armor = entry.armor,
                moveSpeed = entry.moveSpeed,
                attackSpeed = entry.attackSpeed,
                attackRange = entry.attackRange,
                aggroRange = entry.aggroRange,
                patrolRadius = entry.patrolRadius,
                experience = entry.experience,
                goldReward = entry.goldReward,
                lootDropRate = entry.lootDropRate
            };

            enemies.Add(enemy);
        }

        Debug.Log($"Loaded {enemies.Count} enemy types from JSON");
        return enemies;
    }

    /// <summary>
    /// Gets a random enemy type.
    /// </summary>
    public static EnemyData GetRandomEnemy()
    {
        List<EnemyData> enemies = LoadEnemiesFromJSON();
        if (enemies.Count == 0)
            return null;

        return enemies[Random.Range(0, enemies.Count)];
    }

    /// <summary>
    /// Gets enemy by ID.
    /// </summary>
    public static EnemyData GetEnemyById(int id)
    {
        List<EnemyData> enemies = LoadEnemiesFromJSON();
        foreach (var enemy in enemies)
        {
            if (enemy.id == id)
                return enemy;
        }
        return null;
    }
}
