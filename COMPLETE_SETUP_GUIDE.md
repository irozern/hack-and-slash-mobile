# Complete Setup Guide - Hack & Slash Game

## Project Overview

A professional isometric hack and slash game for Android/iOS built in Unity with 5 complete phases:
- **Phase 1**: Core gameplay (player, camera, input)
- **Phase 2**: Combat effects and loot system
- **Phase 3**: Inventory and equipment
- **Phase 4**: Monetization (Game Pass, chests)
- **Phase 5**: Quests and polish

## Prerequisites

- **Unity 2022 LTS** or newer
- **Android SDK** (for Android builds)
- **Xcode** (for iOS builds)
- **Git** (for version control)

## Project Structure

```
HackSlashGame/
├── Assets/
│   ├── Scripts/
│   │   ├── Core/              # GameManager, InputManager, CameraController
│   │   ├── Player/            # PlayerController, PlayerStats, PlayerCombat
│   │   ├── Enemy/             # EnemyController, EnemyAI, EnemyStats
│   │   ├── Combat/            # DamageNumber, HitDetection
│   │   ├── Loot/              # ItemData, LootItem, LootManager, ItemDatabase
│   │   ├── Inventory/         # InventoryManager
│   │   ├── Monetization/      # GamePassManager, PremiumChestManager
│   │   ├── Quests/            # QuestManager
│   │   ├── UI/                # HUDManager
│   │   └── Utils/             # Constants
│   ├── Prefabs/
│   │   ├── Player/
│   │   ├── Enemies/
│   │   ├── UI/
│   │   └── Loot/
│   ├── Scenes/
│   │   ├── MainMenu.unity
│   │   ├── GameScene.unity
│   │   └── LoadingScene.unity
│   ├── Materials/
│   ├── Textures/
│   ├── Audio/
│   └── Resources/
│       └── Data/
│           └── Items/         # ItemData scriptable objects
└── Documentation/
    ├── PHASE_1_DOCUMENTATION.md
    ├── PHASE_2_5_DOCUMENTATION.md
    ├── COMPLETE_SETUP_GUIDE.md
    └── ARCHITECTURE.md
```

## Phase-by-Phase Setup

### Phase 1: Core Gameplay

#### Scene Setup
1. Create new scene: `Assets/Scenes/GameScene.unity`
2. Add ground plane (Quad, scale 50x50)
3. Bake NavMesh:
   - Window → AI → Navigation
   - Select ground plane
   - Mark as "Walkable"
   - Click "Bake"

#### Player Setup
1. Create Player GameObject:
   ```
   Player (GameObject)
   ├── Capsule (Model, 0.5 scale)
   ├── CharacterController (Component)
   ├── PlayerController (Script)
   ├── PlayerStats (Script)
   ├── PlayerCombat (Script)
   ├── Animator (Component)
   └── HitDetection (Script)
   ```

2. Configure CharacterController:
   - Height: 2
   - Radius: 0.5
   - Center: (0, 1, 0)

3. Create Player Prefab:
   - Drag Player into `Assets/Prefabs/Player/PlayerPrefab.prefab`

#### Enemy Setup
1. Create Enemy GameObject:
   ```
   Enemy (GameObject)
   ├── Capsule (Model, 0.3 scale)
   ├── NavMeshAgent (Component)
   ├── EnemyStats (Script)
   ├── EnemyAI (Script)
   ├── EnemyController (Script)
   ├── Animator (Component)
   └── HitDetection (Script)
   ```

2. Configure NavMeshAgent:
   - Speed: 3.5
   - Stopping Distance: 0.5
   - Auto Braking: True

3. Create Enemy Prefab:
   - Drag Enemy into `Assets/Prefabs/Enemies/EnemyPrefab.prefab`

#### Camera Setup
1. Create CameraController GameObject:
   - Add CameraController script
   - Assign Main Camera reference
   - Position: (0, 8, -10)

#### UI Setup (Mobile Controls)
1. Create Canvas:
   - Render Mode: Screen Space - Overlay
   - Scale Mode: Scale with Screen Size

2. Create Joystick UI:
   - Panel (bottom-left, 200x200)
   - Add Image (joystick background)
   - Add Image (joystick handle)
   - Assign to InputManager

3. Create Attack Button:
   - Button (bottom-right, 100x100)
   - Add Image (red color)
   - Add Text ("ATTACK")
   - Assign to InputManager

4. Create Dash Button:
   - Button (bottom-right, 100x100, above attack)
   - Add Image (blue color)
   - Add Text ("DASH")
   - Assign to InputManager

