# Stage 1: App Store Publication Guide

**Complete guide for publishing Hack & Slash Game on Google Play and Apple App Store.**

---

## 📋 Pre-Publication Checklist

Before submitting to app stores, verify:

- [ ] All scripts compile without errors
- [ ] No console warnings
- [ ] Game runs on test devices
- [ ] Performance: 60 FPS
- [ ] Memory: < 300 MB
- [ ] Crash rate: < 0.1%
- [ ] All features working
- [ ] UI responsive
- [ ] Touch controls working
- [ ] Audio working
- [ ] Monetization tested

---

## 🎮 GOOGLE PLAY STORE PUBLICATION

### Step 1: Create Developer Account

**Cost**: $25 USD (one-time)

```
1. Go to https://play.google.com/console
2. Click "Create account"
3. Accept Google Play Developer Agreement
4. Pay $25 registration fee
5. Complete your developer profile
6. Wait for account activation (usually instant)
```

### Step 2: Create App Listing

```
1. In Google Play Console, click "Create app"
2. Enter app name: "Hack & Slash"
3. Select default language: English
4. Select app category: Games → Action
5. Select content rating: ESRB Teen (13+)
6. Click "Create app"
```

### Step 3: Prepare App Materials

#### Icon (512x512 PNG)
```
Requirements:
- Format: PNG
- Size: 512x512 pixels
- No rounded corners
- No transparency
- Must be square
```

#### Feature Graphic (1024x500 PNG)
```
Requirements:
- Format: PNG
- Size: 1024x500 pixels
- Landscape orientation
- High quality
- Represents app theme
```

#### Screenshots (5-8 images)
```
Requirements:
- Format: PNG or JPEG
- Size: 1080x1920 pixels
- Portrait orientation
- Show key features
- Include HUD, combat, loot
- Add text overlays (optional)

Example Screenshots:
1. Main gameplay with HUD
2. Combat in action
3. Enemy variety
4. Loot and inventory
5. Character progression
6. Boss fight
7. Skill trees
8. Leaderboards
```

#### App Description (4000 characters)
```
Title: "Hack & Slash - Isometric Action RPG"

Description:
"Experience intense isometric action in Hack & Slash, 
a fast-paced action RPG where you battle hordes of enemies, 
collect epic loot, and become a legendary warrior.

FEATURES:
• Smooth isometric gameplay with responsive controls
• Hack and slash combat with satisfying hit feedback
• Hundreds of unique items to collect and equip
• Challenging enemies and epic bosses
• Level progression system (1-100)
• Equipment system with stat bonuses
• Monetization-free core gameplay
• Optimized for mobile devices
• Cross-platform support (Android & iOS)

GAMEPLAY:
- Master your combat skills
- Defeat increasingly challenging enemies
- Collect legendary loot
- Upgrade your equipment
- Progress through 10 story chapters
- Complete daily and weekly quests
- Challenge other players in PvP
- Join guilds and participate in events

STORY:
Embark on an epic journey to save the world from darkness. 
Meet fascinating characters, uncover ancient secrets, 
and become the hero the world needs.

MONETIZATION:
- Free to play
- No energy system
- No pay-to-win mechanics
- Optional cosmetics
- Optional Game Pass for XP boost

Download now and start your adventure!"
```

#### Short Description (80 characters)
```
"Defeat hordes of enemies in this action RPG"
```

### Step 4: Upload APK

```
1. In Google Play Console, go to "Release" → "Production"
2. Click "Create new release"
3. Upload APK file
   - File: HackSlashGame.apk
   - Size: ~50-100 MB
4. Add release notes:
   "Version 1.0.0 - Initial Release
   - Complete isometric action RPG
   - Story mode with 10 chapters
   - Hundreds of items to collect
   - Challenging enemies and bosses
   - Optimized for mobile"
5. Review and confirm
```

### Step 5: Content Rating

```
1. Go to "Content rating"
2. Fill out questionnaire:
   - Violence: Yes (fantasy violence)
   - Blood: No
   - Sexual content: No
   - Language: No
   - Alcohol/Drugs: No
3. Get rating: ESRB Teen (13+)
4. Save rating
```

### Step 6: Privacy Policy

```
1. Create privacy policy (use template)
2. Upload to your website
3. In Google Play Console, add URL to privacy policy
4. Ensure policy covers:
   - Data collection
   - Data usage
   - User rights
   - Contact information
```

### Step 7: Set Pricing

```
1. Go to "Pricing and distribution"
2. Select "Free"
3. Select target countries (all available)
4. Review content rating
5. Accept agreements
```

### Step 8: Review and Publish

