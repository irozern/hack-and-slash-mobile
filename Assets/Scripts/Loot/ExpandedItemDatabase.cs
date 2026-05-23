using UnityEngine;
using System.Collections.Generic;

namespace HackSlash.Loot
{
    /// <summary>
    /// Expanded item database with 50+ items across multiple categories.
    /// Includes weapons, armor, accessories, and cosmetics.
    /// </summary>
    public class ExpandedItemDatabase : MonoBehaviour
    {
        public static ExpandedItemDatabase Instance { get; private set; }

        [System.Serializable]
        public class Item
        {
            public int itemId;
            public string itemName;
            public string description;
            public ItemRarity rarity;
            public ItemType itemType;
            public int requiredLevel;
            public int sellPrice;
            public ItemStats stats;
            public string iconPath;
        }

        [System.Serializable]
        public class ItemStats
        {
            public int damageBonus;
            public int armorBonus;
            public int hpBonus;
            public int speedBonus;
            public int critChanceBonus;
            public int critDamageBonus;
        }

        public enum ItemRarity
        {
            Common,
            Magic,
            Rare,
            Legendary,
            Mythic
        }

        public enum ItemType
        {
            Weapon,
            Armor,
            Ring,
            Amulet,
            Potion,
            Cosmetic
        }

        private Dictionary<int, Item> items = new();

        void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeItems();
        }

