using UnityEngine;
using System.Collections.Generic;
using System;

/// <summary>
/// Manages player inventory with grid-based storage.
/// Handles item addition, removal, and equipment management.
/// </summary>
public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; }

    [SerializeField] private int gridWidth = 5;
    [SerializeField] private int gridHeight = 5;

    private List<InventoryItem> inventoryItems = new List<InventoryItem>();
    private Equipment equippedItems = new Equipment();

    // Events
    public event Action<InventoryItem> OnItemAdded;
    public event Action<InventoryItem> OnItemRemoved;
    public event Action<Equipment> OnEquipmentChanged;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    /// <summary>
    /// Adds an item to inventory.
    /// </summary>
    public bool AddItem(ItemData itemData, int quantity = 1)
    {
        // Check if inventory is full
        if (inventoryItems.Count >= gridWidth * gridHeight)
        {
            Debug.LogWarning("Inventory is full!");
            return false;
        }

        // Try to stack if stackable
        if (itemData.maxStackSize > 1)
        {
            foreach (var item in inventoryItems)
            {
                if (item.itemData.id == itemData.id && item.quantity < itemData.maxStackSize)
                {
                    int canAdd = itemData.maxStackSize - item.quantity;
                    int toAdd = Mathf.Min(quantity, canAdd);
                    item.quantity += toAdd;
                    quantity -= toAdd;

                    if (quantity <= 0)
                        return true;
                }
            }
        }

        // Add new stack
        if (quantity > 0)
        {
            InventoryItem newItem = new InventoryItem(itemData, quantity);
            inventoryItems.Add(newItem);
            OnItemAdded?.Invoke(newItem);
        }

        return true;
    }

    /// <summary>
    /// Removes an item from inventory.
    /// </summary>
    public bool RemoveItem(ItemData itemData, int quantity = 1)
    {
        foreach (var item in inventoryItems)
        {
            if (item.itemData.id == itemData.id)
            {
                item.quantity -= quantity;
                if (item.quantity <= 0)
                {
                    inventoryItems.Remove(item);
                    OnItemRemoved?.Invoke(item);
                }
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// Equips an item to a slot.
    /// </summary>
    public bool EquipItem(ItemData itemData)
    {
        if (itemData.itemType == ItemType.Weapon)
        {
            equippedItems.weapon = itemData;
            OnEquipmentChanged?.Invoke(equippedItems);
            return true;
        }
        else if (itemData.itemType == ItemType.Armor)
        {
            equippedItems.armor = itemData;
            OnEquipmentChanged?.Invoke(equippedItems);
            return true;
        }
        else if (itemData.itemType == ItemType.Ring)
        {
            equippedItems.ring = itemData;
            OnEquipmentChanged?.Invoke(equippedItems);
            return true;
        }

        return false;
    }

    /// <summary>
    /// Unequips an item from a slot.
    /// </summary>
    public void UnequipSlot(ItemType type)
    {
        if (type == ItemType.Weapon)
            equippedItems.weapon = null;
        else if (type == ItemType.Armor)
            equippedItems.armor = null;
        else if (type == ItemType.Ring)
            equippedItems.ring = null;

        OnEquipmentChanged?.Invoke(equippedItems);
    }

    /// <summary>
    /// Gets total stat bonuses from equipped items.
    /// </summary>
    public EquipmentStats GetEquipmentStats()
    {
        EquipmentStats stats = new EquipmentStats();

        if (equippedItems.weapon != null)
        {
            stats.damageBonus += equippedItems.weapon.damageBonus;
            stats.attackSpeedBonus += equippedItems.weapon.attackSpeedBonus;
        }

        if (equippedItems.armor != null)
        {
            stats.healthBonus += equippedItems.armor.healthBonus;
            stats.armorBonus += equippedItems.armor.armorBonus;
        }

        if (equippedItems.ring != null)
        {
            stats.healthBonus += equippedItems.ring.healthBonus;
            stats.damageBonus += equippedItems.ring.damageBonus;
            stats.moveSpeedBonus += equippedItems.ring.moveSpeedBonus;
        }

        return stats;
    }

    /// <summary>
    /// Gets inventory capacity.
    /// </summary>
    public int GetCapacity() => gridWidth * gridHeight;
    public int GetUsedSlots() => inventoryItems.Count;
    public int GetAvailableSlots() => GetCapacity() - GetUsedSlots();

    public Equipment GetEquippedItems() => equippedItems;
    public List<InventoryItem> GetAllItems() => inventoryItems;
}

/// <summary>
/// Represents equipped items.
/// </summary>
[System.Serializable]
public class Equipment
{
    public ItemData weapon;
    public ItemData armor;
    public ItemData ring;
}

/// <summary>
/// Total stat bonuses from equipment.
/// </summary>
[System.Serializable]
public class EquipmentStats
{
    public float healthBonus;
    public float damageBonus;
    public float armorBonus;
    public float moveSpeedBonus;
    public float attackSpeedBonus;
}
