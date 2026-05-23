# GitHub + Codemagic Setup Guide

**Complete step-by-step guide to upload project to GitHub and connect with Codemagic for automated builds.**

---

## 📋 Overview

This guide will help you:
1. Upload project to GitHub
2. Connect with Codemagic
3. Automate Android APK builds
4. Automate iOS IPA builds
5. Get notifications on build completion

---

## 🚀 STEP 1: Prepare Project for GitHub

### 1.1 Initialize Git Repository

```bash
cd /home/ubuntu/HackSlashGame

# Initialize git
git init

# Add all files
git add .

# Create initial commit
git commit -m "Initial commit: Hack & Slash Mobile Game v1.0.0"
```

### 1.2 Verify .gitignore

Check that `.gitignore` file exists and contains:
```
Library/
Temp/
Obj/
Build/
Builds/
Logs/
UserSettings/
.vs/
.vscode/
*.apk
*.aab
*.ipa
*.app
.gradle/
.idea/
```

✅ Already created in project!

### 1.3 Check Project Structure

```bash
# Verify key files exist
ls -la Assets/Scripts/Editor/BuildScript.cs
ls -la codemagic.yaml
ls -la .gitignore
```

---

## 🐙 STEP 2: Create GitHub Repository

### 2.1 Create GitHub Account (if needed)

1. Go to https://github.com/signup
2. Create account with email
3. Verify email
4. Complete profile

### 2.2 Create New Repository

1. Go to https://github.com/new
2. Fill in details:
   - **Repository name**: `hack-and-slash-mobile`
   - **Description**: `Hack & Slash Mobile Game - Isometric Action RPG`
   - **Visibility**: Public (for free builds) or Private
   - **Initialize**: Don't select anything (we already have files)
3. Click "Create repository"

### 2.3 Get Repository URL

After creation, you'll see:
```
https://github.com/YOUR_USERNAME/hack-and-slash-mobile.git
```

Copy this URL!

---

## 📤 STEP 3: Push Project to GitHub

### 3.1 Add Remote Repository

```bash
cd /home/ubuntu/HackSlashGame

# Add GitHub remote
git remote add origin https://github.com/YOUR_USERNAME/hack-and-slash-mobile.git

# Verify remote
git remote -v
```

### 3.2 Push to GitHub

```bash
# Push to main branch
git branch -M main
git push -u origin main

# Enter GitHub credentials:
# Username: your-github-username
# Password: your-github-token (or password)
```

### 3.3 Verify Upload

1. Go to https://github.com/YOUR_USERNAME/hack-and-slash-mobile
2. Verify all files are uploaded
3. Check that `.gitignore` is working (no Library/ or Temp/ folders)

---

## 🔧 STEP 4: Setup Codemagic

### 4.1 Create Codemagic Account

1. Go to https://codemagic.io/signup
2. Click "Sign up with GitHub"
3. Authorize Codemagic
4. Complete setup

### 4.2 Connect GitHub Repository

1. Go to https://codemagic.io/apps
2. Click "Add application"
3. Select GitHub
4. Find and select `hack-and-slash-mobile`
5. Click "Create"

### 4.3 Configure Build Settings

**Codemagic will automatically detect `codemagic.yaml`**

If not detected:
1. Go to App Settings
2. Click "Build"
3. Select "Android"
4. Set:
   - **Build script**: `codemagic.yaml`
   - **Output**: `build/outputs/apk/release/HackSlash-*.apk`

---

## 🔐 STEP 5: Setup Android Signing

### 5.1 Create Keystore (if needed)

```bash
# Generate keystore
keytool -genkey -v -keystore hack-slash.keystore \
  -keyalg RSA -keysize 2048 -validity 10000 \
  -alias hack-slash-key \
  -storepass your_password \
  -keypass your_password \
  -dname "CN=Manus,O=Manus,C=US"

# Convert to base64
base64 hack-slash.keystore > hack-slash.keystore.b64
```

### 5.2 Add to Codemagic

1. Go to Codemagic → App Settings → Signing
2. Click "Add Android signing"
3. Upload `hack-slash.keystore.b64`
4. Enter:
   - **Keystore password**: your_password
   - **Key alias**: hack-slash-key
   - **Key password**: your_password
5. Save

---

## 🏗️ STEP 6: Configure Build Workflow

### 6.1 Edit codemagic.yaml

Update the following in `codemagic.yaml`:

```yaml
environment:
  vars:
    UNITY_VERSION: "2022.3.15f1"  # Match your Unity version
```

### 6.2 Set Environment Variables

In Codemagic App Settings → Environment variables:

```
UNITY_VERSION = 2022.3.15f1
ANDROID_SDK_ROOT = /opt/android-sdk
JAVA_HOME = /usr/lib/jvm/java-11-openjdk-amd64
```