        /// <summary>
        /// Initialize all items.
        /// </summary>
        private void InitializeItems()
        {
            // WEAPONS (10)
            
            // Tier 1
            items[1] = CreateWeapon(1, "Iron Sword", "A basic iron sword", ItemRarity.Common, 1, 100, 5, 0, 0, 0, 0, 0);
            items[2] = CreateWeapon(2, "Steel Sword", "A sturdy steel sword", ItemRarity.Magic, 5, 250, 10, 0, 0, 0, 0, 0);
            items[3] = CreateWeapon(3, "Excalibur", "A legendary blade", ItemRarity.Rare, 10, 500, 15, 0, 0, 2, 0, 0);
            
            // Tier 2
            items[4] = CreateWeapon(4, "Dragon Slayer", "Forged to slay dragons", ItemRarity.Rare, 20, 1000, 25, 0, 0, 3, 0, 0);
            items[5] = CreateWeapon(5, "Void Blade", "A blade from the void", ItemRarity.Legendary, 30, 2000, 35, 0, 0, 5, 0, 0);
            
            // Tier 3
            items[6] = CreateWeapon(6, "Godslayer", "The ultimate weapon", ItemRarity.Legendary, 40, 3000, 45, 0, 0, 8, 0, 0);
            items[7] = CreateWeapon(7, "Infinity Blade", "Infinite power", ItemRarity.Mythic, 50, 5000, 60, 0, 0, 10, 0, 0);
            
            // Special Weapons
            items[8] = CreateWeapon(8, "Flame Sword", "Burns enemies", ItemRarity.Rare, 15, 750, 20, 0, 0, 2, 0, 0);
            items[9] = CreateWeapon(9, "Frost Blade", "Freezes enemies", ItemRarity.Rare, 15, 750, 18, 0, 0, 2, 0, 0);
            items[10] = CreateWeapon(10, "Lightning Sword", "Shocks enemies", ItemRarity.Rare, 15, 750, 22, 0, 0, 3, 0, 0);

            // ARMOR (10)
            
            // Tier 1
            items[11] = CreateArmor(11, "Leather Armor", "Light leather protection", ItemRarity.Common, 1, 100, 0, 5, 0, 0, 0, 0);
            items[12] = CreateArmor(12, "Iron Armor", "Heavy iron protection", ItemRarity.Magic, 5, 250, 0, 10, 0, 0, 0, 0);
            items[13] = CreateArmor(13, "Steel Armor", "Strong steel protection", ItemRarity.Rare, 10, 500, 0, 15, 0, 0, 0, 0);
            
            // Tier 2
            items[14] = CreateArmor(14, "Dragon Scale Armor", "Dragon scale protection", ItemRarity.Rare, 20, 1000, 0, 25, 0, 0, 0, 0);
            items[15] = CreateArmor(15, "Void Armor", "Armor from the void", ItemRarity.Legendary, 30, 2000, 0, 35, 0, 0, 0, 0);
            
            // Tier 3
            items[16] = CreateArmor(16, "God Armor", "Divine protection", ItemRarity.Legendary, 40, 3000, 0, 45, 0, 0, 0, 0);
            items[17] = CreateArmor(17, "Infinity Armor", "Infinite defense", ItemRarity.Mythic, 50, 5000, 0, 60, 0, 0, 0, 0);
            
            // Special Armor
            items[18] = CreateArmor(18, "Flame Armor", "Fire resistant", ItemRarity.Rare, 15, 750, 0, 18, 0, 0, 0, 0);
            items[19] = CreateArmor(19, "Frost Armor", "Cold resistant", ItemRarity.Rare, 15, 750, 0, 18, 0, 0, 0, 0);
            items[20] = CreateArmor(20, "Lightning Armor", "Electric resistant", ItemRarity.Rare, 15, 750, 0, 18, 0, 0, 0, 0);

            // RINGS (10)
            
            items[21] = CreateRing(21, "Ring of Strength", "Increases damage", ItemRarity.Common, 1, 100, 3, 0, 0, 0, 0, 0);
            items[22] = CreateRing(22, "Ring of Defense", "Increases armor", ItemRarity.Common, 1, 100, 0, 3, 0, 0, 0, 0);
            items[23] = CreateRing(23, "Ring of Vitality", "Increases HP", ItemRarity.Magic, 5, 250, 0, 0, 10, 0, 0, 0);
            items[24] = CreateRing(24, "Ring of Speed", "Increases speed", ItemRarity.Magic, 5, 250, 0, 0, 0, 2, 0, 0);
            items[25] = CreateRing(25, "Ring of Crit", "Increases crit chance", ItemRarity.Rare, 10, 500, 0, 0, 0, 0, 5, 0);
            items[26] = CreateRing(26, "Ring of Fury", "Increases crit damage", ItemRarity.Rare, 10, 500, 0, 0, 0, 0, 0, 10);
            items[27] = CreateRing(27, "Ring of Power", "Massive damage boost", ItemRarity.Legendary, 20, 1000, 10, 0, 0, 0, 5, 10);
            items[28] = CreateRing(28, "Ring of Immortality", "Massive defense", ItemRarity.Legendary, 20, 1000, 0, 15, 20, 0, 0, 0);
            items[29] = CreateRing(29, "Ring of Infinity", "Ultimate ring", ItemRarity.Mythic, 40, 3000, 15, 15, 30, 3, 10, 20);
            items[30] = CreateRing(30, "Ring of Destiny", "Fate's ring", ItemRarity.Mythic, 40, 3000, 10, 10, 20, 2, 8, 15);

            // AMULETS (10)
            
            items[31] = CreateAmulet(31, "Amulet of Health", "Restores health", ItemRarity.Common, 1, 100, 0, 0, 5, 0, 0, 0);
            items[32] = CreateAmulet(32, "Amulet of Mana", "Restores mana", ItemRarity.Magic, 5, 250, 0, 0, 0, 0, 0, 0);
            items[33] = CreateAmulet(33, "Amulet of Wisdom", "Increases all stats", ItemRarity.Rare, 10, 500, 2, 2, 5, 1, 1, 2);
            items[34] = CreateAmulet(34, "Amulet of Protection", "Reduces damage taken", ItemRarity.Rare, 10, 500, 0, 5, 0, 0, 0, 0);
            items[35] = CreateAmulet(35, "Amulet of Swiftness", "Increases speed", ItemRarity.Magic, 5, 250, 0, 0, 0, 3, 0, 0);
            items[36] = CreateAmulet(36, "Amulet of Fortune", "Increases loot", ItemRarity.Rare, 15, 750, 0, 0, 0, 0, 0, 0);
            items[37] = CreateAmulet(37, "Amulet of Power", "Massive boost", ItemRarity.Legendary, 25, 1500, 5, 5, 10, 1, 3, 5);
            items[38] = CreateAmulet(38, "Amulet of Eternity", "Eternal power", ItemRarity.Legendary, 35, 2500, 10, 10, 20, 2, 5, 10);
            items[39] = CreateAmulet(39, "Amulet of Infinity", "Infinite power", ItemRarity.Mythic, 50, 5000, 15, 15, 30, 3, 8, 15);
            items[40] = CreateAmulet(40, "Amulet of Destiny", "Destiny's favor", ItemRarity.Mythic, 50, 5000, 12, 12, 25, 2, 6, 12);

            // POTIONS (5)
            
            items[41] = CreatePotion(41, "Health Potion", "Restores 50 HP", ItemRarity.Common, 1, 50, 0, 0, 0, 0, 0, 0);
            items[42] = CreatePotion(42, "Greater Health Potion", "Restores 100 HP", ItemRarity.Magic, 5, 100, 0, 0, 0, 0, 0, 0);
            items[43] = CreatePotion(43, "Mana Potion", "Restores 50 Mana", ItemRarity.Common, 1, 50, 0, 0, 0, 0, 0, 0);
            items[44] = CreatePotion(44, "Greater Mana Potion", "Restores 100 Mana", ItemRarity.Magic, 5, 100, 0, 0, 0, 0, 0, 0);
            items[45] = CreatePotion(45, "Elixir of Power", "Massive boost", ItemRarity.Rare, 10, 500, 5, 5, 20, 1, 2, 5);

            // COSMETICS (5)
            
            items[46] = CreateCosmetic(46, "Red Cloak", "A red cloak", ItemRarity.Common, 1, 100, 0, 0, 0, 0, 0, 0);
            items[47] = CreateCosmetic(47, "Blue Cloak", "A blue cloak", ItemRarity.Common, 1, 100, 0, 0, 0, 0, 0, 0);
            items[48] = CreateCosmetic(48, "Golden Cloak", "A golden cloak", ItemRarity.Rare, 10, 500, 0, 0, 0, 0, 0, 0);
            items[49] = CreateCosmetic(49, "Shadow Cloak", "A shadow cloak", ItemRarity.Legendary, 20, 1000, 0, 0, 0, 0, 0, 0);
            items[50] = CreateCosmetic(50, "Infinity Cloak", "An infinite cloak", ItemRarity.Mythic, 40, 3000, 0, 0, 0, 0, 0, 0);

            Debug.Log("Item database initialized with 50 items");
        }

