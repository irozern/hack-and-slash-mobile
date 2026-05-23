# Stage 2: Build Verification & QA Testing Guide

**Complete guide for testing, profiling, and verifying the game build.**

---

## 🎯 Stage 2 Overview

**Duration**: Week 3-4  
**Goal**: Ensure stability, performance, and quality across all devices

### Key Objectives
- ✅ Expand unit tests to 50+
- ✅ Test on 15+ different devices
- ✅ Verify performance (60 FPS, < 300 MB RAM)
- ✅ Fix all critical and high-priority bugs
- ✅ Achieve < 0.1% crash rate
- ✅ Verify all features working

---

## 🧪 EXPANDED UNIT TESTING

### Test Suite Structure

```csharp
public class BuildVerificationTestSuite
{
    // Core Systems (10 tests)
    [TestFixture]
    public class CoreSystemsTests
    {
        [Test] public void TestGameManagerInitialization() { }
        [Test] public void TestInputManagerResponsiveness() { }
        [Test] public void TestCameraFollowsPlayer() { }
        [Test] public void TestSceneLoadingCorrectly() { }
        [Test] public void TestGameStateManagement() { }
        [Test] public void TestTimeScaleChanges() { }
        [Test] public void TestPauseMenuFunctionality() { }
        [Test] public void TestSettingsApplication() { }
        [Test] public void TestAudioInitialization() { }
        [Test] public void TestAnalyticsTracking() { }
    }
    
    // Player System (10 tests)
    [TestFixture]
    public class PlayerSystemTests
    {
        [Test] public void TestPlayerSpawning() { }
        [Test] public void TestPlayerMovement() { }
        [Test] public void TestPlayerRotation() { }
        [Test] public void TestPlayerAttack() { }
        [Test] public void TestPlayerDash() { }
        [Test] public void TestPlayerDamage() { }
        [Test] public void TestPlayerDeath() { }
        [Test] public void TestPlayerRespawn() { }
        [Test] public void TestPlayerLevelUp() { }
        [Test] public void TestPlayerStatCalculation() { }
    }
    
    // Enemy System (10 tests)
    [TestFixture]
    public class EnemySystemTests
    {
        [Test] public void TestEnemySpawning() { }
        [Test] public void TestEnemyPatrol() { }
        [Test] public void TestEnemyChase() { }
        [Test] public void TestEnemyAttack() { }
        [Test] public void TestEnemyDamage() { }
        [Test] public void TestEnemyDeath() { }
        [Test] public void TestEnemyLootDrop() { }
        [Test] public void TestEnemyAIStateTransition() { }
        [Test] public void TestEnemyPathfinding() { }
        [Test] public void TestEnemyDifficulty() { }
    }
    
    // Combat System (10 tests)
    [TestFixture]
    public class CombatSystemTests
    {
        [Test] public void TestDamageCalculation() { }
        [Test] public void TestCriticalHitChance() { }
        [Test] public void TestCriticalMultiplier() { }
        [Test] public void TestKnockback() { }
        [Test] public void TestHitDetection() { }
        [Test] public void TestCooldownManagement() { }
        [Test] public void TestDamageNumbers() { }
        [Test] public void TestHitFlashEffect() { }
        [Test] public void TestAOEAttack() { }
        [Test] public void TestStatusEffects() { }
    }
    
    // Loot System (10 tests)
    [TestFixture]
    public class LootSystemTests
    {
        [Test] public void TestLootGeneration() { }
        [Test] public void TestLootRarity() { }
        [Test] public void TestLootDropPosition() { }
        [Test] public void TestLootPhysics() { }
        [Test] public void TestLootPickup() { }
        [Test] public void TestLootDespawn() { }
        [Test] public void TestItemStacking() { }
        [Test] public void TestItemPersistence() { }
        [Test] public void TestLootRateScaling() { }
        [Test] public void TestLootQuality() { }
    }
}
```

### Running Tests

```bash
# Run all tests
unity -runTests -testPlatform editmode -testResults results.xml

# Run specific test class
unity -runTests -testPlatform editmode -testCategory PlayerSystemTests

# Generate coverage report
unity -runTests -testPlatform editmode -enableCodeCoverage -coverageResultsPath coverage
```

### Test Coverage Goals

| System | Target Coverage |
|--------|-----------------|
| **Core** | 85%+ |
| **Player** | 90%+ |
| **Enemy** | 85%+ |
| **Combat** | 90%+ |
| **Loot** | 85%+ |
| **Overall** | 85%+ |

---

## 📱 DEVICE TESTING

### Test Devices (15+)

