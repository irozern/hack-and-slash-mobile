# Phases 2-5: Complete Game Implementation

## Phase 2: Combat Effects & Loot System

### Components Implemented

#### 1. **DamageNumber.cs** (`Combat/DamageNumber.cs`)
Floating damage text that appears above enemies when hit.

**Features:**
- White text for normal damage
- Yellow text for critical damage
- Rises up and fades out over 1.5 seconds
- Scales down as it fades

**Usage:**
```csharp
GameObject damageObj = Instantiate(damageNumberPrefab, worldPos, Quaternion.identity);
DamageNumber damageNum = damageObj.GetComponent<DamageNumber>();
damageNum.Initialize(worldPos, damage, isCritical);
```

#### 2. **HitDetection.cs** (`Combat/HitDetection.cs`)
Handles hit effects: white flash, knockback, and damage numbers.

**Features:**
- Hit flash effect (0.1 second white flash)
- Knockback physics
- Damage number display
- Material color restoration

**Usage:**
```csharp
hitDetection.PlayHitFlash();
hitDetection.ApplyKnockback(direction, force);
hitDetection.ShowDamageNumber(damage, isCritical);
```

#### 3. **ItemData.cs** (`Loot/ItemData.cs`)
Defines item data structure and rarity system.

**Item Properties:**
- Name, description, type
- Rarity (Common/Magic/Rare/Legendary)
- Stat bonuses (HP, Damage, Armor, Speed)
- Pricing (sell/buy)
- Icon and color

**Item Types:**
- Weapon (damage bonus)
- Armor (HP + armor bonus)
- Ring (mixed bonuses)
- Consumable
- Material
- Quest

#### 4. **LootItem.cs** (`Loot/LootItem.cs`)
Represents a loot item dropped in the world.

**Features:**
- Fountain physics on drop
- Auto-despawn after 60 seconds
- Fade out in last 5 seconds
- Pickup detection

**Usage:**
```csharp
lootItem.Initialize(itemData, dropPosition, dropForce);
if (lootItem.TryPickup())
{
    // Add to inventory
}
```

#### 5. **ItemDatabase.cs** (`Loot/ItemDatabase.cs`)
Centralized database for all game items.

**Features:**
- Load items from Resources/Data/Items
- Fast lookup by ID
- Filter by rarity or type
- Weighted random item generation

**Usage:**
```csharp
ItemData item = ItemDatabase.Instance.GetItemById(1);
ItemData random = ItemDatabase.Instance.GetRandomItem();
List<ItemData> rareItems = ItemDatabase.Instance.GetItemsByRarity(ItemRarity.Rare);
```

#### 6. **LootManager.cs** (`Loot/LootManager.cs`)
Manages loot generation and collection.

**Features:**
- Generates loot on enemy death
- Fountain physics (random direction/angle)
- Item scaling by enemy level
- Auto-pickup when player nearby
- Despawn tracking

**Usage:**
```csharp
LootManager.Instance.GenerateLoot(enemyPosition, enemyLevel);
```

---

## Phase 3: Inventory & Equipment System

### Components Implemented

#### 1. **InventoryManager.cs** (`Inventory/InventoryManager.cs`)
Grid-based inventory with equipment slots.

**Features:**
- 5x5 grid (25 slots)
- Item stacking for consumables
- Equipment slots (Weapon, Armor, Ring)
- Stat bonus calculation
- Persistence via PlayerPrefs

**Usage:**
```csharp
InventoryManager.Instance.AddItem(itemData);
InventoryManager.Instance.EquipItem(itemData);
EquipmentStats stats = InventoryManager.Instance.GetEquipmentStats();
```

**Equipment Bonuses:**
- Weapon: +Damage, +Attack Speed
- Armor: +HP, +Armor
- Ring: +HP, +Damage, +Speed

#### 2. **HUDManager.cs** (`UI/HUDManager.cs`)
Main HUD display for player stats and actions.

**UI Elements:**
- Health bar (red) with text
- Mana bar (blue) with text
- Experience bar with level
- Action bar with cooldown overlays
- Stat display (Damage, Armor, Speed)

**Usage:**
```csharp
HUDManager.Instance.ShowNotification("Item equipped!");
HUDManager.Instance.ShowDamagePopup(worldPos, damage, isCritical);
```

---

## Phase 4: Monetization Systems

### Components Implemented

#### 1. **GamePassManager.cs** (`Monetization/GamePassManager.cs`)
Subscription-based Game Pass system.

**Features:**
- 30-day subscription
- 1.5x XP multiplier
- 1.2x currency multiplier
- Auto-expiration tracking
- Persistent storage

**Usage:**
```csharp
GamePassManager.Instance.PurchaseGamePass();
float xpMult = GamePassManager.Instance.GetXPMultiplier();
bool hasPass = GamePassManager.Instance.HasActivePass();
```

#### 2. **PremiumChestManager.cs** (`Monetization/PremiumChestManager.cs`)
Premium chest rewards system.

**Chest Types:**
- **Common**: 500 gold, 50 XP
- **Rare**: 1500 gold, 200 XP, 1 item
- **Legendary**: 5000 gold, 500 XP, 2 items, 100 premium currency

**Usage:**
```csharp
PremiumChestManager.Instance.AddChest(ChestType.Rare);
ChestReward reward = PremiumChestManager.Instance.OpenChest(ChestType.Rare);
```

---

## Phase 5: Quest System

### Components Implemented