        /// <summary>
        /// Create a weapon item.
        /// </summary>
        private Item CreateWeapon(int id, string name, string desc, ItemRarity rarity, int level, int price, int dmg, int armor, int hp, int speed, int crit, int critDmg)
        {
            return new Item
            {
                itemId = id,
                itemName = name,
                description = desc,
                rarity = rarity,
                itemType = ItemType.Weapon,
                requiredLevel = level,
                sellPrice = price,
                stats = new ItemStats { damageBonus = dmg, armorBonus = armor, hpBonus = hp, speedBonus = speed, critChanceBonus = crit, critDamageBonus = critDmg }
            };
        }

        /// <summary>
        /// Create an armor item.
        /// </summary>
        private Item CreateArmor(int id, string name, string desc, ItemRarity rarity, int level, int price, int dmg, int armor, int hp, int speed, int crit, int critDmg)
        {
            return new Item
            {
                itemId = id,
                itemName = name,
                description = desc,
                rarity = rarity,
                itemType = ItemType.Armor,
                requiredLevel = level,
                sellPrice = price,
                stats = new ItemStats { damageBonus = dmg, armorBonus = armor, hpBonus = hp, speedBonus = speed, critChanceBonus = crit, critDamageBonus = critDmg }
            };
        }

        /// <summary>
        /// Create a ring item.
        /// </summary>
        private Item CreateRing(int id, string name, string desc, ItemRarity rarity, int level, int price, int dmg, int armor, int hp, int speed, int crit, int critDmg)
        {
            return new Item
            {
                itemId = id,
                itemName = name,
                description = desc,
                rarity = rarity,
                itemType = ItemType.Ring,
                requiredLevel = level,
                sellPrice = price,
                stats = new ItemStats { damageBonus = dmg, armorBonus = armor, hpBonus = hp, speedBonus = speed, critChanceBonus = crit, critDamageBonus = critDmg }
            };
        }