#### GameManager Setup
1. Create GameManager GameObject:
   - Add GameManager script
   - Assign Player Prefab
   - Assign Enemy Prefab
   - Set initial enemy count: 5
   - Set spawn radius: 20

#### Testing Phase 1
- Play scene
- Test joystick movement
- Test attack button
- Test dash button
- Verify camera follows player
- Verify enemies spawn and patrol
- Verify enemies chase player
- Verify enemies attack

---

### Phase 2: Combat & Loot

#### Damage Numbers Setup
1. Create DamageNumber Prefab:
   - Canvas (World Space)
   - TextMeshPro (white text)
   - CanvasGroup (for fade)
   - Add DamageNumber script

2. Assign to PlayerCombat:
   - Drag prefab into scene
   - Reference in PlayerCombat script

#### Hit Detection Setup
1. Add HitDetection to Player and Enemies:
   - Assign Renderer components
   - Assign CharacterController
   - Script handles flash and knockback

#### Item Database Setup
1. Create Items folder:
   - `Assets/Resources/Data/Items/`

2. Create sample items (ScriptableObjects):
   - Common Sword: +5 Damage
   - Iron Armor: +20 HP, +5 Armor
   - Gold Ring: +10 HP, +2 Damage

3. Create ItemDatabase GameObject:
   - Add ItemDatabase script
   - Will auto-load items on startup

#### Loot System Setup
1. Create LootItem Prefab:
   - Cube (visual)
   - TextMeshPro (item name)
   - Rigidbody (for physics)
   - Add LootItem script

2. Create LootManager GameObject:
   - Add LootManager script
   - Assign LootItem prefab
   - Set despawn time: 60 seconds

3. Connect to Enemy Death:
   - In EnemyController.HandleDeath():
   ```csharp
   LootManager.Instance.GenerateLoot(transform.position, level);
   ```

#### Testing Phase 2
- Enemies drop loot on death
- Loot items have correct rarity colors
- Loot despawns after 60 seconds
- Damage numbers appear and fade
- Hit flash effect plays
- Knockback applies to enemies

---

### Phase 3: Inventory & Equipment

#### Inventory UI Setup
1. Create Inventory Panel:
   - Canvas Panel (center screen)
   - Grid Layout Group (5x5)
   - Create 25 inventory slots
   - Each slot: Image + Button

2. Create Equipment Panel:
   - Show 3 equipment slots (Weapon, Armor, Ring)
   - Display stat bonuses

3. Create Stats Panel:
   - Show current stats
   - Show equipment bonuses
   - Show total stats

#### HUD Setup
1. Create HUD Canvas:
   - Health Bar (red, bottom-left)
   - Mana Bar (blue, bottom-left)
   - Experience Bar (yellow, bottom)
   - Level Text
   - Action Bar (skill icons with cooldowns)

2. Add HUDManager:
   - Assign all UI elements
   - Subscribe to PlayerStats events

3. Connect to PlayerStats:
   - PlayerStats.OnHealthChanged → UpdateHealthBar()
   - PlayerStats.OnManaChanged → UpdateManaBar()
   - PlayerStats.OnLevelUp → UpdateLevelDisplay()

#### Inventory Manager Setup
1. Create InventoryManager GameObject:
   - Add InventoryManager script
   - Set grid size: 5x5

2. Connect to LootManager:
   - In LootManager.CheckPickups():
   ```csharp
   InventoryManager.Instance.AddItem(lootItem.GetItemData());
   ```

#### Testing Phase 3
- Items can be added to inventory
- Inventory displays correctly
- Items can be equipped
- Equipment bonuses apply to stats
- HUD updates on changes
- Stats display correctly

---

### Phase 4: Monetization

#### Game Pass Setup
1. Create GamePassManager GameObject:
   - Add GamePassManager script
   - Set pass duration: 30 days
   - Set XP multiplier: 1.5x

2. Create Game Pass UI:
   - Show pass status
   - Show time remaining
   - Show purchase button

3. Connect to PlayerStats:
   - In PlayerStats.GainExperience():
   ```csharp
   float multiplier = GamePassManager.Instance.GetXPMultiplier();
   experience += (long)(amount * multiplier);
   ```

#### Premium Chest Setup
1. Create PremiumChestManager GameObject:
   - Add PremiumChestManager script
   - Configure chest rewards

2. Create Chest UI:
   - Show chest inventory
   - Show open button
   - Display rewards

3. Connect to InventoryManager:
   - Chests add items on open
   - Apply gold/XP rewards

