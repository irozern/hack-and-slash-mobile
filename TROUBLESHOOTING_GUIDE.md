# Troubleshooting Guide - Hack & Slash Game

Complete guide for solving common issues and problems.

---

## 🔴 Critical Issues

### Issue: Game Won't Start / Crashes on Launch

**Symptoms**: 
- White screen
- Immediate crash
- Console errors

**Solutions**:

1. **Check Console for Errors**
   ```
   Window → General → Console
   Look for red error messages
   ```

2. **Verify All Managers in Scene**
   - GameManager ✓
   - InputManager ✓
   - CameraController ✓
   - ItemDatabase ✓
   - LootManager ✓
   - InventoryManager ✓
   - GamePassManager ✓
   - PremiumChestManager ✓
   - QuestManager ✓

3. **Check Main Camera**
   - Must have tag "MainCamera"
   - Must have Camera component
   - Must have CameraController script

4. **Verify NavMesh**
   - Window → AI → Navigation
   - Select ground plane
   - Click "Bake"
   - Should see blue mesh

5. **Reset Project**
   ```
   Delete Library folder
   Reopen project
   Let Unity reimport
   ```

---

## 🟡 Gameplay Issues

### Issue: Player Won't Move

**Symptoms**:
- Joystick doesn't respond
- Player stays in place
- No movement at all

**Solutions**:

1. **Check InputManager**
   - Must be in scene
   - Must have InputManager script
   - Joystick UI must be assigned

2. **Check Joystick UI**
   - Must exist in Canvas
   - Must have Image component
   - Must have RectTransform
   - Position: Bottom-left

3. **Check PlayerController**
   - Must have CharacterController
   - Must have PlayerController script
   - Must have Player tag

4. **Debug Input**
   ```csharp
   // Add to PlayerController.cs temporarily
   Debug.Log($"Input: {InputManager.Instance.GetMovementInput()}");
   ```

5. **Check Character Controller**
   - Height: 2
   - Radius: 0.5
   - Center: (0, 1, 0)

---

### Issue: Enemies Don't Spawn

**Symptoms**:
- No enemies appear
- Game is empty
- Only player visible

**Solutions**:

1. **Check GameManager**
   - Must be in scene
   - Must have GameManager script
   - Initial enemy count > 0

2. **Check NavMesh**
   - Must be baked
   - Must cover spawn area
   - Must be blue in Scene view

3. **Check Enemy Prefab**
   - Must have NavMeshAgent
   - Must have EnemyAI script
   - Must have EnemyController script

4. **Check Spawn Radius**
   - Must be > 0
   - Must be < 50
   - Recommended: 20

5. **Check Logs**
   ```
   Console should show:
   "Spawned X enemies"
   ```

---

### Issue: Enemies Don't Chase Player

**Symptoms**:
- Enemies stand still
- Enemies don't move toward player
- No AI behavior

**Solutions**:

1. **Check EnemyAI Script**
   - Must have FSM logic
   - Must have Chase state
   - Must have aggro range

2. **Check NavMeshAgent**
   - Must be enabled
   - Must have valid destination
   - Speed must be > 0

3. **Check Pathfinding**
   - NavMesh must be baked
   - Enemy must be on NavMesh
   - Path must be clear

4. **Debug AI State**
   ```csharp
   // Add to EnemyAI.cs temporarily
   Debug.Log($"Enemy State: {currentState}");
   ```

5. **Check Distance to Player**
   - Aggro range: 10 units
   - If player > 10 units away, enemy won't chase

---

### Issue: Combat Not Working

**Symptoms**:
- Attacks don't damage enemies
- No damage numbers appear
- Enemies take no damage

**Solutions**:

1. **Check PlayerCombat Script**
   - Must have attack logic
   - Must detect enemies
   - Must apply damage

2. **Check Hit Detection**
   - Must have HitDetection script
   - Must detect collisions
   - Must apply knockback

3. **Check Damage Calculation**
   ```csharp
   // Verify in PlayerCombat.cs
   float damage = playerStats.damage + equipmentBonus;
   Debug.Log($"Damage: {damage}");
   ```

4. **Check Attack Range**
   - Range: 2 units
   - Angle: 90 degrees
   - If enemy > 2 units away, won't hit

5. **Check Attack Cooldown**
   - Cooldown: 0.8 seconds
   - Wait between attacks
   - Cooldown must expire

---

### Issue: Loot Doesn't Drop

**Symptoms**:
- No items appear when enemy dies
- Inventory stays empty
- No loot on ground

**Solutions**:

1. **Check LootManager**
   - Must be in scene
   - Must have LootManager script
   - Drop rate must be > 0

2. **Check ItemDatabase**
   - Must load items from JSON
   - Must have items available
   - Check console for load errors