#### Android Devices
```
High-End (2 devices):
- Samsung Galaxy S23 (12GB RAM, Snapdragon 8 Gen 2)
- Google Pixel 8 Pro (12GB RAM, Tensor G3)

Mid-Range (5 devices):
- Samsung Galaxy A53 (6GB RAM, Exynos 1280)
- OnePlus 11 (8GB RAM, Snapdragon 8 Gen 1)
- Motorola Edge 40 (8GB RAM, Snapdragon 7 Gen 1)
- Xiaomi 13 (8GB RAM, Snapdragon 8 Gen 2)
- Nothing Phone 2 (8GB RAM, Snapdragon 8+ Gen 1)

Low-End (3 devices):
- Samsung Galaxy A12 (3GB RAM, MediaTek Helio G80)
- Motorola G7 (4GB RAM, Snapdragon 632)
- Redmi 9A (3GB RAM, MediaTek Helio G25)

Tablets (2 devices):
- Samsung Galaxy Tab S8 (8GB RAM)
- iPad Pro 11" (8GB RAM)
```

#### iOS Devices
```
High-End (2 devices):
- iPhone 15 Pro Max (8GB RAM, A17 Pro)
- iPhone 15 Pro (6GB RAM, A17 Pro)

Mid-Range (2 devices):
- iPhone 14 (6GB RAM, A15 Bionic)
- iPhone 13 (4GB RAM, A15 Bionic)

Low-End (1 device):
- iPhone SE (3GB RAM, A13 Bionic)

Tablets (1 device):
- iPad Pro 12.9" (8GB RAM)
```

### Testing Checklist (Per Device)

```
□ Game starts without crashes
□ Main menu displays correctly
□ Player spawns correctly
□ Movement works smoothly
□ Combat system works
□ Enemies spawn and attack
□ Loot drops correctly
□ Inventory opens/closes
□ Equipment works
□ HUD displays correctly
□ Audio plays
□ Touch controls responsive
□ Screen rotation handled
□ Notch/safe area handled
□ No frame drops
□ No stuttering
□ No memory leaks
□ Battery drain acceptable
□ Device doesn't overheat
```

### Device Testing Report Template

```
Device: [Model]
OS: [Version]
RAM: [Amount]
Storage: [Available]
Date: [Date]

Performance:
- FPS: [Average/Min/Max]
- Memory: [Usage]
- Battery: [Drain Rate]
- Temperature: [Max]

Functionality:
- Startup: [Pass/Fail]
- Menu: [Pass/Fail]
- Gameplay: [Pass/Fail]
- Combat: [Pass/Fail]
- Loot: [Pass/Fail]
- Inventory: [Pass/Fail]
- Audio: [Pass/Fail]
- UI: [Pass/Fail]

Issues Found:
1. [Issue]
2. [Issue]
3. [Issue]

Recommendations:
- [Recommendation]
- [Recommendation]
```

---

## ⚡ PERFORMANCE PROFILING

### FPS Profiling

```csharp
public class FPSProfiler : MonoBehaviour
{
    private float[] frameTimes;
    private int frameIndex = 0;
    private const int SAMPLE_SIZE = 300; // 5 seconds at 60 FPS
    
    void Update()
    {
        frameTimes[frameIndex] = Time.deltaTime;
        frameIndex = (frameIndex + 1) % SAMPLE_SIZE;
        
        if (frameIndex == 0)
            LogFPSStats();
    }
    
    void LogFPSStats()
    {
        float avgTime = frameTimes.Average();
        float avgFPS = 1f / avgTime;
        float minFPS = 1f / frameTimes.Max();
        float maxFPS = 1f / frameTimes.Min();
        
        Debug.Log($"FPS Stats - Avg: {avgFPS:F1}, Min: {minFPS:F1}, Max: {maxFPS:F1}");
    }
}
```

### Memory Profiling

```csharp
public class MemoryProfiler : MonoBehaviour
{
    void Update()
    {
        long totalMemory = System.GC.GetTotalMemory(false);
        long managedMemory = totalMemory / (1024 * 1024); // MB
        
        Debug.Log($"Memory Usage: {managedMemory} MB");
        
        if (managedMemory > 300)
            Debug.LogWarning("Memory usage exceeding 300 MB!");
    }
}
```

### Performance Targets

| Metric | Target | Min Acceptable |
|--------|--------|----------------|
| **FPS** | 60 | 30 |
| **Memory** | < 250 MB | < 300 MB |
| **Battery** | 2-3 hours | 1.5 hours |
| **Load Time** | < 5 sec | < 10 sec |
| **Startup Time** | < 3 sec | < 5 sec |

### Profiling Tools

```
Unity Profiler:
- Window → Analysis → Profiler
- Monitor: CPU, Memory, GPU, Audio
- Record gameplay
- Analyze bottlenecks

Android Profiler:
- Android Studio → Profiler
- Monitor: CPU, Memory, Network, Energy
- Identify performance issues

Xcode Instruments:
- Xcode → Product → Profile
- Monitor: CPU, Memory, Disk, Network
- Identify performance issues
```

---

## 🐛 BUG TRACKING & FIXING

### Bug Priority Levels

