using UnityEngine;
using System;

/// <summary>
/// Manages premium chests and reward distribution.
/// Handles rare and legendary chest opening with rewards.
/// </summary>
public class PremiumChestManager : MonoBehaviour
{
    public static PremiumChestManager Instance { get; private set; }

    [SerializeField] private int commonChestCount = 0;
    [SerializeField] private int rareChestCount = 0;
    [SerializeField] private int legendaryChestCount = 0;

    // Events
    public event Action<ChestType, ChestReward> OnChestOpened;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        LoadChestData();
    }

    /// <summary>
    /// Adds a chest to inventory.
    /// </summary>
    public void AddChest(ChestType chestType)
    {
        switch (chestType)
        {
            case ChestType.Common:
                commonChestCount++;
                break;
            case ChestType.Rare:
                rareChestCount++;
                break;
            case ChestType.Legendary:
                legendaryChestCount++;
                break;
        }

        SaveChestData();
    }

    /// <summary>
    /// Opens a chest and returns rewards.
    /// </summary>
    public ChestReward OpenChest(ChestType chestType)
    {
        // Check if player has chest
        if (!RemoveChest(chestType))
            return null;

        // Generate reward
        ChestReward reward = GenerateReward(chestType);
        OnChestOpened?.Invoke(chestType, reward);

        // Apply rewards
        ApplyReward(reward);

        SaveChestData();
        return reward;
    }

    /// <summary>
    /// Generates reward based on chest type.
    /// </summary>
    private ChestReward GenerateReward(ChestType chestType)
    {
        ChestReward reward = new ChestReward();

        switch (chestType)
        {
            case ChestType.Common:
                reward.gold = Constants.Monetization.COMMON_CHEST_GOLD;
                reward.experience = 50;
                break;

            case ChestType.Rare:
                reward.gold = Constants.Monetization.RARE_CHEST_GOLD;
                reward.experience = 200;
                reward.items = new ItemData[1] { ItemDatabase.Instance.GetRandomItem() };
                break;

            case ChestType.Legendary:
                reward.gold = Constants.Monetization.LEGENDARY_CHEST_GOLD;
                reward.experience = 500;
                reward.items = new ItemData[2] 
                { 
                    ItemDatabase.Instance.GetRandomItem(),
                    ItemDatabase.Instance.GetRandomItem()
                };
                reward.premiumCurrency = 100;
                break;
        }

        return reward;
    }

    /// <summary>
    /// Applies chest rewards to player.
    /// </summary>
    private void ApplyReward(ChestReward reward)
    {
        GameObject playerObj = GameManager.Instance.GetPlayer();
        if (playerObj == null) return;

        PlayerStats playerStats = playerObj.GetComponent<PlayerStats>();
        if (playerStats != null)
        {
            playerStats.AddGold(reward.gold);
            playerStats.GainExperience(reward.experience * (int)GamePassManager.Instance.GetXPMultiplier());
        }

        // Add items to inventory
        if (reward.items != null)
        {
            foreach (var item in reward.items)
            {
                if (item != null)
                    InventoryManager.Instance.AddItem(item);
            }
        }

        // Add premium currency
        if (reward.premiumCurrency > 0)
        {
            GamePassManager.Instance.AddPremiumCurrency(reward.premiumCurrency);
        }
    }

    private bool RemoveChest(ChestType chestType)
    {
        switch (chestType)
        {
            case ChestType.Common:
                if (commonChestCount > 0) { commonChestCount--; return true; }
                break;
            case ChestType.Rare:
                if (rareChestCount > 0) { rareChestCount--; return true; }
                break;
            case ChestType.Legendary:
                if (legendaryChestCount > 0) { legendaryChestCount--; return true; }
                break;
        }
        return false;
    }

    // ==================== PERSISTENCE ====================

    private void SaveChestData()
    {
        PlayerPrefs.SetInt("Chest_Common", commonChestCount);
        PlayerPrefs.SetInt("Chest_Rare", rareChestCount);
        PlayerPrefs.SetInt("Chest_Legendary", legendaryChestCount);
        PlayerPrefs.Save();
    }

    private void LoadChestData()
    {
        commonChestCount = PlayerPrefs.GetInt("Chest_Common", 0);
        rareChestCount = PlayerPrefs.GetInt("Chest_Rare", 0);
        legendaryChestCount = PlayerPrefs.GetInt("Chest_Legendary", 0);
    }

    // ==================== GETTERS ====================

    public int GetChestCount(ChestType type)
    {
        switch (type)
        {
            case ChestType.Common: return commonChestCount;
            case ChestType.Rare: return rareChestCount;
            case ChestType.Legendary: return legendaryChestCount;
            default: return 0;
        }
    }
}

public enum ChestType
{
    Common,
    Rare,
    Legendary
}

/// <summary>
/// Represents rewards from opening a chest.
/// </summary>
[System.Serializable]
public class ChestReward
{
    public long gold;
    public float experience;
    public ItemData[] items;
    public int premiumCurrency;
}
