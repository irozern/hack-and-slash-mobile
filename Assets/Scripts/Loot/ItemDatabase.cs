using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Centralized database for all items in the game.
/// Loads from JSON and provides item lookup.
/// </summary>
public class ItemDatabase : MonoBehaviour
{
    public static ItemDatabase Instance { get; private set; }

    [SerializeField] private List<ItemData> allItems = new List<ItemData>();

    private Dictionary<int, ItemData> itemDictionary = new Dictionary<int, ItemData>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        InitializeDatabase();
    }

    private void InitializeDatabase()
    {
        // Load items from JSON
        allItems = ItemDatabaseLoader.LoadItemsFromJSON();

        // Build dictionary for fast lookup
        foreach (var item in allItems)
        {
            itemDictionary[item.id] = item;
        }

        Debug.Log($"ItemDatabase initialized with {allItems.Count} items");
    }

    /// <summary>
    /// Gets an item by ID.
    /// </summary>
    public ItemData GetItemById(int id)
    {
        if (itemDictionary.TryGetValue(id, out var item))
            return item;

        Debug.LogWarning($"Item with ID {id} not found!");
        return null;
    }

    /// <summary>
    /// Gets all items of a specific rarity.
    /// </summary>
    public List<ItemData> GetItemsByRarity(ItemRarity rarity)
    {
        List<ItemData> result = new List<ItemData>();
        foreach (var item in allItems)
        {
            if (item.rarity == rarity)
                result.Add(item);
        }
        return result;
    }

    /// <summary>
    /// Gets all items of a specific type.
    /// </summary>
    public List<ItemData> GetItemsByType(ItemType type)
    {
        List<ItemData> result = new List<ItemData>();
        foreach (var item in allItems)
        {
            if (item.itemType == type)
                result.Add(item);
        }
        return result;
    }

    /// <summary>
    /// Gets a random item with weighted rarity distribution.
    /// </summary>
    public ItemData GetRandomItem()
    {
        float roll = Random.value;

        if (roll < Constants.Loot.LEGENDARY_DROP_RATE)
            return GetRandomItemOfRarity(ItemRarity.Legendary);
        else if (roll < Constants.Loot.LEGENDARY_DROP_RATE + Constants.Loot.RARE_DROP_RATE)
            return GetRandomItemOfRarity(ItemRarity.Rare);
        else if (roll < Constants.Loot.LEGENDARY_DROP_RATE + Constants.Loot.RARE_DROP_RATE + Constants.Loot.MAGIC_DROP_RATE)
            return GetRandomItemOfRarity(ItemRarity.Magic);
        else
            return GetRandomItemOfRarity(ItemRarity.Common);
    }

    private ItemData GetRandomItemOfRarity(ItemRarity rarity)
    {
        List<ItemData> items = GetItemsByRarity(rarity);
        if (items.Count == 0)
            return null;

        return items[Random.Range(0, items.Count)];
    }

    public int GetItemCount() => allItems.Count;
}
