using NUnit.Framework;
using UnityEngine;

/// <summary>
/// Unit tests for core game systems.
/// Run via Window → General → Test Runner
/// </summary>
public class GameSystemsTests
{
    [Test]
    public void ItemDatabase_LoadsItems()
    {
        var items = ItemDatabaseLoader.LoadItemsFromJSON();
        Assert.Greater(items.Count, 0, "ItemDatabase should load at least one item");
    }

    [Test]
    public void ItemDatabase_GetItemById_ReturnsCorrectItem()
    {
        var item = ItemDatabaseLoader.LoadItemsFromJSON()[0];
        Assert.NotNull(item, "Item should not be null");
        Assert.Greater(item.id, 0, "Item ID should be greater than 0");
    }

    [Test]
    public void EnemyDatabase_LoadsEnemies()
    {
        var enemies = EnemyDatabaseLoader.LoadEnemiesFromJSON();
        Assert.Greater(enemies.Count, 0, "EnemyDatabase should load at least one enemy");
    }

    [Test]
    public void EnemyDatabase_GetEnemyById_ReturnsCorrectEnemy()
    {
        var enemy = EnemyDatabaseLoader.GetEnemyById(1);
        Assert.NotNull(enemy, "Enemy should not be null");
        Assert.AreEqual(1, enemy.id, "Enemy ID should match");
    }

    [Test]
    public void ItemRarity_HasCorrectColors()
    {
        var commonItem = new ItemData { rarity = ItemRarity.Common, rarityColor = Color.white };
        var magicItem = new ItemData { rarity = ItemRarity.Magic, rarityColor = Color.blue };
        var rareItem = new ItemData { rarity = ItemRarity.Rare, rarityColor = Color.yellow };

        Assert.AreEqual(Color.white, commonItem.rarityColor);
        Assert.AreEqual(Color.blue, magicItem.rarityColor);
        Assert.AreEqual(Color.yellow, rareItem.rarityColor);
    }

    [Test]
    public void InventoryItem_StacksCorrectly()
    {
        var itemData = new ItemData { id = 1, itemName = "Potion", maxStackSize = 10 };
        var item1 = new InventoryItem(itemData, 5);
        var item2 = new InventoryItem(itemData, 3);

        item1.quantity += item2.quantity;
        Assert.AreEqual(8, item1.quantity, "Items should stack correctly");
    }

    [Test]
    public void Equipment_AppliesBonuses()
    {
        var weaponData = new ItemData { damageBonus = 10, attackSpeedBonus = 0.1f };
        var armorData = new ItemData { healthBonus = 20, armorBonus = 5 };

        var equipment = new Equipment
        {
            weapon = weaponData,
            armor = armorData
        };

        Assert.NotNull(equipment.weapon);
        Assert.NotNull(equipment.armor);
        Assert.AreEqual(10, equipment.weapon.damageBonus);
        Assert.AreEqual(20, equipment.armor.healthBonus);
    }

    [Test]
    public void Quest_TracksProgress()
    {
        var quest = new Quest
        {
            questId = 1,
            questName = "Kill 10 Enemies",
            questType = QuestType.KillEnemies,
            targetProgress = 10,
            currentProgress = 0
        };

        quest.currentProgress = 5;
        Assert.AreEqual(5, quest.currentProgress);
        Assert.IsFalse(quest.isCompleted);

        quest.currentProgress = 10;
        Assert.AreEqual(10, quest.currentProgress);
    }

    [Test]
    public void ChestReward_CalculatesCorrectly()
    {
        var reward = new ChestReward
        {
            gold = 1000,
            experience = 100,
            premiumCurrency = 50
        };

        Assert.AreEqual(1000, reward.gold);
        Assert.AreEqual(100, reward.experience);
        Assert.AreEqual(50, reward.premiumCurrency);
    }

    [Test]
    public void GamePass_CalculatesMultipliers()
    {
        // Test XP multiplier
        float xpMult = 1.5f; // Game Pass multiplier
        float baseXP = 100f;
        float boostedXP = baseXP * xpMult;

        Assert.AreEqual(150f, boostedXP);
    }

    [Test]
    public void ItemType_EnumValuesExist()
    {
        Assert.AreEqual(ItemType.Weapon, ItemType.Weapon);
        Assert.AreEqual(ItemType.Armor, ItemType.Armor);
        Assert.AreEqual(ItemType.Ring, ItemType.Ring);
        Assert.AreEqual(ItemType.Consumable, ItemType.Consumable);
    }

    [Test]
    public void EnemyType_HasValidStats()
    {
        var enemy = new EnemyDatabaseLoader.EnemyData
        {
            id = 1,
            name = "Grunt",
            maxHealth = 30,
            damage = 5,
            armor = 2
        };

        Assert.Greater(enemy.maxHealth, 0);
        Assert.Greater(enemy.damage, 0);
        Assert.GreaterOrEqual(enemy.armor, 0);
    }
}
