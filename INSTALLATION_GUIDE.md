# Installation Guide - Hack & Slash Game

**Complete step-by-step guide for installing and setting up the project.**

---

## 📋 Pre-Installation Requirements

### System Requirements

| Requirement | Minimum | Recommended |
|-------------|---------|-------------|
| **OS** | Windows 10, macOS 10.13, Ubuntu 18.04 | Windows 11, macOS 12, Ubuntu 22.04 |
| **RAM** | 4 GB | 8 GB+ |
| **Storage** | 10 GB | 20 GB+ |
| **GPU** | 2 GB VRAM | 4 GB+ VRAM |
| **Internet** | Required | Required |

### Software Requirements

| Software | Version | Purpose |
|----------|---------|---------|
| **Unity** | 2022 LTS+ | Game engine |
| **Visual Studio** | 2019+ | Code editor |
| **Android SDK** | API 24+ | Android build |
| **Xcode** | 13+ | iOS build |
| **Git** | Latest | Version control |

---

## 🚀 Step-by-Step Installation

### Step 1: Download Project

#### Option A: Download ZIP
```
1. Download HackSlashGame.zip
2. Extract to desired location
3. Remember the path (you'll need it)
```

#### Option B: Clone from Git
```bash
git clone https://github.com/yourusername/HackSlashGame.git
cd HackSlashGame
```

#### Option C: Manual Copy
```
1. Copy all files to a folder
2. Name it "HackSlashGame"
3. Remember the path
```

### Step 2: Install Unity

#### Download Unity
```
1. Go to https://unity.com/download
2. Download Unity 2022 LTS
3. Run installer
4. Follow installation wizard
```

#### Install Components
```
During installation, select:
✅ Unity Editor
✅ Android Build Support
✅ iOS Build Support
✅ Visual Studio Community
```

#### Verify Installation
```
1. Open Unity Hub
2. Check if Unity 2022 LTS is listed
3. Click "Install" if not present
```

### Step 3: Open Project in Unity

#### Method 1: Unity Hub (Recommended)
```
1. Open Unity Hub
2. Click "Open" → "Add project from disk"
3. Navigate to HackSlashGame folder
4. Select the folder
5. Click "Open"
6. Wait for import (5-10 minutes)
```

#### Method 2: Direct Open
```
1. Open Unity 2022 LTS
2. File → Open Project
3. Navigate to HackSlashGame folder
4. Click "Open"
5. Wait for import
```

#### Method 3: Command Line
```bash
# Windows
"C:\Program Files\Unity\Hub\Editor\2022.3.0f1\Editor\Unity.exe" -projectPath "C:\path\to\HackSlashGame"

# macOS
/Applications/Unity/Hub/Editor/2022.3.0f1/Unity.app/Contents/MacOS/Unity -projectPath /path/to/HackSlashGame

# Linux
/opt/Unity/Editor/Unity -projectPath /path/to/HackSlashGame
```

### Step 4: Wait for Import

**What's Happening:**
- Unity is importing all files
- Compiling scripts
- Loading assets
- Building library

**Estimated Time**: 5-10 minutes

**You'll See:**
- Progress bar in bottom right
- "Importing..." messages
- Console messages

**Wait until:**
- Progress bar reaches 100%
- Console shows no errors
- Project opens in editor

### Step 5: Verify Installation

#### Check Console
```
Window → General → Console
Look for errors (red messages)
Should see no errors
```

#### Check Project Structure
```
Assets/
├── Scripts/ (26 files)
├── Resources/
│   └── Data/ (2 JSON files)
└── Tests/ (1 test file)
```

#### Check Scene
```
File → Open Scene
Select any scene
Should load without errors
```

### Step 6: Install Dependencies

#### TextMeshPro
```
If prompted:
Click "Import TextMeshPro Essentials"
Wait for import
```

#### NavMesh
```
If not present:
Window → AI → Navigation
Select ground plane
Click "Bake"
```

### Step 7: Configure Build Settings

#### Android Setup
```
File → Build Settings
Select Android
Player Settings → Android
Set:
- Minimum API: 24
- Target API: 33
```

#### iOS Setup
```
File → Build Settings
Select iOS
Player Settings → iOS
Set:
- Minimum iOS: 14.0
```

---

## ✅ Verification Checklist

After installation, verify:

- [ ] Project opens without errors
- [ ] Console shows no red errors
- [ ] All scripts compile
- [ ] Project structure is intact
- [ ] Assets are loaded
- [ ] Can press Play in editor
- [ ] Game runs without crashing
- [ ] All UI elements visible
- [ ] Player can move
- [ ] Enemies spawn

---

## 🔧 Troubleshooting Installation

### Issue: Project Won't Open

**Solution:**
```
1. Close Unity completely
2. Delete Library folder
3. Reopen project
4. Wait for reimport (10+ minutes)
```

### Issue: Scripts Don't Compile

**Solution:**
```
1. Check Console for errors
2. Fix any syntax errors
3. Wait for recompile
4. If still failing: Delete Library folder
```

### Issue: Missing Dependencies

**Solution:**
```
1. Window → TextMeshPro → Import Essentials
2. Window → AI → Navigation (bake NavMesh)
3. Check Console for other missing items
```

### Issue: Out of Memory

**Solution:**
```
1. Close other applications
2. Increase RAM allocation
3. Try on different computer
4. Delete Library folder and reimport
```

### Issue: Very Slow Import