#### IAP Integration (TODO)
1. Setup Google Play Billing:
   - Configure in Google Play Console
   - Add SKUs for Game Pass and chests

2. Setup Apple IAP:
   - Configure in App Store Connect
   - Add product IDs

#### Testing Phase 4
- Game Pass can be purchased (mock)
- XP multiplier applies
- Game Pass expires after 30 days
- Premium chests can be opened
- Chest rewards apply correctly
- Premium currency displays

---

### Phase 5: Quests & Polish

#### Quest System Setup
1. Create QuestManager GameObject:
   - Add QuestManager script
   - Generate sample quests

2. Create Quest UI:
   - Show active quests
   - Show quest progress
   - Show completed quests

3. Connect to Game Events:
   - Enemy death → KillEnemies quest progress
   - Item pickup → CollectItems quest progress
   - Level up → ReachLevel quest progress

#### Polish & Effects
1. Add Particle Effects:
   - Enemy death explosion
   - Loot pickup effect
   - Level up effect
   - Critical hit effect

2. Add Sound Effects:
   - Attack sound
   - Hit sound
   - Loot pickup sound
   - Level up sound

3. Add Animations:
   - Player attack animation
   - Enemy attack animation
   - Player death animation
   - Enemy death animation

#### Testing Phase 5
- Daily quests generate
- Quest progress updates
- Quests complete correctly
- Quest rewards apply
- Particle effects display
- Sound effects play
- Animations play smoothly

---

## Build Configuration

### Android Build
1. File → Build Settings
2. Select Android platform
3. Player Settings:
   - Company Name: Your Company
   - Product Name: Hack & Slash
   - Package Name: com.company.hackslash
   - Minimum API Level: 24
   - Target API Level: 33

4. Build and Run

### iOS Build
1. File → Build Settings
2. Select iOS platform
3. Player Settings:
   - Company Name: Your Company
   - Product Name: Hack & Slash
   - Bundle ID: com.company.hackslash
   - Minimum iOS Version: 14.0

4. Build and open in Xcode
5. Configure signing and build

---

## Performance Optimization

### Memory
- Use object pooling for enemies and loot
- Limit active enemies to 50
- Limit active loot to 50 items
- Unload unused assets

### CPU
- Use NavMesh for pathfinding (not raycasts)
- Batch UI updates
- Cache component references
- Use coroutines for delayed actions

### GPU
- Use simple materials (no complex shaders)
- Batch rendering with atlases
- Limit particle effects
- Use LOD for distant objects

---

## Debugging Tips

### Common Issues

**Enemies not spawning:**
- Check NavMesh is baked
- Check spawn radius is valid
- Check enemy prefab is assigned

**Player not moving:**
- Check InputManager is in scene
- Check joystick UI is assigned
- Check CharacterController is enabled

**Loot not dropping:**
- Check ItemDatabase is initialized
- Check LootManager is in scene
- Check LootItem prefab is assigned

**UI not updating:**
- Check HUDManager is in scene
- Check PlayerStats events are subscribed
- Check UI elements are assigned

### Debug Commands
```csharp
// Add to GameManager for testing
if (Input.GetKeyDown(KeyCode.K))
{
    // Kill all enemies
    foreach (var enemy in GameManager.Instance.GetActiveEnemies())
    {
        enemy.TakeDamage(9999);
    }
}

if (Input.GetKeyDown(KeyCode.L))
{
    // Level up player
    PlayerStats.GainExperience(999999);
}

if (Input.GetKeyDown(KeyCode.G))
{
    // Add gold
    PlayerStats.AddGold(1000);
}
```

---

## Next Steps

1. **Implement Animations**
   - Create animator controllers
   - Add blend trees for movement
   - Add attack animations

2. **Add Sound**
   - Import audio clips
   - Create AudioManager
   - Add sound effects and music

3. **Polish UI**
   - Create main menu
   - Add pause menu
   - Add settings menu

4. **Add Content**
   - Create more enemy types
   - Create more items
   - Create boss encounters

5. **Optimize & Test**
   - Profile on target devices
   - Fix performance issues
   - Test on real hardware

---

## Resources

- **Unity Documentation**: https://docs.unity3d.com/
- **NavMesh**: https://docs.unity3d.com/Manual/nav-NavigationSystem.html
- **UI System**: https://docs.unity3d.com/Manual/UISystem.html
- **Mobile Development**: https://docs.unity3d.com/Manual/MobileOptimization.html

---

**Last Updated**: May 23, 2026  
**Version**: 1.0.0 (All Phases Complete)
