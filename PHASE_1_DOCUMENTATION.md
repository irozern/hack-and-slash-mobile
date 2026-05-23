# Phase 1: Player Controller, Isometric Camera & Joystick Controls

## Overview
Phase 1 establishes the core gameplay foundation with player movement, isometric camera system, and mobile-optimized input controls.

## Completed Components

### 1. **Constants.cs** (`Utils/Constants.cs`)
Centralized configuration for all game parameters:
- Player stats (HP, Mana, Damage, Speed, Attack Cooldown)
- Enemy parameters (HP, Damage, Aggro Range, Attack Cooldown)
- Combat settings (Damage numbers, Hit flash duration, Knockback)
- Loot system parameters
- Camera configuration
- UI dimensions
- Animation parameter names
- Layer and tag definitions

**Usage:**
```csharp
float playerSpeed = Constants.Player.MOVE_SPEED;
float enemyAggroRange = Constants.Enemy.AGGRO_RANGE;
```

---

### 2. **InputManager.cs** (`Core/InputManager.cs`)
Handles all input for mobile (Android/iOS) and desktop platforms.

**Features:**
- Virtual joystick with touch tracking
- Attack button (LPM equivalent)
- Dash button
- Automatic fallback to mouse input for testing
- Multi-touch support

**Key Methods:**
```csharp
Vector2 joystickInput = InputManager.Instance.GetJoystickInput();
Vector3 moveDir = InputManager.Instance.GetMovementDirection();
bool isAttacking = InputManager.Instance.IsAttackPressed();
bool isDashing = InputManager.Instance.IsDashPressed();
```

**Implementation Notes:**
- Joystick input is normalized to [-1, 1] range
- Touch IDs are tracked separately for each UI element
- Automatic conversion from 2D touch input to 3D world movement

---

### 3. **CameraController.cs** (`Core/CameraController.cs`)
Implements isometric camera system with smooth following.

**Features:**
- Isometric view angle (45°)
- Smooth camera follow
- Zoom support (mouse wheel for testing, pinch for mobile - TODO)
- World-to-screen coordinate conversion

**Key Methods:**
```csharp
CameraController.Instance.SetTarget(playerTransform);
Vector3 screenPos = CameraController.Instance.WorldToScreenPoint(worldPos);
```

**Camera Configuration:**
- Isometric Angle: 45°
- Camera Distance: 10 units
- Camera Height: 8 units
- Follow Speed: 5 units/sec

---

### 4. **PlayerStats.cs** (`Player/PlayerStats.cs`)
Character statistics and progression system.

**Features:**
- Health and Mana management
- Experience and leveling system
- Damage calculation with crit chance
- Armor-based damage reduction
- Equipment bonus system
- Gold management

**Key Methods:**
```csharp
playerStats.TakeDamage(damage);
playerStats.Heal(amount);
playerStats.GainExperience(amount);
float damage = playerStats.CalculateDamage(); // Includes crit
playerStats.AddBonusDamage(amount); // From equipment
```

**Events:**
```csharp
playerStats.OnHealthChanged += (current, max) => { /* Update UI */ };
playerStats.OnManaChanged += (current, max) => { /* Update UI */ };
playerStats.OnLevelUp += (level) => { /* Handle level up */ };
playerStats.OnDeath += () => { /* Handle death */ };
```

---

### 5. **PlayerController.cs** (`Player/PlayerController.cs`)
Player movement and basic interactions.

**Features:**
- Joystick-based movement
- Smooth rotation toward movement direction
- Dash/roll ability with cooldown
- Gravity and CharacterController integration
- Animation parameter updates

**Key Methods:**
```csharp
Vector3 facingDir = playerController.GetFacingDirection();
playerController.TakeDamage(damage);
bool isDashing = playerController.IsDashing();
```

**Movement System:**
- Uses CharacterController for physics
- Smooth rotation with lerp (10 units/sec rotation speed)
- Dash applies invincibility frames (TODO)
- Gravity applied automatically

---

### 6. **PlayerCombat.cs** (`Player/PlayerCombat.cs`)
Attack system with cone-based AOE detection.

**Features:**
- Melee attack with cooldown
- Cone-based hit detection (90° cone)
- Multiple enemy hitting
- Damage calculation with crit
- Attack animation triggering

**Key Methods:**
```csharp
float cooldownProgress = playerCombat.GetAttackCooldownProgress(); // 0-1
bool isReady = playerCombat.IsAttackReady();
```

**Attack Mechanics:**
- Attack Range: 2 units
- Attack Angle: 90° cone
- Attack Cooldown: 0.8 seconds
- Uses Physics.OverlapSphere for hit detection

---

### 7. **EnemyStats.cs** (`Enemy/EnemyStats.cs`)
Enemy statistics and health management.

**Features:**
- Health management
- Elite enemy modifiers (+50% HP, +25% Damage)
- Armor-based damage reduction
- Death event system

**Key Methods:**
```csharp
enemyStats.TakeDamage(damage);
float healthPercent = enemyStats.GetHealthPercent();
bool isElite = enemyStats.IsElite();
```

---

### 8. **EnemyAI.cs** (`Enemy/EnemyAI.cs`)
Enemy AI using Finite State Machine (FSM).

**States:**
1. **Idle** - Standing still, waiting
2. **Patrol** - Random movement in patrol range
3. **Chase** - Following player when in aggro range
4. **Attack** - Attacking when in attack range
5. **Death** - Dead state