```
1. Review all information
2. Check for errors
3. Verify all assets uploaded
4. Click "Submit for review"
5. Wait for approval (24-48 hours)
```

### Approval Timeline

```
Typical timeline:
- Submission: Immediate
- Initial review: 2-4 hours
- Final review: 12-24 hours
- Approval: 24-48 hours
- Live on store: 1-2 hours after approval
```

---

## 🍎 APPLE APP STORE PUBLICATION

### Step 1: Create Developer Account

**Cost**: $99 USD (annual)

```
1. Go to https://developer.apple.com
2. Click "Account" → "Enroll"
3. Sign in with Apple ID
4. Accept Developer Agreement
5. Complete tax information
6. Pay $99 annual fee
7. Wait for account activation (usually instant)
```

### Step 2: Create App in App Store Connect

```
1. Go to https://appstoreconnect.apple.com
2. Click "My Apps"
3. Click "+" → "New App"
4. Select platform: iOS
5. Enter app name: "Hack & Slash"
6. Select bundle ID (e.g., com.hackslash.game)
7. Select SKU (e.g., hackslash001)
8. Select user access level: Full Access
9. Click "Create"
```

### Step 3: Prepare App Materials

#### App Icon (1024x1024 PNG)
```
Requirements:
- Format: PNG
- Size: 1024x1024 pixels
- No rounded corners
- No transparency
- Must be square
```

#### Screenshots (2-5 per device)
```
Requirements:
- Format: PNG or JPEG
- Sizes:
  * iPhone: 1170x2532 pixels
  * iPad: 2048x2732 pixels
- Portrait orientation
- Show key features
- Add text overlays (optional)

Recommended: 5 screenshots per device
```

#### Preview Video (optional)
```
Requirements:
- Format: MP4
- Duration: 15-30 seconds
- Resolution: 1920x1080 or higher
- Shows gameplay highlights
```

#### App Description (4000 characters)
```
Same as Google Play description
```

#### Keywords (100 characters)
```
"action, RPG, hack and slash, isometric, adventure"
```

#### Support URL
```
https://hackslashgame.com/support
```

#### Privacy Policy URL
```
https://hackslashgame.com/privacy
```

### Step 4: Set App Information

```
1. Go to "App Information"
2. Set category: Games → Action
3. Set content rating: 12+
4. Set age rating: 12+
5. Set primary language: English
6. Set bundle ID
```

### Step 5: Set Pricing

```
1. Go to "Pricing and Availability"
2. Select "Free"
3. Select availability date
4. Select territories (all available)
5. Set pricing tier: Free
```

### Step 6: Build and Upload

```
1. In Xcode, select "Generic iOS Device"
2. Product → Archive
3. Organizer window opens
4. Select latest build
5. Click "Distribute App"
6. Select "App Store Connect"
7. Select "Upload"
8. Sign with Apple ID
9. Wait for upload completion
```

### Step 7: Submit for Review

```
1. In App Store Connect, go to "Build"
2. Select latest build
3. Click "Submit for Review"
4. Fill out review information:
   - Sign in required: No
   - Advertising: Yes (optional)
   - IDFA: No
   - Alcohol/Tobacco: No
   - Gambling: No
5. Add review notes:
   "Hack & Slash is an isometric action RPG 
   featuring fast-paced combat, loot collection, 
   and story progression. No external login required."
6. Click "Submit"
```

### Approval Timeline

```
Typical timeline:
- Submission: Immediate
- Initial review: 4-24 hours
- Final review: 12-48 hours
- Approval: 24-72 hours
- Live on store: 1-2 hours after approval
```

---

## 📊 Marketing Materials

### Social Media Posts

#### Twitter
```
"🎮 Hack & Slash is now LIVE on iOS and Android!

Experience intense isometric action, collect epic loot, 
and become a legendary warrior. Download now!

🔗 [Google Play Link]
🔗 [App Store Link]

#HackAndSlash #ActionRPG #MobileGaming"
```

#### Instagram
```
Caption:
"⚔️ HACK & SLASH IS LIVE! ⚔️

Experience the ultimate isometric action RPG. 
Battle hordes of enemies, collect legendary loot, 
and save the world.

Download now on iOS and Android!

#HackAndSlash #ActionRPG #MobileGame #IndieGame"
```

#### Facebook
```
"Hack & Slash is officially LIVE on Google Play and 
Apple App Store! 🎮

Join thousands of players in this epic isometric action RPG. 
Battle enemies, collect loot, and become a legend.

Download FREE now:
- Google Play: [Link]
- App Store: [Link]"
```

### Press Release