3. **Check Enemy Death**
   - Enemy must actually die
   - Death state must trigger
   - Loot must be spawned

4. **Check Loot Drop Rate**
   - Default: 60% for common
   - Increase in Constants.cs if needed
   - Must be between 0-1

5. **Check Loot Spawn Position**
   - Must be near enemy
   - Must be above ground
   - Must be visible

---

## 🟡 UI Issues

### Issue: HUD Not Displaying

**Symptoms**:
- No health bar visible
- No mana bar visible
- No UI elements

**Solutions**:

1. **Check Canvas**
   - Must exist in scene
   - Must have Canvas component
   - Render Mode: Screen Space - Overlay

2. **Check HUDManager**
   - Must be attached to Canvas
   - Must have HUDManager script
   - Must have references to UI elements

3. **Check UI Elements**
   - Health Bar: Must exist
   - Mana Bar: Must exist
   - Experience Bar: Must exist
   - All must have Image components

4. **Check Canvas Scaler**
   - Must have CanvasScaler
   - Scale Mode: Scale with Screen Size
   - Reference Resolution: 1080x1920

5. **Check Layer**
   - UI must be on UI layer
   - Canvas must be visible
   - Check sorting order

---

### Issue: Buttons Don't Work

**Symptoms**:
- Attack button doesn't attack
- Dash button doesn't dash
- Buttons not responsive

**Solutions**:

1. **Check Button Component**
   - Must have Button script
   - Must have Image component
   - Must have OnClick event

2. **Check Button Events**
   - OnClick must have listener
   - Listener must call correct method
   - Method must exist

3. **Check Input Handler**
   - InputManager must detect button press
   - Must call correct function
   - Must pass correct parameters

4. **Check Button Position**
   - Must be visible on screen
   - Must not be behind other UI
   - Must be clickable area

5. **Debug Button Press**
   ```csharp
   // Add to button OnClick event
   Debug.Log("Button pressed!");
   ```

---

## 🟡 Performance Issues

### Issue: Low FPS / Stuttering

**Symptoms**:
- Game runs slow
- Stuttering/lag
- FPS < 60

**Solutions**:

1. **Check Enemy Count**
   - Reduce initial enemy count
   - Reduce spawn rate
   - Limit max enemies

2. **Check Physics**
   - Disable unnecessary colliders
   - Use kinematic rigidbodies
   - Reduce physics update rate

3. **Check Rendering**
   - Disable shadows
   - Use simple materials
   - Batch rendering

4. **Check Memory**
   - Check Profiler (Window → Analysis → Profiler)
   - Look for memory leaks
   - Check garbage collection

5. **Optimize Code**
   ```csharp
   // Cache references
   private Transform cachedTransform;
   
   void Start()
   {
       cachedTransform = transform; // Cache once
   }
   
   void Update()
   {
       // Use cached reference
       cachedTransform.position += Vector3.forward;
   }
   ```

---

### Issue: High Memory Usage

**Symptoms**:
- Game uses > 500 MB RAM
- Crashes on low-end devices
- Memory keeps growing

**Solutions**:

1. **Check Object Pooling**
   - Reuse objects instead of destroying
   - Disable instead of destroy
   - Implement object pool

2. **Check Texture Sizes**
   - Compress textures
   - Use appropriate resolution
   - Limit texture count

3. **Check Audio**
   - Compress audio files
   - Use streaming for long clips
   - Limit simultaneous sounds

4. **Check Garbage Collection**
   - Avoid allocations in Update()
   - Cache references
   - Use object pooling

5. **Profile Memory**
   ```
   Window → Analysis → Profiler
   Memory tab
   Look for large allocations
   ```

---

## 🟡 Build Issues

### Issue: Android Build Fails

**Symptoms**:
- Build error
- APK not created
- Compilation error

**Solutions**:

1. **Check Android SDK**
   - Must be installed
   - Path must be correct
   - Version must be compatible

2. **Check Build Settings**
   - Platform: Android
   - Min API: 24
   - Target API: 33

3. **Check Keystore**
   - Must exist
   - Must have valid key
   - Password must be correct

4. **Check Console**
   - Look for error messages
   - Search for specific error
   - Check Build Report

5. **Clean Build**
   ```
   File → Build Settings
   Click "Clean Build"
   Rebuild
   ```

---

### Issue: iOS Build Fails

**Symptoms**:
- Build error
- IPA not created
- Xcode error

**Solutions**:

1. **Check iOS SDK**
   - Must be installed
   - Version must be compatible
   - Xcode must be up to date

2. **Check Build Settings**
   - Platform: iOS
   - Min iOS: 14.0
   - Architecture: ARM64

3. **Check Signing**
   - Must have provisioning profile
   - Must have signing certificate
   - Team ID must be correct

4. **Check Xcode**
   - Open project in Xcode
   - Check for errors
   - Fix any issues
   - Build again

