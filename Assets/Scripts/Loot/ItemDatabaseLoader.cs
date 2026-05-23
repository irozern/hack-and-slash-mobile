using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Loads item database from JSON file.
/// Converts JSON data to ItemData objects.
/// </summary>
public class ItemDatabaseLoader
{
    [System.Serializable]
    private class ItemDataJSON
    {
        public List<ItemDataEntry> items;
    }

    [System.Serializable]
    private class ItemDataEntry
    {
        public int id;
        public string itemName;
        public string description;
        public string itemType;
        public string rarity;
        public int maxStackSize;
        public float healthBonus;
        public float damageBonus;
        public float armorBonus;
        public float moveSpeedBonus;
        public float attackSpeedBonus;
        public int sellPrice;
        public int buyPrice;
    }

    /// <summary>
    /// Loads items from JSON resource file.
    /// </summary>
    public static List<ItemData> LoadItemsFromJSON()
    {
        TextAsset jsonFile = Resources.Load<TextAsset>("Data/items");
        if (jsonFile == null)
        {
            Debug.LogError("Could not load items.json from Resources/Data/");
            return new List<ItemData>();
        }

        ItemDataJSON jsonData = JsonUtility.FromJson<ItemDataJSON>(jsonFile.text);
        List<ItemData> items = new List<ItemData>();

        foreach (var entry in jsonData.items)
        {
            ItemData item = new ItemData
            {
                id = entry.id,
                itemName = entry.itemName,
                description = entry.description,
                itemType = (ItemType)System.Enum.Parse(typeof(ItemType), entry.itemType),
                rarity = (ItemRarity)System.Enum.Parse(typeof(ItemRarity), entry.rarity),
                maxStackSize = entry.maxStackSize,
                healthBonus = entry.healthBonus,
                damageBonus = entry.damageBonus,
                armorBonus = entry.armorBonus,
                moveSpeedBonus = entry.moveSpeedBonus,
                attackSpeedBonus = entry.attackSpeedBonus,
                sellPrice = entry.sellPrice,
                buyPrice = entry.buyPrice,
                rarityColor = GetRarityColor(entry.rarity)
            };

            items.Add(item);
        }

        Debug.Log($"Loaded {items.Count} items from JSON");
        return items;
    }

    private static Color GetRarityColor(string rarity)
    {
        switch (rarity)
        {
            case "Common": return Color.white;
            case "Magic": return Color.blue;
            case "Rare": return Color.yellow;
            case "Legendary": return new Color(1f, 0.5f, 0f); // Orange
            default: return Color.white;
        }
    }
}