        /// <summary>
        /// Create an amulet item.
        /// </summary>
        private Item CreateAmulet(int id, string name, string desc, ItemRarity rarity, int level, int price, int dmg, int armor, int hp, int speed, int crit, int critDmg)
        {
            return new Item
            {
                itemId = id,
                itemName = name,
                description = desc,
                rarity = rarity,
                itemType = ItemType.Amulet,
                requiredLevel = level,
                sellPrice = price,
                stats = new ItemStats { damageBonus = dmg, armorBonus = armor, hpBonus = hp, speedBonus = speed, critChanceBonus = crit, critDamageBonus = critDmg }
            };
        }

        /// <summary>
        /// Create a potion item.
        /// </summary>
        private Item CreatePotion(int id, string name, string desc, ItemRarity rarity, int level, int price, int dmg, int armor, int hp, int speed, int crit, int critDmg)
        {
            return new Item
            {
                itemId = id,
                itemName = name,
                description = desc,
                rarity = rarity,
                itemType = ItemType.Potion,
                requiredLevel = level,
                sellPrice = price,
                stats = new ItemStats { damageBonus = dmg, armorBonus = armor, hpBonus = hp, speedBonus = speed, critChanceBonus = crit, critDamageBonus = critDmg }
            };
        }

        /// <summary>
        /// Create a cosmetic item.
        /// </summary>
        private Item CreateCosmetic(int id, string name, string desc, ItemRarity rarity, int level, int price, int dmg, int armor, int hp, int speed, int crit, int critDmg)
        {
            return new Item
            {
                itemId = id,
                itemName = name,
                description = desc,
                rarity = rarity,
                itemType = ItemType.Cosmetic,
                requiredLevel = level,
                sellPrice = price,
                stats = new ItemStats { damageBonus = dmg, armorBonus = armor, hpBonus = hp, speedBonus = speed, critChanceBonus = crit, critDamageBonus = critDmg }
            };
        }

        /// <summary>
        /// Get item by ID.
        /// </summary>
        public Item GetItem(int itemId)
        {
            if (items.ContainsKey(itemId))
                return items[itemId];
            return null;
        }

        /// <summary>
        /// Get all items.
        /// </summary>
        public List<Item> GetAllItems()
        {
            return new List<Item>(items.Values);
        }

        /// <summary>
        /// Get items by type.
        /// </summary>
        public List<Item> GetItemsByType(ItemType type)
        {
            List<Item> result = new();
            foreach (var item in items.Values)
            {
                if (item.itemType == type)
                    result.Add(item);
            }
            return result;
        }

        /// <summary>
        /// Get items by rarity.
        /// </summary>
        public List<Item> GetItemsByRarity(ItemRarity rarity)
        {
            List<Item> result = new();
            foreach (var item in items.Values)
            {
                if (item.rarity == rarity)
                    result.Add(item);
            }
            return result;
        }

        /// <summary>
        /// Get random item.
        /// </summary>
        public Item GetRandomItem()
        {
            List<Item> allItems = GetAllItems();
            if (allItems.Count == 0)
                return null;
            return allItems[Random.Range(0, allItems.Count)];
        }
    }
}