```
FOR IMMEDIATE RELEASE

Hack & Slash Action RPG Launches on iOS and Android

[CITY, STATE] – [DATE] – Independent game developer 
announces the launch of Hack & Slash, an isometric action 
RPG for iOS and Android devices.

Hack & Slash delivers fast-paced combat, hundreds of 
collectible items, and an engaging story across 10 chapters. 
Players battle increasingly challenging enemies, collect epic 
loot, and progress through a rich fantasy world.

"We're thrilled to bring Hack & Slash to mobile players," 
said [Developer Name]. "This game represents months of 
development and testing to ensure the best possible 
experience on mobile devices."

Key Features:
- Smooth isometric gameplay
- Hundreds of unique items
- Challenging enemies and bosses
- Story mode with 10 chapters
- Daily and weekly quests
- Optimized for all devices

Hack & Slash is available now on:
- Google Play Store: [Link]
- Apple App Store: [Link]

About [Developer Name]
[Developer Name] is an independent game developer 
focused on creating engaging mobile games.

Contact:
[Email]
[Website]
[Social Media]

###
```

---

## 📈 Launch Day Checklist

### 24 Hours Before Launch
- [ ] Final testing on real devices
- [ ] Verify all links work
- [ ] Check social media posts
- [ ] Notify press contacts
- [ ] Prepare support team

### Launch Day
- [ ] Post on social media
- [ ] Send press release
- [ ] Monitor app store pages
- [ ] Check crash reports
- [ ] Respond to user feedback
- [ ] Monitor analytics

### First Week
- [ ] Track downloads
- [ ] Monitor ratings
- [ ] Fix reported bugs
- [ ] Engage with community
- [ ] Plan updates

---

## 🎯 Success Metrics (First Month)

| Metric | Target |
|--------|--------|
| **Downloads** | 100K+ |
| **Rating** | 4.5+ stars |
| **DAU** | 50K+ |
| **Crash Rate** | < 0.1% |
| **Retention (D1)** | 30%+ |
| **Retention (D7)** | 15%+ |

---

## 🆘 Troubleshooting

### App Rejected on Google Play

**Common Reasons**:
- Crashes on startup
- Inappropriate content
- Misleading description
- Missing privacy policy
- Performance issues

**Solution**:
1. Fix the issue
2. Update APK
3. Resubmit for review

### App Rejected on Apple App Store

**Common Reasons**:
- Crashes or bugs
- Incomplete functionality
- Misleading screenshots
- Missing privacy policy
- Performance issues

**Solution**:
1. Fix the issue
2. Update build
3. Resubmit for review

### Low Downloads

**Solutions**:
1. Improve app store listing
2. Better screenshots
3. More marketing
4. Improve ratings
5. Fix reported issues

---

## 📞 Support Resources

### Google Play
- Developer Console: https://play.google.com/console
- Help Center: https://support.google.com/googleplay
- Community: https://support.google.com/googleplay/community

### Apple App Store
- App Store Connect: https://appstoreconnect.apple.com
- Help Center: https://help.apple.com/app-store-connect
- Community: https://developer.apple.com/forums

---

## ✅ Publication Checklist

### Before Submission
- [ ] All features working
- [ ] No crashes
- [ ] Performance verified
- [ ] All assets prepared
- [ ] Descriptions written
- [ ] Privacy policy ready
- [ ] Screenshots created
- [ ] Icon created

### Google Play
- [ ] Developer account created
- [ ] App listing created
- [ ] APK uploaded
- [ ] Content rating set
- [ ] Privacy policy added
- [ ] Pricing set
- [ ] Submitted for review

### Apple App Store
- [ ] Developer account created
- [ ] App in App Store Connect
- [ ] IPA uploaded
- [ ] Screenshots added
- [ ] Description added
- [ ] Privacy policy added
- [ ] Submitted for review

### Post-Launch
- [ ] Monitor downloads
- [ ] Monitor ratings
- [ ] Monitor crashes
- [ ] Respond to feedback
- [ ] Plan updates

---

## 🚀 Next Steps

1. **Prepare Materials** (Day 1-2)
   - Create icon, screenshots, descriptions
   - Set up developer accounts

2. **Submit to Google Play** (Day 3)
   - Upload APK
   - Submit for review
   - Wait for approval

3. **Submit to Apple App Store** (Day 3)
   - Upload IPA
   - Submit for review
   - Wait for approval

4. **Launch Marketing** (Day 5)
   - Post on social media
   - Send press release
   - Notify community

5. **Monitor and Iterate** (Day 7+)
   - Track metrics
   - Fix issues
   - Plan updates

---

**Last Updated**: May 23, 2026  
**Version**: 1.0.0  
**Status**: Ready for Publication
