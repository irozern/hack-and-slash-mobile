# Implementation Checklist - Hack & Slash Game

Use this checklist to track your progress setting up the game in Unity.

## Phase 1: Project Setup

### Prerequisites
- [ ] Unity 2022 LTS or newer installed
- [ ] Project imported into Unity
- [ ] All scripts compile without errors
- [ ] No console warnings

### Folder Structure
- [ ] `Assets/Scripts/` folder exists
- [ ] `Assets/Prefabs/` folder exists
- [ ] `Assets/Scenes/` folder exists
- [ ] `Assets/Resources/Data/` folder exists
- [ ] `Assets/Tests/` folder exists

### Data Files
- [ ] `items.json` in `Assets/Resources/Data/`
- [ ] `enemies.json` in `Assets/Resources/Data/`
- [ ] ItemDatabaseLoader.cs exists
- [ ] EnemyDatabaseLoader.cs exists

---

## Phase 2: Scene Creation

### Ground Setup
- [ ] Create new scene: `GameScene.unity`
- [ ] Create Plane for ground (50x50 scale)
- [ ] Remove Collider from ground
- [ ] Add NavMeshSurface to ground
- [ ] Bake NavMesh (Window → AI → Navigation)
- [ ] Verify NavMesh is blue/visible

### Manager Setup
- [ ] Create GameManager GameObject
- [ ] Create InputManager GameObject
- [ ] Create ItemDatabase GameObject
- [ ] Create LootManager GameObject
- [ ] Create InventoryManager GameObject
- [ ] Create GamePassManager GameObject
- [ ] Create PremiumChestManager GameObject
- [ ] Create QuestManager GameObject
- [ ] Verify all managers are in scene

### Camera Setup
- [ ] Create CameraController GameObject
- [ ] Add CameraController script
- [ ] Position camera at (0, 8, -10)
- [ ] Assign Main Camera reference
- [ ] Test camera follows player

---

## Phase 3: Player Setup

### Player GameObject
- [ ] Create Player GameObject
- [ ] Add Capsule as child (visual)
- [ ] Add CharacterController component
- [ ] Configure CharacterController:
  - [ ] Height: 2
  - [ ] Radius: 0.5
  - [ ] Center: (0, 1, 0)

### Player Scripts
- [ ] Add PlayerStats script
- [ ] Add PlayerController script
- [ ] Add PlayerCombat script
- [ ] Add HitDetection script
- [ ] Add Animator component

### Player Configuration
- [ ] Set tag to "Player"
- [ ] Position at (0, 0, 0)
- [ ] Test movement
- [ ] Test attack
- [ ] Test dash

---

## Phase 4: Enemy Setup

### Enemy GameObject
- [ ] Create Enemy GameObject
- [ ] Add Capsule as child (scale 0.3)
- [ ] Add NavMeshAgent component
- [ ] Configure NavMeshAgent:
  - [ ] Speed: 3.5
  - [ ] Stopping Distance: 0.5
  - [ ] Auto Braking: True

### Enemy Scripts
- [ ] Add EnemyStats script
- [ ] Add EnemyAI script
- [ ] Add EnemyController script
- [ ] Add HitDetection script
- [ ] Add Animator component

### Enemy Configuration
- [ ] Set tag to "Enemy"
- [ ] Test patrol behavior
- [ ] Test chase behavior
- [ ] Test attack behavior
- [ ] Test death behavior

---

## Phase 5: UI Setup

### Canvas
- [ ] Create Canvas
- [ ] Set Render Mode: Screen Space - Overlay
- [ ] Add CanvasScaler
- [ ] Set Scale Mode: Scale with Screen Size

### Joystick UI
- [ ] Create Panel for joystick base
- [ ] Position: Bottom-left
- [ ] Size: 200x200
- [ ] Add Image component (gray)
- [ ] Create child Image for handle
- [ ] Handle size: 80x80
- [ ] Handle color: White

### Buttons
- [ ] Create Attack Button
- [ ] Position: Bottom-right
- [ ] Size: 100x100
- [ ] Color: Red
- [ ] Add Text "ATTACK"
- [ ] Create Dash Button
- [ ] Position: Above Attack
- [ ] Size: 100x100
- [ ] Color: Blue
- [ ] Add Text "DASH"

### HUD Elements
- [ ] Create Health Bar (red)
- [ ] Create Mana Bar (blue)
- [ ] Create Experience Bar (yellow)
- [ ] Create Level Text
- [ ] Create Score Text

---

## Phase 6: Input Configuration

### InputManager Setup
- [ ] Select InputManager in scene
- [ ] Assign Joystick Base
- [ ] Assign Joystick Handle
- [ ] Assign Attack Button
- [ ] Assign Dash Button
- [ ] Test joystick input
- [ ] Test attack button
- [ ] Test dash button