| Priority | Impact | Fix Time | Example |
|----------|--------|----------|---------|
| **Critical** | Game-breaking | Immediately | Crash on startup |
| **High** | Major feature broken | 24 hours | Combat doesn't work |
| **Medium** | Minor feature broken | 1 week | UI glitch |
| **Low** | Cosmetic issue | 2 weeks | Text typo |

### Bug Report Template

```
Title: [Brief description]

Priority: [Critical/High/Medium/Low]

Device: [Model, OS version]

Steps to Reproduce:
1. [Step 1]
2. [Step 2]
3. [Step 3]

Expected Result:
[What should happen]

Actual Result:
[What actually happens]

Screenshots/Video:
[Attach if possible]

Logs:
[Paste relevant logs]

Notes:
[Additional information]
```

### Bug Fixing Workflow

```
1. Report Bug
   - Create bug report
   - Assign priority
   - Assign to developer

2. Investigate
   - Reproduce issue
   - Identify root cause
   - Document findings

3. Fix
   - Implement fix
   - Test fix
   - Verify fix

4. Verify
   - Test on multiple devices
   - Verify no regressions
   - Close bug

5. Release
   - Include in next build
   - Document in changelog
```

---

## ✅ QA CHECKLIST

### Functionality Testing

```
□ Game starts without crashes
□ Main menu works
□ Settings menu works
□ Pause menu works
□ Game can be resumed
□ Game can be quit
□ Player can move
□ Player can attack
□ Player can dash
□ Enemies spawn
□ Enemies attack
□ Combat works
□ Loot drops
□ Loot can be picked up
□ Inventory works
□ Equipment works
□ Stats display correctly
□ HUD updates correctly
□ Audio plays
□ Music plays
□ UI is responsive
□ Touch controls work
□ No infinite loops
□ No memory leaks
```

### Compatibility Testing

```
□ Tested on 5+ Android devices
□ Tested on 5+ iOS devices
□ Works on different screen sizes
□ Works on different OS versions
□ Works with different RAM amounts
□ Works online and offline
□ Works with permissions granted
□ Works with permissions denied
```

### Performance Testing

```
□ 60 FPS on high-end devices
□ 45+ FPS on mid-range devices
□ 30+ FPS on low-end devices
□ Memory < 300 MB
□ No stuttering
□ No frame drops
□ Smooth animations
□ Fast load times
□ Battery drain acceptable
□ Device doesn't overheat
```

### Security Testing

```
□ No crashes on invalid input
□ No crashes on network errors
□ No crashes on permission denial
□ No data leaks
□ No unauthorized access
□ No SQL injection
□ No XSS vulnerabilities
□ Passwords hashed
□ Tokens encrypted
```

---

## 📊 QA REPORT TEMPLATE

```
QA Report - Build [Version]
Date: [Date]
Tester: [Name]

SUMMARY
Overall Status: [Pass/Fail]
Total Tests: [Number]
Passed: [Number]
Failed: [Number]
Pass Rate: [Percentage]

DEVICES TESTED
- [Device 1]: [Pass/Fail]
- [Device 2]: [Pass/Fail]
- [Device 3]: [Pass/Fail]
...

PERFORMANCE RESULTS
- Average FPS: [Number]
- Memory Usage: [Number] MB
- Load Time: [Number] seconds
- Crash Rate: [Percentage]

BUGS FOUND
Critical: [Number]
High: [Number]
Medium: [Number]
Low: [Number]

CRITICAL ISSUES
1. [Issue]
2. [Issue]

RECOMMENDATIONS
1. [Recommendation]
2. [Recommendation]

SIGN-OFF
Tester: [Name]
Date: [Date]
Status: [Approved/Rejected]
```

---

## 🚀 Release Criteria

Before releasing, verify:

- [ ] All critical bugs fixed
- [ ] All high-priority bugs fixed
- [ ] 50+ unit tests passing
- [ ] 85%+ test coverage
- [ ] Tested on 15+ devices
- [ ] Performance verified (60 FPS)
- [ ] Memory verified (< 300 MB)
- [ ] Crash rate < 0.1%
- [ ] No console errors
- [ ] No console warnings
- [ ] All features working
- [ ] UI responsive
- [ ] Audio working
- [ ] Monetization tested

---

## 📞 Support & Resources

### Testing Tools
- Unity Profiler
- Android Profiler
- Xcode Instruments
- Firebase Crashlytics
- Sentry

### Testing Services
- TestFlight (iOS)
- Google Play Beta
- Firebase Test Lab
- BrowserStack

---

## ✅ Stage 2 Completion Checklist

- [ ] 50+ unit tests created
- [ ] 85%+ test coverage
- [ ] Tested on 15+ devices
- [ ] All critical bugs fixed
- [ ] All high-priority bugs fixed
- [ ] Performance verified
- [ ] Memory verified
- [ ] Crash rate < 0.1%
- [ ] QA report completed
- [ ] Release approved

---

**Last Updated**: May 23, 2026  
**Version**: 1.0.0  
**Status**: Ready for Testing
