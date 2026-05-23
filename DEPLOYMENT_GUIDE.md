# Deployment Guide - Hack & Slash Game

Complete guide for deploying the game to Google Play Store and Apple App Store.

---

## Pre-Deployment Checklist

Before submitting to app stores, ensure all items are complete:

| Item | Status |
|------|--------|
| Game builds without errors | ✓ |
| All features tested | ✓ |
| Performance optimized | ✓ |
| No console errors | ✓ |
| Privacy policy prepared | ✓ |
| Screenshots ready | ✓ |
| App icon ready | ✓ |
| Description written | ✓ |

---

## Google Play Store Submission

### Step 1: Create Developer Account

1. Go to [Google Play Console](https://play.google.com/console)
2. Sign in with Google account
3. Pay $25 one-time registration fee
4. Accept agreements
5. Complete profile information

### Step 2: Create App

1. Click "Create app"
2. Enter app name: "Hack & Slash"
3. Select category: Games
4. Select type: Game
5. Click "Create"

### Step 3: Fill App Information

1. **App access**
   - Select "Full game"

2. **Content rating**
   - Click "Content rating questionnaire"
   - Answer questions about game content
   - Get rating (usually PEGI 7 or 12)

3. **Target audience**
   - Select "Teens" or "Mature"
   - Based on content rating

4. **Permissions**
   - Review requested permissions
   - Ensure only necessary permissions

### Step 4: Prepare Store Listing

1. **App name**: "Hack & Slash"

2. **Short description** (80 characters):
   ```
   Fast-paced isometric action RPG with loot, skills, and quests
   ```

3. **Full description** (4000 characters):
   ```
   Hack & Slash is an action-packed isometric RPG where you battle 
   hordes of enemies, collect loot, and grow stronger.
   
   Features:
   - Real-time combat with melee attacks and dodging
   - Loot system with 4 rarity tiers
   - Equipment and stat progression
   - Quest system with daily challenges
   - Game Pass subscription for bonuses
   - Premium cosmetics and battle pass
   
   Battle through procedurally generated dungeons, defeat epic bosses, 
   and become the ultimate warrior!
   ```

4. **Screenshots** (minimum 2, up to 8):
   - Gameplay screenshot
   - Combat screenshot
   - Inventory screenshot
   - Boss fight screenshot
   - Size: 1080x1920 (9:16 aspect ratio)

5. **Feature graphic** (1024x500):
   - Eye-catching promotional image

6. **Icon** (512x512):
   - App icon in PNG format

7. **Category**: Games → Action

8. **Content rating**: [Based on questionnaire]

### Step 5: Set Pricing

1. Click "Pricing & distribution"
2. Select countries (recommended: all)
3. Set price:
   - Free (recommended for launch)
   - Or $0.99 - $199.99
4. Enable in-app purchases
5. Save

### Step 6: Upload APK/AAB

1. Go to "Release" → "Production"
2. Click "Create release"
3. Upload signed APK or AAB file
4. Add release notes:
   ```
   Version 1.0.0 - Launch
   
   - Full game with 5 phases of content
   - 10 items, 5 enemy types
   - Game Pass and premium features
   - Daily quests and progression system
   ```
5. Review and confirm

### Step 7: Submit for Review

1. Review all information
2. Confirm compliance
3. Click "Submit"
4. Wait for review (typically 1-3 hours)

### Step 8: Monitor Review

- Check review status in Console
- Address any issues if rejected
- Resubmit if necessary
- Once approved, app goes live

---

## Apple App Store Submission

### Step 1: Create Developer Account

1. Go to [Apple Developer](https://developer.apple.com/)
2. Sign in with Apple ID
3. Pay $99/year membership fee
4. Complete enrollment
5. Accept agreements

### Step 2: Create App ID

1. Go to Certificates, Identifiers & Profiles
2. Select "Identifiers"
3. Click "+" to create new
4. Select "App IDs"
5. Enter Bundle ID: `com.company.hackslash`
6. Enable required capabilities
7. Register

### Step 3: Create App in App Store Connect

1. Go to [App Store Connect](https://appstoreconnect.apple.com/)
2. Click "My Apps"
3. Click "+"
4. Select "New App"
5. Fill in information:
   - Platform: iOS
   - Name: "Hack & Slash"
   - Bundle ID: Select from list
   - SKU: `hackslash001`
6. Create

### Step 4: Fill App Information

1. **General**
   - Select category: Games
   - Select subcategory: Action
   - Content rating: [Complete questionnaire]

2. **App Information**
   - Name: "Hack & Slash"
   - Subtitle: "Battle, Loot, Conquer"
   - Privacy Policy URL: [Your URL]
   - Support URL: [Your URL]

3. **Pricing & Availability**
   - Price: Free (or select price tier)
   - Availability: All countries
   - Release date: Automatic

### Step 5: Prepare Screenshots

1. **iPhone Screenshots** (required):
   - Minimum 2, maximum 10
   - Size: 1242x2208 or 1125x2436
   - Format: PNG or JPEG
   - Recommended: 5-8 screenshots

2. **iPad Screenshots** (optional):
   - Size: 2048x2732 or 2048x1536
   - Format: PNG or JPEG

3. **App Preview** (optional):
   - 15-30 second video
   - Size: 1242x2208
   - Format: MP4 or MOV

### Step 6: Prepare Metadata

1. **Description**:
   ```
   Hack & Slash is an action-packed isometric RPG where you battle 
   hordes of enemies, collect loot, and grow stronger.
   
   Features:
   - Real-time combat with melee attacks and dodging
   - Loot system with 4 rarity tiers
   - Equipment and stat progression
   - Quest system with daily challenges
   - Game Pass subscription for bonuses
   - Premium cosmetics and battle pass
   ```

2. **Keywords**:
   ```
   action, RPG, hack and slash, dungeon, loot, combat, adventure
   ```

3. **Support URL**: [Your website]

4. **Privacy Policy URL**: [Your privacy policy]

### Step 7: Upload Build

1. Open Xcode project
2. Select Generic iOS Device
3. Product → Archive
4. Organizer → Upload to App Store
5. Sign in with Apple ID
6. Select team
7. Wait for upload to complete

### Step 8: Configure Build

1. In App Store Connect, go to "Builds"
2. Select uploaded build
3. Configure build settings
4. Set as release build

### Step 9: Submit for Review

1. Go to "App Review Information"
2. Fill in contact information
3. Select "Submit for Review"
4. Confirm submission
5. Wait for review (typically 24-48 hours)

### Step 10: Monitor Review

- Check review status in App Store Connect
- Address any issues if rejected
- Resubmit if necessary
- Once approved, app goes live

---

## Post-Launch Monitoring

### Analytics

Track key metrics:
- Downloads
- Daily Active Users (DAU)
- Monthly Active Users (MAU)
- Retention rate
- Session length
- Revenue

### User Feedback

- Monitor app store reviews
- Respond to user feedback
- Fix reported bugs
- Implement feature requests

### Performance

- Monitor crash reports
- Track performance metrics
- Optimize based on data
- Update regularly

### Marketing

- Social media promotion
- Influencer partnerships
- Community engagement
- Content creator program

---

## Update Strategy

### Patch Updates (v1.0.1, v1.0.2)
- Bug fixes
- Performance improvements
- Balance adjustments
- Release every 1-2 weeks

### Minor Updates (v1.1, v1.2)
- New content
- New features
- Quality of life improvements
- Release every 1 month

### Major Updates (v2.0)
- Significant new features
- New game modes
- Major content additions
- Release every 3-6 months

---

## Monetization Setup

### In-App Purchases

1. **Game Pass**
   - Product ID: `gamepass_30days`
   - Price: $4.99
   - Duration: 30 days (auto-renewing)

2. **Premium Chests**
   - Rare: `chest_rare` - $0.99
   - Legendary: `chest_legendary` - $4.99

3. **Premium Currency**
   - 500 gems: `gems_500` - $4.99
   - 1000 gems: `gems_1000` - $9.99

### Setup in App Stores

**Google Play:**
1. Go to Monetization → In-app products
2. Create products with IDs above
3. Set prices
4. Activate

**Apple:**
1. Go to App Store Connect → In-App Purchases
2. Create products with IDs above
3. Set prices
4. Activate

---

## Compliance & Legal

### Privacy Policy

Create privacy policy covering:
- Data collection
- Data usage
- Data sharing
- User rights
- Contact information

Example template: [Privacy Policy Generator](https://www.privacypolicygenerator.info/)

### Terms of Service

Create terms covering:
- User responsibilities
- Prohibited behavior
- Intellectual property
- Limitation of liability
- Dispute resolution

### Age Rating

- PEGI 7: Violence, mild language
- PEGI 12: Moderate violence, language
- PEGI 16: Strong violence, language
- PEGI 18: Extreme violence, language

---

## Troubleshooting

### Google Play Rejection

**Common reasons:**
- Inappropriate content
- Misleading description
- Broken functionality
- Crash on startup
- Missing privacy policy

**Solution:**
- Fix issues
- Resubmit
- Contact support if needed

### Apple Rejection

**Common reasons:**
- Guideline violations
- Broken functionality
- Missing privacy policy
- Misleading screenshots
- Incomplete information

**Solution:**
- Review guidelines
- Fix issues
- Resubmit
- Appeal if necessary

---

## Success Metrics

### Launch Goals (Week 1)
- 10K+ downloads
- 4.5+ star rating
- 1K+ daily active users

### Month 1 Goals
- 100K+ downloads
- 4.5+ star rating
- 10K+ daily active users
- $10K+ revenue

### Year 1 Goals
- 1M+ downloads
- 4.7+ star rating
- 100K+ daily active users
- $100K+ revenue

---

## Resources

- [Google Play Console Help](https://support.google.com/googleplay/android-developer)
- [App Store Connect Help](https://help.apple.com/app-store-connect/)
- [Google Play Policies](https://play.google.com/about/developer-content-policy/)
- [App Store Review Guidelines](https://developer.apple.com/app-store/review/guidelines/)

---

**Ready to launch!** 🚀

**Last Updated**: May 23, 2026  
**Version**: 1.0.0