#### 1. **QuestManager.cs** (`Quests/QuestManager.cs`)
Quest tracking and reward system.

**Quest Types:**
- KillEnemies: Defeat X enemies
- CollectItems: Gather X items
- ReachLevel: Reach level X
- DefeatBoss: Defeat specific boss
- Survive: Survive X seconds

**Features:**
- Daily/weekly quest generation
- Progress tracking
- Reward application
- Persistence

**Usage:**
```csharp
QuestManager.Instance.StartQuest(quest);
QuestManager.Instance.CompleteQuest(quest);
List<Quest> active = QuestManager.Instance.GetActiveQuests();
```

**Reward System:**
- Gold
- Experience
- Items
- Premium currency

---

## Integration Guide

### Step 1: Setup ItemDatabase
1. Create folder: `Assets/Resources/Data/Items/`
2. Create ItemData scriptable objects for each item
3. Assign stats, rarity, and icon
4. ItemDatabase will auto-load on startup

### Step 2: Connect Combat System
```csharp
// In PlayerCombat.cs, after dealing damage:
hitDetection.PlayHitFlash();
hitDetection.ApplyKnockback(direction);
hitDetection.ShowDamageNumber(damage, isCritical);
```

### Step 3: Connect Loot System
```csharp
// In EnemyController.cs, on death:
LootManager.Instance.GenerateLoot(transform.position, enemyLevel);
```

### Step 4: Connect Inventory
```csharp
// In LootItem.cs, on pickup:
if (InventoryManager.Instance.AddItem(itemData))
{
    HUDManager.Instance.ShowNotification("Item acquired!");
}
```

### Step 5: Connect Monetization
```csharp
// In PlayerStats.cs, on experience gain:
float xpMultiplier = GamePassManager.Instance.GetXPMultiplier();
experience += (long)(amount * xpMultiplier);
```

---

## Data Persistence

### Saved Data
- **PlayerPrefs Keys:**
  - `GamePass_Active`: Boolean
  - `GamePass_Expiration`: Float (timestamp)
  - `PremiumCurrency`: Integer
  - `Chest_Common`, `Chest_Rare`, `Chest_Legendary`: Integers
  - Inventory items (TODO: JSON serialization)
  - Quest progress (TODO: JSON serialization)

### Future Enhancements
- Cloud save via Firebase
- Cross-device progression
- Leaderboards
- Social features

---

## Performance Optimization

### Loot System
- Object pooling for loot items
- Despawn after 60 seconds
- Limit active loot to 50 items max

### Inventory
- Grid-based lookup (O(1) access)
- Lazy stat calculation
- Cache equipment stats

### Quests
- Update only active quests
- Batch progress updates
- Lazy reward calculation

---

## Testing Checklist

### Phase 2 (Combat & Loot)
- [ ] Damage numbers appear and fade correctly
- [ ] Hit flash effect plays
- [ ] Knockback applies physics correctly
- [ ] Loot drops from enemies
- [ ] Loot fountain physics works
- [ ] Loot despawns after 60 seconds
- [ ] Player picks up loot automatically
- [ ] Item rarity colors display correctly

### Phase 3 (Inventory & Equipment)
- [ ] Items can be added to inventory
- [ ] Inventory grid displays correctly
- [ ] Items can be equipped
- [ ] Equipment bonuses apply to stats
- [ ] HUD updates on stat changes
- [ ] Inventory persists on reload

### Phase 4 (Monetization)
- [ ] Game Pass can be purchased
- [ ] XP multiplier applies correctly
- [ ] Game Pass expires after 30 days
- [ ] Premium chests can be opened
- [ ] Chest rewards apply correctly
- [ ] Premium currency displays

### Phase 5 (Quests)
- [ ] Daily quests generate
- [ ] Quest progress updates
- [ ] Quests complete when target reached
- [ ] Quest rewards apply
- [ ] Completed quests display

---

## Known Issues & TODOs

### Phase 2
- [ ] Implement damage number prefab pooling
- [ ] Add knockback to player combat
- [ ] Add loot fountain particle effects
- [ ] Implement item rarity visual effects

### Phase 3
- [ ] Implement inventory UI grid
- [ ] Add equipment preview
- [ ] Add item comparison UI
- [ ] Implement item sorting/filtering

### Phase 4
- [ ] Integrate Google Play Billing
- [ ] Integrate Apple IAP
- [ ] Add premium currency shop
- [ ] Add seasonal battle pass

### Phase 5
- [ ] Implement quest UI
- [ ] Add quest markers on map
- [ ] Implement boss encounters
- [ ] Add seasonal quest rotation

---

## Architecture Diagram (Phases 2-5)

```
GameManager
├── LootManager
│   ├── ItemDatabase
│   └── LootItem (pooled)
├── InventoryManager
│   ├── Equipment
│   └── EquipmentStats
├── GamePassManager
│   └── PremiumChestManager
├── QuestManager
│   └── Quest (active/completed)
└── HUDManager
    ├── HealthBar
    ├── ManaBar
    ├── ExperienceBar
    └── ActionBar
```

---

## References

- **Scriptable Objects**: https://docs.unity3d.com/Manual/class-ScriptableObject.html
- **PlayerPrefs**: https://docs.unity3d.com/ScriptReference/PlayerPrefs.html
- **UI System**: https://docs.unity3d.com/Manual/UISystem.html
- **JSON Serialization**: https://docs.unity3d.com/Manual/JSONSerialization.html
