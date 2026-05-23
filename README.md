# Hack & Slash - Isometric Action RPG

A professional-grade isometric hack and slash game for Android and iOS, built in Unity with advanced combat systems, monetization, and progression mechanics.

## Project Overview

**Genre**: Isometric Action RPG / Hack & Slash  
**Platforms**: Android (API 24+), iOS (14+)  
**Engine**: Unity 2022 LTS+  
**Target Audience**: Mobile gamers aged 13+

## Core Features

### Gameplay
- **Isometric Camera**: Fixed 45° isometric perspective with smooth following
- **Mobile Controls**: Virtual joystick + attack/dash buttons
- **Combat System**: Melee attacks with cone-based AOE detection, crit chance, armor
- **Enemy AI**: Finite State Machine with Idle, Patrol, Chase, Attack, Death states
- **Progression**: Experience, leveling, stat scaling
- **Loot System**: Item drops with rarity colors and stat bonuses

### Monetization
- **Game Pass**: 30-day subscription with XP boost
- **Premium Chests**: Rare and legendary reward chests
- **Battle Pass**: Seasonal progression with quests and rewards
- **XP Boosters**: Temporary experience multipliers

### Systems
- **Inventory**: Grid-based item management
- **Equipment**: Weapon, Armor, Ring slots with stat bonuses
- **Quests**: Daily/weekly challenges with rewards
- **Analytics**: Firebase integration for player tracking

## Development Phases

### Phase 1: Core Gameplay ✅
- Player controller with joystick input
- Isometric camera system
- Basic enemy AI (FSM)
- Combat system
- **Status**: Complete

### Phase 2: Combat & Loot (In Progress)
- Enemy variety and elite variants
- Loot generation and drops
- Damage numbers and effects
- Hit detection and knockback
- **Estimated**: 1 week

### Phase 3: Inventory & Equipment
- Grid-based inventory UI
- Equipment system with stat bonuses
- Item database and rarity system
- Character stats display
- **Estimated**: 3-4 days

### Phase 4: Monetization
- Google Play Billing integration
- Game Pass system
- Premium chests
- XP boost system
- **Estimated**: 3-4 days

### Phase 5: Quests & Polish
- Quest system with tracking
- Daily/weekly challenges
- Reward distribution
- UI polish and animations
- **Estimated**: 1 week

## Project Structure

```
HackSlashGame/
├── Assets/
│   ├── Scripts/
│   │   ├── Core/           # GameManager, InputManager, CameraController
│   │   ├── Player/         # PlayerController, PlayerStats, PlayerCombat
│   │   ├── Enemy/          # EnemyController, EnemyAI, EnemyStats
│   │   ├── Combat/         # DamageSystem, HitDetection, DamageNumbers
│   │   ├── Loot/           # LootItem, LootManager, ItemDatabase
│   │   ├── Inventory/      # InventoryManager, Equipment
│   │   ├── Monetization/   # GamePassManager, PremiumChests, IAP
│   │   ├── UI/             # HUD, HealthBar, ActionBar
│   │   └── Utils/          # Constants, ObjectPool, EventSystem
│   ├── Prefabs/
│   ├── Scenes/
│   ├── Materials/
│   ├── Textures/
│   ├── Audio/
│   └── Resources/
│       └── Data/           # JSON databases
└── Documentation/
    ├── PHASE_1_DOCUMENTATION.md
    ├── PHASE_2_DOCUMENTATION.md (TODO)
    └── ARCHITECTURE.md (TODO)
```

## Quick Start

### Prerequisites
- Unity 2022 LTS or newer
- Android SDK (for Android builds)
- Xcode (for iOS builds)

### Setup

1. **Clone/Download Project**
   ```bash
   git clone <repository>
   cd HackSlashGame
   ```

2. **Open in Unity**
   - Open Unity Hub
   - Add project folder
   - Open with Unity 2022 LTS+

3. **Setup Scene**
   - Open `Assets/Scenes/GameScene.unity`
   - Ensure NavMesh is baked (Window → AI → Navigation)
   - Play in editor

4. **Build for Mobile**
   - File → Build Settings
   - Select Android or iOS
   - Configure player settings
   - Build and run

## Key Systems Explained

### Input System
- **Virtual Joystick**: Left side of screen, 8-directional movement
- **Attack Button**: Right side, primary attack
- **Dash Button**: Right side (above attack), dodge ability
- **Desktop Testing**: Mouse movement + left-click for attack

### Combat System
- **Melee Attack**: Cone-based AOE (90° angle, 2 unit range)
- **Crit Chance**: 15% base chance, 1.5x damage multiplier
- **Damage Reduction**: Armor formula: `damage * (1 - armor/(armor+100))`
- **Cooldowns**: Attack (0.8s), Dash (2s)

### Enemy AI (FSM)
```
Idle ↔ Patrol ↔ Chase → Attack
         ↓
       Death
```
- **Aggro Range**: 10 units
- **Attack Range**: 1.5 units
- **Patrol Range**: 5 units
- **Elite Modifier**: +50% HP, +25% Damage

### Camera System
- **Isometric Angle**: 45°
- **Follow Speed**: 5 units/sec
- **Zoom Range**: 5-15 units
- **Height Offset**: 8 units

## Performance Targets

- **FPS**: 60 FPS on mid-range Android/iOS devices
- **Memory**: <300MB RAM usage
- **Battery**: Optimized for 2-3 hour sessions
- **Network**: Minimal data usage (analytics only)

## Testing

### Unit Tests
```bash
# Run tests in Unity Test Framework
Window → General → Test Runner
```

### Manual Testing Checklist
- [ ] Player movement (all 8 directions)
- [ ] Camera follow and zoom
- [ ] Attack animation and hit detection
- [ ] Enemy spawning and AI states
- [ ] Damage calculation and health bars
- [ ] UI responsiveness
- [ ] Mobile touch input
- [ ] Performance on target devices

## Known Issues

- Pinch zoom not yet implemented (TODO)
- Knockback effect needs tweaking (TODO)
- Damage numbers not yet visible (TODO)
- Sound effects missing (TODO)

## Contributing

Guidelines for team members:
1. Follow code style in `Constants.cs`
2. Use XML documentation for public methods
3. Test before committing
4. Update documentation when adding features
5. Use meaningful commit messages

## License

Proprietary - All rights reserved

## Contact

**Project Lead**: [Your Name]  
**Email**: [Your Email]  
**Discord**: [Server Link]

---

## Roadmap

### Post-Launch Content
- [ ] New enemy types and bosses
- [ ] Skill trees and ability system
- [ ] Dungeons and procedural maps
- [ ] Multiplayer co-op (Phase 2)
- [ ] PvP arena (Phase 3)
- [ ] Story campaign (Phase 4)

### Optimization
- [ ] Shader optimization
- [ ] Memory pooling
- [ ] Network optimization
- [ ] Analytics integration

---

**Last Updated**: May 23, 2026  
**Version**: 1.0.0 (Phase 1 Complete)