**Solution:**
```
1. Check internet connection
2. Disable antivirus temporarily
3. Move project to SSD
4. Close other applications
```

---

## 📱 Mobile Setup

### Android Setup

#### Install Android SDK
```
1. Download Android SDK
2. Extract to: C:\Android\sdk (Windows)
3. Set ANDROID_SDK_ROOT environment variable
```

#### Configure in Unity
```
Edit → Preferences → External Tools
Android SDK Path: [path to SDK]
Android NDK Path: [path to NDK]
OpenJDK Path: [path to JDK]
```

#### Build APK
```
File → Build Settings
Select Android
Click "Build"
Wait for compilation
```

### iOS Setup

#### Install Xcode
```
1. Download from App Store
2. Install (requires ~40 GB)
3. Open once to accept license
```

#### Configure in Unity
```
Edit → Preferences → External Tools
Xcode Path: /Applications/Xcode.app
```

#### Build IPA
```
File → Build Settings
Select iOS
Click "Build"
Open in Xcode
Build for device
```

---

## 🎮 First Run

### Launch Game

#### In Editor
```
1. Press Play button (top center)
2. Game should start
3. You should see:
   - Player character (capsule)
   - Enemies (capsules)
   - HUD (health/mana bars)
   - Joystick (bottom left)
```

#### On Device
```
1. Build APK/IPA
2. Install on device
3. Launch app
4. Game should start
```

### Test Controls

```
✓ Move joystick → Player moves
✓ Attack button → Player attacks
✓ Dash button → Player dashes
✓ Enemies move → AI working
✓ Combat works → Enemies take damage
✓ Loot drops → Items appear
✓ HUD updates → UI working
```

---

## 📚 Next Steps After Installation

### Step 1: Read Documentation
```
1. Read MASTER_INDEX.md
2. Choose your path
3. Follow relevant guide
```

### Step 2: Understand Code
```
1. Read PHASE_1_DOCUMENTATION.md
2. Review PlayerController.cs
3. Review EnemyAI.cs
```

### Step 3: Customize Game
```
1. Edit Constants.cs
2. Edit items.json
3. Edit enemies.json
4. Test changes
```

### Step 4: Build for Mobile
```
1. Follow BUILD_CONFIGURATION.md
2. Build for Android
3. Build for iOS
4. Test on device
```

### Step 5: Deploy
```
1. Read DEPLOYMENT_GUIDE.md
2. Create app store listings
3. Submit to stores
4. Monitor performance
```

---

## 🎯 Common Installation Paths

### Path 1: Windows Developer
```
1. Install Unity 2022 LTS (Windows)
2. Extract HackSlashGame.zip
3. Open in Unity
4. Install Android SDK
5. Build APK
```

### Path 2: Mac Developer
```
1. Install Unity 2022 LTS (Mac)
2. Extract HackSlashGame.zip
3. Open in Unity
4. Install Xcode
5. Build IPA
```

### Path 3: Linux Developer
```
1. Install Unity 2022 LTS (Linux)
2. Extract HackSlashGame.zip
3. Open in Unity
4. Install Android SDK
5. Build APK
```

### Path 4: Full Stack (Windows)
```
1. Install Unity 2022 LTS
2. Install Android SDK
3. Install Visual Studio
4. Extract HackSlashGame.zip
5. Open in Unity
6. Build for both platforms
```

---

## 🔒 Security Considerations

### File Permissions
```bash
# Linux/Mac: Ensure execute permissions
chmod +x Assets/Scripts/*.cs
```

### Antivirus
```
Some antivirus software may slow down import
Consider disabling temporarily during import
```

### Network
```
Unity requires internet for:
- License verification
- Asset store
- Cloud services
```

---

## 📊 Installation Verification

### Quick Check
```
✓ Project opens
✓ No console errors
✓ Scripts compile
✓ Can press Play
✓ Game runs
```

### Full Check
```
✓ All 26 scripts present
✓ All 2 JSON files present
✓ All 13 tests present
✓ All 23 documentation files present
✓ No missing dependencies
✓ All systems working
```

---

## 💾 Storage Requirements

| Component | Size |
|-----------|------|
| **Project Files** | 484 KB |
| **Unity Editor** | 3-5 GB |
| **Android SDK** | 5-10 GB |
| **iOS SDK** | 40+ GB |
| **Build Cache** | 2-5 GB |
| **Total** | 50-65 GB |

---

## 🆘 Getting Help

### If Installation Fails

1. **Check Console**
   ```
   Window → General → Console
   Look for error messages
   ```

2. **Read Troubleshooting**
   ```
   See TROUBLESHOOTING_GUIDE.md
   ```

3. **Check Requirements**
   ```
   Verify all requirements met
   ```

4. **Try Clean Install**
   ```
   Delete Library folder
   Reimport project
   ```

5. **Contact Support**
   ```
   Email: support@hackslashgame.com
   ```

---

## ✅ Installation Complete!

When you see this, installation is successful:

```
✅ Project opens in Unity
✅ No console errors
✅ All scripts compile
✅ Can press Play
✅ Game runs without crashing
```

---

## 🎉 What's Next?

1. **Read**: [MASTER_INDEX.md](MASTER_INDEX.md)
2. **Choose**: Your learning path
3. **Start**: Building your game!

---

**Last Updated**: May 23, 2026  
**Version**: 1.0.0  
**Status**: Complete ✅
