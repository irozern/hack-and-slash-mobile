# Getting Started - Hack & Slash Game

Your complete guide to getting the game up and running in minutes.

---

## 🎯 Choose Your Path

### Path 1: Super Fast (5 Minutes)
**For**: Developers who want to see the game immediately

1. Open project in Unity
2. Create new scene
3. Add SceneGenerator
4. Click "Generate Scene"
5. Press Play

✅ **Result**: Fully playable game in editor

---

### Path 2: Quick Setup (15 Minutes)
**For**: Developers who want guided setup

1. Read QUICK_START.md
2. Follow 10 steps
3. Test in editor
4. Build for mobile

✅ **Result**: Game ready for Android/iOS

---

### Path 3: Complete Setup (1-2 Hours)
**For**: Developers who want full understanding

1. Read COMPLETE_SETUP_GUIDE.md
2. Follow detailed instructions
3. Configure all systems
4. Understand architecture

✅ **Result**: Full control over every system

---

## 📚 Documentation Map

```
START HERE
    ↓
Choose Your Path (above)
    ↓
┌─────────────────────────────────────┐
│ Path 1: Super Fast (5 min)          │
│ → SceneGenerator                    │
│ → Press Play                        │
└─────────────────────────────────────┘
    ↓
┌─────────────────────────────────────┐
│ Path 2: Quick Setup (15 min)        │
│ → QUICK_START.md                    │
│ → 10 steps                          │
│ → Build for mobile                  │
└─────────────────────────────────────┘
    ↓
┌─────────────────────────────────────┐
│ Path 3: Complete Setup (1-2 hours)  │
│ → COMPLETE_SETUP_GUIDE.md           │
│ → All systems explained             │
│ → Full customization                │
└─────────────────────────────────────┘
    ↓
Ready to Deploy
    ↓
DEPLOYMENT_GUIDE.md
    ↓
Google Play Store & Apple App Store
```

---

## 🚀 Super Fast Path (Recommended for First Time)

### Step 1: Open Project
```
1. Open Unity Hub
2. Click "Add project from disk"
3. Select HackSlashGame folder
4. Wait for import
```

### Step 2: Create Scene
```
1. File → New Scene
2. Name: "GameScene"
3. Save to Assets/Scenes/
```

### Step 3: Add SceneGenerator
```
1. Right-click in Hierarchy
2. Create Empty → Name "Setup"
3. Add Component → SceneGenerator
4. In Inspector, set Initial Enemy Count: 5
```

### Step 4: Generate
```
1. In Inspector, find SceneGenerator
2. Click "Generate Scene" button
3. Wait for console message "Scene generation complete!"
```

### Step 5: Play
```
1. Press Play button
2. Use joystick to move
3. Click Attack button to attack
4. Click Dash button to dodge
5. Kill enemies and collect loot!
```

✅ **Done!** Game is fully playable.

---

## 🎮 First Time Playing

### Controls
- **Joystick** (bottom-left): Move character
- **Attack Button** (bottom-right, red): Melee attack
- **Dash Button** (bottom-right, blue): Dodge/Roll

### Gameplay
- Move around with joystick
- Enemies will spawn around you
- Attack enemies to damage them
- Dodge incoming attacks
- Kill enemies to get loot
- Collect items to upgrade stats
- Reach higher levels

### Tips
- Keep moving to avoid damage
- Use dash to escape danger
- Attack multiple enemies at once
- Collect rare items for better stats
- Complete quests for rewards

---

## 🛠️ Customization

### Change Game Settings

**Edit Constants.cs:**
```csharp
// Player settings
public static class Player
{
    public const float MOVE_SPEED = 5f;
    public const float ATTACK_COOLDOWN = 0.8f;
    public const float DASH_COOLDOWN = 2f;
}

// Enemy settings
public static class Enemy
{
    public const float SPAWN_RADIUS = 20f;
    public const int INITIAL_COUNT = 5;
}

// Combat settings
public static class Combat
{
    public const float ATTACK_RANGE = 2f;
    public const float ATTACK_ANGLE = 90f;
    public const float CRIT_CHANCE = 0.15f;
}
```

### Add New Items

