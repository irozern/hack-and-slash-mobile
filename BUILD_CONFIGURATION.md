# Build Configuration Guide

Complete instructions for building the game for Android and iOS.

## Android Build

### Prerequisites

- Android SDK installed
- Android NDK installed
- Java Development Kit (JDK) installed
- Minimum API Level: 24 (Android 7.0)
- Target API Level: 33 (Android 13)

### Step 1: Configure Player Settings

1. File → Build Settings
2. Select **Android** platform
3. Click "Player Settings"

### Step 2: Player Settings - Other

| Setting | Value |
|---------|-------|
| Company Name | Your Company |
| Product Name | Hack & Slash |
| Package Name | com.company.hackslash |
| Version | 1.0.0 |
| Bundle Version Code | 1 |

### Step 3: Player Settings - Resolution

| Setting | Value |
|---------|-------|
| Default Orientation | Portrait |
| Supported Orientations | Portrait only |
| Allow Fullscreen | Checked |

### Step 4: Player Settings - Graphics

| Setting | Value |
|---------|-------|
| Graphics APIs | OpenGL ES 3.0 |
| Multithreaded Rendering | Checked |
| Instancing Variants | Checked |

### Step 5: Player Settings - Android

| Setting | Value |
|---------|-------|
| Minimum API Level | 24 |
| Target API Level | 33 |
| Active Input Handling | Both |
| Internet Access | Required |

### Step 6: Player Settings - Publishing

1. Create a keystore:
   - Click "Create New Keystore"
   - Set password (remember it!)
   - Save location: `Assets/Android/keystore.keystore`

2. Create a key:
   - Click "Create New Key"
   - Alias: `hackslash`
   - Password: (same as keystore)
   - Validity: 25 years
   - First and Last Name: Your Name

### Step 7: Build

1. File → Build Settings
2. Select Android
3. Click "Build" or "Build and Run"
4. Choose output location
5. Wait for build to complete

### Step 8: Test on Device

```bash
# If using "Build and Run"
# Device will automatically install and launch

# Or manually install APK:
adb install -r output.apk
```

---

## iOS Build

### Prerequisites

- Xcode installed (latest version)
- Apple Developer Account
- iOS deployment target: 14.0 or later
- Minimum iPhone 6s or later

### Step 1: Configure Player Settings

1. File → Build Settings
2. Select **iOS** platform
3. Click "Player Settings"

### Step 2: Player Settings - Other

| Setting | Value |
|---------|-------|
| Company Name | Your Company |
| Product Name | Hack & Slash |
| Bundle ID | com.company.hackslash |
| Version | 1.0.0 |
| Build | 1 |

### Step 3: Player Settings - Resolution

| Setting | Value |
|---------|-------|
| Default Orientation | Portrait |
| Supported Orientations | Portrait only |
| Allow Fullscreen | Checked |

### Step 4: Player Settings - Graphics

| Setting | Value |
|---------|-------|
| Graphics APIs | Metal |
| Multithreaded Rendering | Checked |

### Step 5: Player Settings - iOS

| Setting | Value |
|---------|-------|
| Minimum iOS Version | 14.0 |
| Target Device | iPhone |
| Requires ARKit Support | Unchecked |

### Step 6: Player Settings - Publishing

1. Signing Team ID:
   - Get from Apple Developer Account
   - Format: `XXXXXXXXXX` (10 characters)

2. Provisioning Profile:
   - Create in Apple Developer Account
   - Download and install

### Step 7: Build

1. File → Build Settings
2. Select iOS
3. Click "Build"
4. Choose output location
5. Wait for build to complete

### Step 8: Open in Xcode

1. Open generated Xcode project
2. Select target device/simulator
3. Click Play button to build and run

### Step 9: Configure Signing (if needed)

1. Select project in Xcode
2. Select target
3. Go to Signing & Capabilities
4. Select team
5. Fix any issues

### Step 10: Build and Run

1. Product → Build
2. Product → Run
3. App will install and launch on device

---

## Google Play Store Submission

### Prerequisites

- Google Play Developer Account ($25 one-time fee)
- Signed APK or AAB file
- App icon (512x512 PNG)
- Screenshots (at least 2)
- Description and privacy policy

### Step 1: Create App Listing

1. Go to Google Play Console
2. Click "Create App"
3. Fill in app name and language
4. Accept agreements

### Step 2: Fill Store Listing

1. App name
2. Short description (80 characters)
3. Full description (4000 characters)
4. Category: Games
5. Content rating: Fill questionnaire
6. Target audience: Teens