### 6.3 Configure Notifications

1. Go to App Settings → Notifications
2. Add email for build notifications
3. Add Slack channel (optional)
4. Save

---

## 🚀 STEP 7: Trigger First Build

### 7.1 Start Build

1. Go to Codemagic → Your App
2. Click "Start new build"
3. Select branch: `main`
4. Select workflow: `android-build`
5. Click "Start build"

### 7.2 Monitor Build

1. Watch build progress in real-time
2. Check logs for any errors
3. Wait for completion (10-20 minutes)

### 7.3 Download APK

When build succeeds:
1. Click "Artifacts"
2. Download `HackSlash-1.0.0.apk`
3. Transfer to Android device
4. Install and test!

---

## 📱 STEP 8: Test on Android Device

### 8.1 Enable Developer Mode

On Android device:
1. Settings → About phone
2. Tap "Build number" 7 times
3. Developer mode enabled

### 8.2 Enable USB Debugging

1. Settings → Developer options
2. Enable "USB debugging"

### 8.3 Install APK

```bash
# Connect device via USB
adb devices

# Install APK
adb install HackSlash-1.0.0.apk

# Launch app
adb shell am start -n space.manus.hackslash/.MainActivity
```

### 8.4 Test Gameplay

- Test joystick movement
- Test attack button
- Test enemy spawning
- Test UI display
- Check performance (FPS)

---

## 🔄 STEP 9: Setup Automatic Builds

### 9.1 Enable Webhooks

In Codemagic:
1. App Settings → Webhooks
2. Enable "Build on push"
3. Select branch: `main`
4. Save

Now builds trigger automatically on every push!

### 9.2 Test Webhook

1. Make a small change to project
2. Commit and push:
   ```bash
   git add .
   git commit -m "Test webhook"
   git push
   ```
3. Check Codemagic - build should start automatically

---

## 📊 STEP 10: Monitor Builds

### 10.1 View Build History

1. Go to Codemagic → Your App
2. View all builds
3. Click build to see details
4. Check logs for errors

### 10.2 Setup Notifications

**Email Notifications**:
- Automatically sent on build success/failure

**Slack Notifications**:
1. Create Slack workspace
2. Create channel: `#builds`
3. Get webhook URL
4. Add to Codemagic

**GitHub Status Checks**:
- Codemagic automatically updates GitHub PR status

---

## ✅ TROUBLESHOOTING

### Build Fails: "Unity not found"

**Solution**:
```yaml
# Update codemagic.yaml
environment:
  vars:
    UNITY_VERSION: "2022.3.15f1"
```

### Build Fails: "Keystore not found"

**Solution**:
1. Upload keystore in Codemagic Settings
2. Update codemagic.yaml with keystore reference

### Build Fails: "Scene not found"

**Solution**:
1. Add scene to Unity Build Settings
2. Verify path in BuildScript.cs
3. Commit and push

### Build Takes Too Long

**Solution**:
- Reduce build timeout in codemagic.yaml
- Disable unused features
- Optimize build settings

---

## 📈 NEXT STEPS

### 1. Setup iOS Builds (Optional)

To build for iOS:
1. Get Apple Developer Account ($99/year)
2. Setup provisioning profiles
3. Configure iOS signing in Codemagic
4. Trigger iOS build

### 2. Setup App Store Publishing

To auto-publish to app stores:
1. Setup Google Play credentials
2. Setup Apple App Store credentials
3. Configure publishing in codemagic.yaml
4. Builds auto-publish to stores

### 3. Setup Testing

To auto-test builds:
1. Add test scripts to codemagic.yaml
2. Run unit tests before build
3. Run integration tests after build
4. Fail build if tests fail

---

## 🎯 SUMMARY

**What you've done**:
- ✅ Uploaded project to GitHub
- ✅ Connected with Codemagic
- ✅ Configured Android builds
- ✅ Setup automatic builds on push
- ✅ Generated first APK
- ✅ Tested on Android device

**You now have**:
- ✅ Automated CI/CD pipeline
- ✅ One-click builds
- ✅ Automatic notifications
- ✅ Version control
- ✅ Build history

---

## 🚀 PUBLISH TO GOOGLE PLAY

When ready to publish:

1. **Create Google Play Account**
   - Go to https://play.google.com/apps/publish
   - Pay $25 registration fee

2. **Create App Listing**
   - App name: "Hack & Slash"
   - Category: Games → Action
   - Content rating: Fill questionnaire

3. **Upload APK**
   - Download from Codemagic
   - Upload to Google Play Console
   - Set version: 1.0.0

4. **Review & Publish**
   - Submit for review
   - Wait 24-48 hours
   - Publish to production

---

**Your game is now ready for automated builds and publication!** 🎮🚀

**Questions?** Check Codemagic docs: https://docs.codemagic.io/
