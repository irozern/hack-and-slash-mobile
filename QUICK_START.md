# Quick Start Guide - Hack & Slash Game

Get the game running in **5 minutes** with this quick start guide.

## Prerequisites

- Unity 2022 LTS or newer
- Project already imported

## Step 1: Open the Project

1. Open Unity Hub
2. Click "Add project from disk"
3. Select the `HackSlashGame` folder
4. Wait for import to complete

## Step 2: Create a New Scene

1. File → New Scene
2. Name it "GameScene"
3. Save it to `Assets/Scenes/GameScene.unity`

## Step 3: Create Player Prefab (Quick Method)

1. Create a new GameObject: Right-click in Hierarchy → 3D Object → Capsule
2. Name it "Player"
3. Add these components:
   - CharacterController
   - PlayerController (script)
   - PlayerStats (script)
   - PlayerCombat (script)
   - Animator
   - HitDetection (script)

4. Configure CharacterController:
   - Height: 2
   - Radius: 0.5
   - Center: (0, 1, 0)

5. Drag into `Assets/Prefabs/Player/PlayerPrefab.prefab`

## Step 4: Create Enemy Prefab (Quick Method)

1. Create a new GameObject: Right-click in Hierarchy → 3D Object → Capsule
2. Scale it down: (0.3, 0.3, 0.3)
3. Name it "Enemy"
4. Add these components:
   - NavMeshAgent
   - EnemyStats (script)
   - EnemyAI (script)
   - EnemyController (script)
   - Animator
   - HitDetection (script)

5. Configure NavMeshAgent:
   - Speed: 3.5
   - Stopping Distance: 0.5

6. Drag into `Assets/Prefabs/Enemies/EnemyPrefab.prefab`

## Step 5: Create Ground with NavMesh

1. Create a new Plane: Right-click in Hierarchy → 3D Object → Plane
2. Name it "Ground"
3. Scale it: (50, 1, 50)
4. Remove the Collider component
5. Add NavMeshSurface component (from Navigation package)
6. Click "Bake" to create NavMesh

## Step 6: Add Managers to Scene

Create empty GameObjects and add these scripts:

| GameObject | Script |
|------------|--------|
| GameManager | GameManager.cs |
| InputManager | InputManager.cs |
| ItemDatabase | ItemDatabase.cs |
| LootManager | LootManager.cs |
| InventoryManager | InventoryManager.cs |
| GamePassManager | GamePassManager.cs |
| PremiumChestManager | PremiumChestManager.cs |
| QuestManager | QuestManager.cs |

## Step 7: Configure GameManager

1. Select GameManager in Hierarchy
2. In Inspector, assign:
   - Player Prefab: Your Player prefab
   - Enemy Prefab: Your Enemy prefab
   - Initial Enemy Count: 5
   - Spawn Radius: 20

## Step 8: Create UI Canvas

1. Right-click in Hierarchy → UI → Canvas
2. Set Render Mode: Screen Space - Overlay
3. Add CanvasScaler component

### Add Joystick UI

1. Right-click on Canvas → UI → Panel
2. Name it "JoystickBase"
3. Position: Bottom-left (set Anchor to bottom-left)
4. Size: 200x200
5. Add Image component (gray color)

6. Create child Image for joystick handle:
   - Right-click on JoystickBase → UI → Image
   - Name it "JoystickHandle"
   - Size: 80x80
   - Color: White

### Add Attack Button

1. Right-click on Canvas → UI → Button
2. Name it "AttackButton"
3. Position: Bottom-right
4. Size: 100x100
5. Color: Red

### Add Dash Button

1. Duplicate AttackButton
2. Name it "DashButton"
3. Position above AttackButton
4. Color: Blue

## Step 9: Configure InputManager

1. Select InputManager in Hierarchy
2. Assign in Inspector:
   - Joystick Base: JoystickBase
   - Joystick Handle: JoystickHandle
   - Attack Button: AttackButton
   - Dash Button: DashButton

## Step 10: Create Camera Controller

1. Create empty GameObject: "CameraController"
2. Add CameraController script
3. Position camera at: (0, 8, -10)
4. In CameraController, assign Main Camera reference

## Step 11: Play!

1. Press Play button
2. Use joystick to move
3. Click Attack button to attack
4. Click Dash button to dodge
5. Enemies should spawn and attack you
6. Kill enemies to get loot

---

## Troubleshooting

### Enemies not spawning
- Check NavMesh is baked (Window → AI → Navigation)
- Check GameManager has prefabs assigned
- Check spawn radius is not too small

### Player not moving
- Check InputManager is in scene
- Check joystick UI is assigned
- Check CharacterController is enabled

### No enemies attacking
- Check enemy prefabs have all components
- Check NavMeshAgent is configured
- Check EnemyAI script is assigned

### Loot not appearing
- Check ItemDatabase is in scene
- Check items.json is in Resources/Data/
- Check LootManager is in scene

---

## Next Steps

1. **Add Animations**
   - Create Animator controllers
   - Add movement and attack animations
   - Assign to Player and Enemy

2. **Add Sound**
   - Import audio clips
   - Create AudioManager
   - Add sound effects

3. **Create More Content**
   - Add more enemy types
   - Create more items
   - Design levels/dungeons

4. **Polish**
   - Add particle effects
   - Improve UI
   - Optimize performance

---

## Full Documentation

For detailed setup instructions, see:
- `COMPLETE_SETUP_GUIDE.md` — Step-by-step for all phases
- `PHASE_1_DOCUMENTATION.md` — Phase 1 detailed reference
- `PHASE_2_5_DOCUMENTATION.md` — Phases 2-5 detailed reference

---

**Happy developing!** 🎮