**State Transitions:**
```
Idle → Patrol (random)
Idle/Patrol → Chase (player in aggro range)
Chase → Attack (player in attack range)
Chase → Patrol (player out of aggro range)
Any → Death (health <= 0)
```

**Key Methods:**
```csharp
float healthPercent = enemyAI.GetHealthPercent();
bool isAlive = enemyAI.IsAlive();
```

**NavMesh Requirements:**
- Scene must have NavMesh baked
- Enemy must have NavMeshAgent component
- Aggro Range: 10 units
- Attack Range: 1.5 units
- Patrol Range: 5 units

---

### 9. **EnemyController.cs** (`Enemy/EnemyController.cs`)
Main enemy entity controller.

**Features:**
- Damage application
- Health bar UI updates
- Death handling
- Loot generation (TODO)

**Key Methods:**
```csharp
enemyController.TakeDamage(damage);
bool isAlive = enemyController.IsAlive();
```

---

### 10. **GameManager.cs** (`Core/GameManager.cs`)
Core game management and spawning.

**Features:**
- Player spawning
- Enemy spawning and wave management
- Game state management
- Pause/resume functionality
- Elite enemy random assignment

**Key Methods:**
```csharp
GameManager.Instance.SetGamePaused(true);
bool running = GameManager.Instance.IsGameRunning();
List<EnemyController> enemies = GameManager.Instance.GetActiveEnemies();
```

---

## Setup Instructions

### 1. **Scene Setup**
1. Create a new scene called "GameScene"
2. Add a ground plane (Quad or Plane)
3. Bake NavMesh:
   - Window → AI → Navigation
   - Select ground, mark as "Walkable"
   - Click "Bake"

### 2. **Player Setup**
1. Create Player GameObject:
   - Add Capsule (0.5 scale)
   - Add CharacterController component
   - Add PlayerController script
   - Add PlayerStats script
   - Add PlayerCombat script
   - Add Animator component

2. Create Player Prefab:
   - Drag into Assets/Prefabs/Player/

### 3. **Enemy Setup**
1. Create Enemy GameObject:
   - Add Capsule (0.3 scale)
   - Add NavMeshAgent component
   - Add EnemyStats script
   - Add EnemyAI script
   - Add EnemyController script
   - Add Animator component

2. Create Enemy Prefab:
   - Drag into Assets/Prefabs/Enemies/

### 4. **Camera Setup**
1. Create CameraController GameObject
2. Add CameraController script
3. Assign Main Camera to reference
4. Set Player target in GameManager

### 5. **UI Setup**
1. Create Canvas for HUD
2. Add virtual joystick UI (bottom-left)
3. Add attack button (bottom-right)
4. Add dash button (bottom-right, above attack)
5. Assign to InputManager

### 6. **GameManager Setup**
1. Create GameManager GameObject
2. Add GameManager script
3. Assign Player Prefab
4. Assign Enemy Prefab
5. Set spawn point

---

## Testing Checklist

- [ ] Player moves with joystick input
- [ ] Player rotates toward movement direction
- [ ] Camera follows player smoothly
- [ ] Player can attack (animation plays)
- [ ] Enemies spawn around player
- [ ] Enemies patrol when idle
- [ ] Enemies chase player when in aggro range
- [ ] Enemies attack player when in attack range
- [ ] Damage is applied correctly
- [ ] Health bars update
- [ ] Enemies die when health reaches 0
- [ ] Dash button works (TODO: invincibility)
- [ ] UI buttons respond to touch/click

---

## Known Issues & TODO

### Phase 1 TODOs:
- [ ] Add dash invincibility frames
- [ ] Add knockback effect
- [ ] Add damage numbers UI
- [ ] Add hit flash effect
- [ ] Add attack VFX (slash effect)
- [ ] Add death animation
- [ ] Add pinch zoom for mobile
- [ ] Add sound effects
- [ ] Optimize NavMesh queries

### Performance Considerations:
- Use object pooling for enemies
- Cache component references
- Minimize Physics.OverlapSphere calls
- Use NavMesh for pathfinding (already done)

---

## Next Phase (Phase 2)
- Loot system with item drops
- Damage numbers and hit effects
- Advanced combat mechanics
- Enemy variety and types
- Particle effects system

---

## Code Style Guidelines

All scripts follow these conventions:
- **Namespacing**: Not used (simple project)
- **Naming**: PascalCase for classes, camelCase for variables
- **Comments**: XML documentation for public methods
- **Organization**: Private fields at top, public methods at bottom
- **Constants**: Use Constants.cs for all magic numbers

---

## Architecture Diagram

```
GameManager (Singleton)
├── Player
│   ├── PlayerController (Movement)
│   ├── PlayerStats (Health/Damage)
│   └── PlayerCombat (Attacks)
├── Enemies (List)
│   ├── EnemyController
│   ├── EnemyAI (FSM)
│   └── EnemyStats
├── InputManager (Singleton)
│   └── Virtual Joystick
└── CameraController (Singleton)
    └── Isometric Camera
```

---

## References

- **NavMesh Documentation**: https://docs.unity3d.com/Manual/nav-NavigationSystem.html
- **CharacterController**: https://docs.unity3d.com/ScriptReference/CharacterController.html
- **Physics.OverlapSphere**: https://docs.unity3d.com/ScriptReference/Physics.OverlapSphere.html