**Edit items.json:**
```json
{
  "id": 11,
  "itemName": "Legendary Sword",
  "itemType": "Weapon",
  "rarity": "Legendary",
  "damageBonus": 100,
  "attackSpeedBonus": 0.5,
  "sellPrice": 1000,
  "buyPrice": 2000
}
```

### Add New Enemies

**Edit enemies.json:**
```json
{
  "id": 6,
  "name": "Dragon",
  "level": 5,
  "maxHealth": 200,
  "damage": 50,
  "armor": 20,
  "moveSpeed": 4,
  "experience": 500,
  "goldReward": 500
}
```

---

## 📱 Building for Mobile

### Android Build

```
1. File → Build Settings
2. Select Android
3. Player Settings:
   - Package Name: com.company.hackslash
   - Min API: 24
   - Target API: 33
4. Build → Build and Run
```

### iOS Build

```
1. File → Build Settings
2. Select iOS
3. Player Settings:
   - Bundle ID: com.company.hackslash
   - Min iOS: 14.0
4. Build → Choose location
5. Open in Xcode
6. Build and Run
```

---

## 🧪 Testing

### In Editor
```
1. Press Play
2. Test all features
3. Check console for errors
4. Verify performance (60 FPS)
```

### On Device
```
1. Build APK/IPA
2. Install on device
3. Test gameplay
4. Check performance
5. Report any issues
```

### Unit Tests
```
1. Window → General → Test Runner
2. Run All Tests
3. Verify all pass
```

---

## 🐛 Troubleshooting

### Game Won't Start
**Solution**: Check console for errors
- Missing scripts?
- Missing components?
- NavMesh not baked?

### No Enemies Spawning
**Solution**: 
- Check NavMesh is baked
- Check GameManager is in scene
- Check spawn radius is valid

### Player Not Moving
**Solution**:
- Check InputManager is in scene
- Check joystick UI is assigned
- Check CharacterController is enabled

### Low Performance
**Solution**:
- Reduce enemy count
- Disable shadows
- Use simpler materials
- Profile with Profiler window

---

## 📖 Next Steps

### After First Play (30 minutes)
1. ✅ Read QUICK_START.md
2. ✅ Understand the controls
3. ✅ Explore the code
4. ✅ Customize settings

### Before Building (1-2 hours)
1. ✅ Read COMPLETE_SETUP_GUIDE.md
2. ✅ Understand all systems
3. ✅ Test all features
4. ✅ Optimize performance

### Before Launching (1 day)
1. ✅ Read BUILD_CONFIGURATION.md
2. ✅ Configure build settings
3. ✅ Build for Android
4. ✅ Build for iOS
5. ✅ Test on real devices

### Before Submitting (1 week)
1. ✅ Read DEPLOYMENT_GUIDE.md
2. ✅ Prepare app store listings
3. ✅ Create screenshots
4. ✅ Write description
5. ✅ Submit to app stores

---

## 🎓 Learning Resources

### Included in Project
- 220+ pages of documentation
- 13 unit tests
- 25 well-commented scripts
- 2 JSON data files
- 3 automated tools

### External Resources
- [Unity Documentation](https://docs.unity3d.com/)
- [Mobile Development](https://developer.android.com/)
- [Game Design Patterns](https://gameprogrammingpatterns.com/)

---

## ✅ Verification Checklist

After setup, verify everything works:

- [ ] Game runs in editor
- [ ] Player can move
- [ ] Player can attack
- [ ] Player can dash
- [ ] Enemies spawn
- [ ] Enemies chase player
- [ ] Enemies attack player
- [ ] Loot drops from enemies
- [ ] Loot auto-pickups
- [ ] HUD displays correctly
- [ ] No console errors
- [ ] 60 FPS performance

---

## 🚀 You're Ready!

You now have everything needed to:
- ✅ Play the game
- ✅ Understand the code
- ✅ Customize features
- ✅ Build for mobile
- ✅ Launch on app stores

---

## 📞 Need Help?

1. **Check the documentation** - 220+ pages of guides
2. **Read the code comments** - Well documented
3. **Run the unit tests** - Verify systems work
4. **Check troubleshooting** - Common issues covered

---

## 🎉 Have Fun!

The game is fully playable and ready to customize. Start with the Super Fast Path, then explore the other documentation as needed.

**Happy gaming!** 🎮

---

**Last Updated**: May 23, 2026  
**Version**: 1.0.0  
**Status**: Ready to Play