---

## Phase 7: Loot System

### Loot Item Setup
- [ ] Create Loot GameObject
- [ ] Add Cube as child (scale 0.3)
- [ ] Add Rigidbody
- [ ] Configure Rigidbody:
  - [ ] Mass: 0.5
  - [ ] Constraints: Freeze Rotation X & Z
- [ ] Add LootItem script
- [ ] Test loot drops
- [ ] Test loot despawn
- [ ] Test auto-pickup

---

## Phase 8: Inventory System

### Inventory UI
- [ ] Create Inventory Panel
- [ ] Create 5x5 grid (25 slots)
- [ ] Create Equipment Panel
- [ ] Create 3 equipment slots
- [ ] Create Stats Display
- [ ] Test inventory open/close
- [ ] Test item pickup
- [ ] Test equipment

---

## Phase 9: Monetization

### Game Pass UI
- [ ] Create Game Pass Panel
- [ ] Show pass status
- [ ] Show time remaining
- [ ] Show purchase button
- [ ] Test Game Pass purchase

### Premium Chest UI
- [ ] Create Chest Panel
- [ ] Show chest inventory
- [ ] Create open button
- [ ] Test chest opening
- [ ] Test reward display

---

## Phase 10: Testing

### Core Gameplay
- [ ] Player can move with joystick
- [ ] Player can attack enemies
- [ ] Player can dash/dodge
- [ ] Enemies spawn correctly
- [ ] Enemies patrol
- [ ] Enemies chase player
- [ ] Enemies attack player
- [ ] Player takes damage
- [ ] Enemies die

### Combat
- [ ] Damage numbers appear
- [ ] Hit flash effect plays
- [ ] Knockback applies
- [ ] Crit chance works
- [ ] Crit multiplier works

### Loot
- [ ] Enemies drop loot
- [ ] Loot has correct rarity color
- [ ] Loot despawns after 60s
- [ ] Player auto-picks up loot
- [ ] Loot adds to inventory

### Inventory
- [ ] Items display in inventory
- [ ] Items can be equipped
- [ ] Equipment bonuses apply
- [ ] Stats update correctly

### HUD
- [ ] Health bar updates
- [ ] Mana bar updates
- [ ] Experience bar updates
- [ ] Level displays
- [ ] Damage numbers display

### Performance
- [ ] 60 FPS in editor
- [ ] No memory leaks
- [ ] No console errors
- [ ] No console warnings

---

## Phase 11: Optimization

### Graphics
- [ ] Disable unnecessary shadows
- [ ] Use simple materials
- [ ] Batch rendering
- [ ] Use LOD for distant objects

### Physics
- [ ] Disable unnecessary colliders
- [ ] Use kinematic rigidbodies where possible
- [ ] Limit physics update rate

### Memory
- [ ] Implement object pooling
- [ ] Unload unused assets
- [ ] Compress textures
- [ ] Limit active enemies

### CPU
- [ ] Cache component references
- [ ] Use coroutines for delays
- [ ] Batch UI updates
- [ ] Optimize AI calculations

---

## Phase 12: Build Preparation

### Android
- [ ] Set package name
- [ ] Set minimum API level: 24
- [ ] Set target API level: 33
- [ ] Create keystore
- [ ] Create signing key
- [ ] Test APK build

### iOS
- [ ] Set bundle ID
- [ ] Set minimum iOS version: 14.0
- [ ] Create provisioning profile
- [ ] Configure signing
- [ ] Test iOS build

---

## Phase 13: Testing on Devices

### Android Testing
- [ ] Install APK on Android device
- [ ] Test all gameplay features
- [ ] Test all UI elements
- [ ] Test performance
- [ ] Check for crashes

### iOS Testing
- [ ] Install IPA on iOS device
- [ ] Test all gameplay features
- [ ] Test all UI elements
- [ ] Test performance
- [ ] Check for crashes

---

## Phase 14: Release Preparation

### App Store
- [ ] Create app listing
- [ ] Write description
- [ ] Upload screenshots
- [ ] Set price
- [ ] Configure metadata

### Google Play
- [ ] Create app listing
- [ ] Write description
- [ ] Upload screenshots
- [ ] Set price
- [ ] Configure metadata

---

## Final Verification

- [ ] All scripts compile
- [ ] No console errors
- [ ] No console warnings
- [ ] Game runs at 60 FPS
- [ ] All features working
- [ ] Tested on real devices
- [ ] Ready for submission

---

## Completion Status

**Total Items**: 200+  
**Completed**: [ ] / 200+  
**Percentage**: 0%

---

**Last Updated**: May 23, 2026  
**Status**: Ready for Implementation
