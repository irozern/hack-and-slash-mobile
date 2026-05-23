# Hack & Slash Game - Unity Project Structure

## Directory Layout
```
HackSlashGame/
├── Assets/
│   ├── Scripts/
│   │   ├── Core/
│   │   │   ├── GameManager.cs
│   │   │   ├── InputManager.cs
│   │   │   └── CameraController.cs
│   │   ├── Player/
│   │   │   ├── PlayerController.cs
│   │   │   ├── PlayerStats.cs
│   │   │   ├── PlayerCombat.cs
│   │   │   └── PlayerAnimator.cs
│   │   ├── Enemy/
│   │   │   ├── EnemyController.cs
│   │   │   ├── EnemyAI.cs
│   │   │   ├── EnemyStats.cs
│   │   │   └── EnemyAnimator.cs
│   │   ├── Combat/
│   │   │   ├── DamageSystem.cs
│   │   │   ├── HitDetection.cs
│   │   │   └── DamageNumber.cs
│   │   ├── Loot/
│   │   │   ├── LootItem.cs
│   │   │   ├── LootManager.cs
│   │   │   └── ItemDatabase.cs
│   │   ├── Inventory/
│   │   │   ├── InventoryManager.cs
│   │   │   ├── InventoryUI.cs
│   │   │   └── Equipment.cs
│   │   ├── Monetization/
│   │   │   ├── GamePassManager.cs
│   │   │   ├── PremiumChestManager.cs
│   │   │   └── IAPManager.cs
│   │   ├── UI/
│   │   │   ├── HUDManager.cs
│   │   │   ├── HealthBar.cs
│   │   │   ├── ManaBar.cs
│   │   │   └── ActionBar.cs
│   │   └── Utils/
│   │       ├── ObjectPool.cs
│   │       ├── EventSystem.cs
│   │       └── Constants.cs
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
│       ├── Data/
│       │   ├── ItemDatabase.json
│       │   ├── EnemyDatabase.json
│       │   └── QuestDatabase.json
│       └── Prefabs/
└── ProjectSettings/
```

## Key Technologies
- **Unity Version**: 2022 LTS or newer
- **Mobile Platforms**: Android (API 24+), iOS (14+)
- **Networking**: Mirror (for future multiplayer)
- **Monetization**: Unity IAP + Google Play Billing + Apple In-App Purchase
- **Analytics**: Firebase Analytics

## Development Phases
1. **Phase 1**: Core gameplay (player, camera, input)
2. **Phase 2**: Combat & enemies
3. **Phase 3**: Inventory & equipment
4. **Phase 4**: Monetization
5. **Phase 5**: Quests & polish