### Step 3: Upload APK/AAB

1. Go to Release → Production
2. Click "Create Release"
3. Upload signed APK or AAB
4. Review and confirm

### Step 4: Set Price

1. Go to Pricing & Distribution
2. Set price (free or paid)
3. Select countries

### Step 5: Submit for Review

1. Review all information
2. Click "Submit"
3. Wait for review (typically 1-3 hours)

---

## Apple App Store Submission

### Prerequisites

- Apple Developer Account ($99/year)
- Signed IPA file
- App icon (1024x1024 PNG)
- Screenshots (at least 2 per device)
- Description and privacy policy

### Step 1: Create App ID

1. Go to Apple Developer Account
2. Certificates, Identifiers & Profiles
3. Create new App ID
4. Bundle ID: `com.company.hackslash`

### Step 2: Create App in App Store Connect

1. Go to App Store Connect
2. Click "My Apps"
3. Click "+"
4. Select "New App"
5. Fill in app information

### Step 3: Fill App Information

1. App Name
2. Subtitle
3. Description
4. Keywords
5. Support URL
6. Privacy Policy URL

### Step 4: Set Pricing

1. Go to Pricing and Availability
2. Set price (free or paid)
3. Select countries

### Step 5: Upload Build

1. Go to TestFlight
2. Click "Build"
3. Upload IPA file using Xcode
4. Wait for processing

### Step 6: Submit for Review

1. Go to Version Release
2. Fill in release notes
3. Select build
4. Click "Submit for Review"
5. Wait for review (typically 24-48 hours)

---

## Performance Optimization for Mobile

### Graphics

```csharp
// In Player Settings:
// - Graphics APIs: OpenGL ES 3.0 (Android) or Metal (iOS)
// - Multithreaded Rendering: Enabled
// - GPU Instancing: Enabled
// - Batching: Enabled
```

### Physics

```csharp
// Use simplified physics
// - Disable unnecessary colliders
// - Use kinematic rigidbodies where possible
// - Limit physics update rate
```

### Memory

```csharp
// Target: <300 MB RAM
// - Use object pooling
// - Unload unused assets
// - Compress textures
// - Limit active enemies to 50
```

### CPU

```csharp
// Target: 60 FPS
// - Use NavMesh for pathfinding
// - Batch UI updates
// - Cache component references
// - Use coroutines for delayed actions
```

---

## Testing on Real Devices

### Android Testing

```bash
# Connect device via USB
# Enable Developer Mode on device

# List connected devices
adb devices

# Install APK
adb install -r app.apk

# View logs
adb logcat

# Run specific app
adb shell am start -n com.company.hackslash/.MainActivity
```

### iOS Testing

1. Connect iPhone via USB
2. Open Xcode project
3. Select device in top-left
4. Click Play button
5. App will build and run

---

## Troubleshooting

### Android Build Fails

**Error: "Unable to find SDK"**
- Set Android SDK path in Preferences → External Tools

**Error: "Gradle build failed"**
- Update Gradle version
- Check Java version (JDK 11 recommended)

### iOS Build Fails

**Error: "No provisioning profile found"**
- Create provisioning profile in Apple Developer Account
- Download and install

**Error: "Code signing required"**
- Select team in Xcode
- Verify signing certificate

### App Crashes on Launch

**Android:**
- Check logcat: `adb logcat`
- Look for NullReferenceException
- Verify all managers are in scene

**iOS:**
- Check Xcode console
- Look for crash logs
- Verify all components are assigned

---

## Release Checklist

Before submitting to app stores:

- [ ] All scripts compile without errors
- [ ] All UI elements are visible and responsive
- [ ] Game runs at 60 FPS on target devices
- [ ] Memory usage is below 300 MB
- [ ] No console errors or warnings
- [ ] All features are working
- [ ] Game is tested on real devices
- [ ] Icons and screenshots are ready
- [ ] Privacy policy is prepared
- [ ] Description and keywords are set
- [ ] Price is set
- [ ] Countries are selected

---

## Post-Launch

### Monitor Performance

- Use Firebase Analytics
- Monitor crash reports
- Track user retention
- Analyze gameplay metrics

### Update Strategy

- Monthly content updates
- Seasonal events
- Bug fixes and optimization
- New features based on feedback

### Marketing

- Social media promotion
- Influencer partnerships
- App store optimization
- Community engagement

---

**Ready to launch!** 🚀