5. **Check Console**
   - Look for error messages
   - Check Build Report
   - Search error online

---

## 🟡 Data Issues

### Issue: Items Not Loading

**Symptoms**:
- No items in inventory
- ItemDatabase empty
- JSON not loading

**Solutions**:

1. **Check JSON File**
   - Must exist at: Assets/Resources/Data/items.json
   - Must be valid JSON
   - Must have correct structure

2. **Check ItemDatabaseLoader**
   - Must load from Resources
   - Must parse JSON correctly
   - Must create ItemData objects

3. **Validate JSON**
   ```
   Use online JSON validator
   Check for syntax errors
   Verify structure
   ```

4. **Check Console**
   - Should show: "Loaded X items"
   - If error, check message
   - Look for file path issues

5. **Verify Data**
   ```csharp
   // Add to ItemDatabase.cs
   var items = ItemDatabaseLoader.LoadItemsFromJSON();
   Debug.Log($"Loaded {items.Count} items");
   foreach (var item in items)
   {
       Debug.Log($"Item: {item.itemName}");
   }
   ```

---

### Issue: Enemies Not Loading

**Symptoms**:
- No enemies spawn
- EnemyDatabase empty
- JSON not loading

**Solutions**:

1. **Check JSON File**
   - Must exist at: Assets/Resources/Data/enemies.json
   - Must be valid JSON
   - Must have correct structure

2. **Check EnemyDatabaseLoader**
   - Must load from Resources
   - Must parse JSON correctly
   - Must create EnemyData objects

3. **Validate JSON**
   ```
   Use online JSON validator
   Check for syntax errors
   Verify structure
   ```

4. **Check Console**
   - Should show: "Loaded X enemy types"
   - If error, check message
   - Look for file path issues

5. **Verify Data**
   ```csharp
   // Add to GameManager.cs
   var enemies = EnemyDatabaseLoader.LoadEnemiesFromJSON();
   Debug.Log($"Loaded {enemies.Count} enemy types");
   ```

---

## 🟢 Common Solutions

### Clear Cache
```
Delete Library folder
Reopen project
Let Unity reimport
```

### Reimport Assets
```
Right-click Assets folder
Select Reimport
Wait for completion
```

### Reset Scene
```
File → New Scene
Or delete and recreate GameScene
```

### Check Console
```
Window → General → Console
Look for error messages
Search for specific error
```

### Use Profiler
```
Window → Analysis → Profiler
Check CPU usage
Check memory usage
Check frame time
```

---

## 📞 Getting Help

### Before Asking for Help

1. **Check Console** for error messages
2. **Read Documentation** for relevant guide
3. **Search Troubleshooting** for your issue
4. **Run Unit Tests** to verify systems
5. **Check Code Comments** for implementation details

### If Still Stuck

1. **Isolate Problem** - Which system is failing?
2. **Reproduce Issue** - Can you repeat it?
3. **Check Logs** - What does console say?
4. **Review Code** - Is implementation correct?
5. **Test Components** - Are all components present?

---

## 🔧 Debug Commands

Add these to your code for debugging:

```csharp
// Player Debug
Debug.Log($"Player Position: {player.transform.position}");
Debug.Log($"Player HP: {playerStats.currentHealth}");
Debug.Log($"Player Level: {playerStats.level}");

// Enemy Debug
Debug.Log($"Enemy Count: {enemies.Count}");
Debug.Log($"Enemy State: {enemyAI.currentState}");
Debug.Log($"Enemy HP: {enemyStats.currentHealth}");

// Combat Debug
Debug.Log($"Damage: {damage}");
Debug.Log($"Crit: {isCrit}");
Debug.Log($"Hit: {isHit}");

// Loot Debug
Debug.Log($"Loot Spawned: {lootItem.itemData.itemName}");
Debug.Log($"Loot Count: {lootItems.Count}");

// UI Debug
Debug.Log($"Health Bar: {healthBar.fillAmount}");
Debug.Log($"Inventory: {inventory.items.Count}");
```

---

## ✅ Verification Checklist

Before reporting issue, verify:

- [ ] All scripts compile
- [ ] No console errors
- [ ] All managers in scene
- [ ] NavMesh baked
- [ ] UI elements visible
- [ ] Player can move
- [ ] Enemies spawn
- [ ] Combat works
- [ ] Loot drops
- [ ] 60 FPS performance

---

## 📝 Issue Report Template

If you need to report an issue:

```
Issue: [Clear title]
Symptoms: [What happens]
Expected: [What should happen]
Steps to Reproduce:
1. [Step 1]
2. [Step 2]
3. [Step 3]
Console Error: [Error message]
Environment: [Unity version, platform]
```

---

**Last Updated**: May 23, 2026  
**Version**: 1.0.0
