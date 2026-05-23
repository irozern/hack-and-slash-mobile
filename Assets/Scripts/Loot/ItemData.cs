using UnityEngine;

/// <summary>
/// Defines item data structure for loot system.
/// Supports equipment, consumables, and crafting materials.
/// </summary>
[System.Serializable]
public class ItemData
{
    public int id;
    public string itemName;
    public string description;
    public ItemType itemType;
    public ItemRarity rarity;
    public int maxStackSize = 1;

    // Stats
    public float healthBonus;
    public float damageBonus;
    public float armorBonus;
    public float moveSpeedBonus;
    public float attackSpeedBonus;

    // Pricing
    public int sellPrice;
    public int buyPrice;

    // Visual
    public Sprite icon;
    public Color rarityColor;
}

public enum ItemType
{
    Weapon,
    Armor,
    Ring,
    Consumable,
    Material,
    Quest
}

public enum ItemRarity
{
    Common,      // White
    Magic,       // Blue
    Rare,        // Yellow
    Legendary    // Orange
}

/// <summary>
/// Represents an instance of an item in the world or inventory.
/// </summary>
[System.Serializable]
public class InventoryItem
{
    public ItemData itemData;
    public int quantity = 1;
    public int level = 1; // For scaling items

    public InventoryItem(ItemData data, int qty = 1)
    {
        itemData = data;
        quantity = qty;
    }

    public float GetTotalValue()
    {
        return itemData.sellPrice * quantity;
    }
}
