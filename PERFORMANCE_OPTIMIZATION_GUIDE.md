# Performance Optimization Guide - Hack & Slash Game

Complete guide for optimizing game performance on mobile devices.

---

## 🎯 Performance Targets

| Metric | Target | Priority |
|--------|--------|----------|
| FPS | 60 | Critical |
| Memory | < 300 MB | High |
| Battery | 2-3 hours | High |
| Load Time | < 5 seconds | Medium |
| Network | Minimal | Low |

---

## 📊 Profiling

### Enable Profiler

```
Window → Analysis → Profiler
```

### Key Metrics to Monitor

| Metric | Location | Target |
|--------|----------|--------|
| FPS | Profiler → CPU | 60 |
| Memory | Profiler → Memory | < 300 MB |
| GC Alloc | Profiler → Memory | < 1 MB/frame |
| Draw Calls | Profiler → Rendering | < 100 |
| Batch Count | Profiler → Rendering | > 50% |

### How to Use Profiler

1. **Open Profiler**
   ```
   Window → Analysis → Profiler
   ```

2. **Play Game**
   ```
   Press Play in editor
   Profiler records data
   ```

3. **Analyze Data**
   ```
   Look for spikes
   Identify bottlenecks
   Check memory usage
   ```

4. **Optimize**
   ```
   Fix identified issues
   Retest in profiler
   Verify improvement
   ```

---

## 🎮 Gameplay Optimization

### Enemy Count

**Current**: 5-10 enemies  
**Optimization**: Reduce based on device

```csharp
// In Constants.cs
public static class Enemy
{
    // Adjust based on device performance
    public const int INITIAL_COUNT = 5; // Low-end
    public const int INITIAL_COUNT = 10; // Mid-range
    public const int INITIAL_COUNT = 15; // High-end
}
```

### Spawn Rate

**Current**: Spawn new enemies every 5 seconds  
**Optimization**: Increase spawn delay

```csharp
// In GameManager.cs
private float spawnRate = 5f; // Increase to 7-10 for low-end
```

### Max Enemies

**Current**: No limit  
**Optimization**: Cap max enemies

```csharp
// In GameManager.cs
private const int MAX_ENEMIES = 20; // Adjust as needed

if (enemies.Count < MAX_ENEMIES)
{
    SpawnEnemy();
}
```

### AI Update Rate

**Current**: Every frame  
**Optimization**: Update every N frames

```csharp
// In EnemyAI.cs
private int updateCounter = 0;
private const int UPDATE_INTERVAL = 2; // Update every 2 frames

void Update()
{
    updateCounter++;
    if (updateCounter >= UPDATE_INTERVAL)
    {
        UpdateAI();
        updateCounter = 0;
    }
}
```

---

## 🎨 Graphics Optimization

### Disable Shadows

```csharp
// In CameraController.cs or Start()
Light mainLight = FindObjectOfType<Light>();
mainLight.shadows = LightShadows.None;
```

### Use Simple Materials

```csharp
// Instead of complex shaders, use simple colors
renderer.material = new Material(Shader.Find("Standard"));
renderer.material.color = Color.white;
```

### Batch Rendering

```csharp
// Group objects with same material
// Use static batching for non-moving objects
// Use dynamic batching for moving objects
```

### Reduce Draw Calls

| Technique | Impact | Difficulty |
|-----------|--------|------------|
| Combine meshes | High | Medium |
| Use atlases | High | Medium |
| Reduce objects | Medium | Low |
| Use LOD | Medium | High |

### Texture Optimization

```csharp
// Compress textures
// Use appropriate resolution
// Limit texture count

// In texture import settings:
// Format: RGBA Compressed
// Max Size: 512 or 1024
// Compression: High Quality
```

---

## 💾 Memory Optimization

### Object Pooling

```csharp
// Instead of instantiate/destroy
public class ObjectPool
{
    private Queue<GameObject> pool = new Queue<GameObject>();
    
    public GameObject Get()
    {
        if (pool.Count > 0)
            return pool.Dequeue();
        return Instantiate(prefab);
    }
    
    public void Return(GameObject obj)
    {
        obj.SetActive(false);
        pool.Enqueue(obj);
    }
}
```

### Cache References

```csharp
// Bad: Allocates every frame
void Update()
{
    transform.position += Vector3.forward;
}

// Good: Cache reference
private Transform cachedTransform;

void Start()
{
    cachedTransform = transform;
}

void Update()
{
    cachedTransform.position += Vector3.forward;
}
```

### Avoid Allocations in Update

```csharp
// Bad: Allocates every frame
void Update()
{
    Vector3[] positions = new Vector3[10]; // Allocation!
}

// Good: Allocate once
private Vector3[] positions = new Vector3[10];

void Update()
{
    // Use cached array
}
```

### Unload Unused Assets

```csharp
// Periodically unload unused assets
Resources.UnloadUnusedAssets();
```

### Monitor Memory Usage

```csharp
// Log memory usage
Debug.Log($"Memory: {System.GC.GetTotalMemory(false) / 1024 / 1024} MB");
```

---

## ⚡ Physics Optimization

### Disable Unnecessary Colliders

```csharp
// Remove colliders from visual-only objects
Collider collider = GetComponent<Collider>();
if (collider != null)
    collider.enabled = false;
```

### Use Kinematic Rigidbodies

```csharp
// For non-physics objects
rigidbody.isKinematic = true;
```

### Reduce Physics Update Rate

```csharp
// In Edit → Project Settings → Physics
// Time.fixedDeltaTime = 0.02 (default)
// Increase to 0.033 for lower-end devices
```

### Simplify Collision Shapes

```csharp
// Use simple shapes (Box, Sphere, Capsule)
// Avoid complex mesh colliders
// Use compound colliders instead
```

---

## 🔊 Audio Optimization

### Compress Audio

```csharp
// In audio import settings:
// Compression Format: Vorbis
// Quality: High
// Sample Rate: 44100 Hz
```

### Use Audio Pooling

```csharp
// Reuse audio sources
// Don't create new AudioSources
// Reuse existing ones
```

### Limit Simultaneous Sounds

```csharp
private const int MAX_AUDIO_SOURCES = 8;

if (audioSources.Count < MAX_AUDIO_SOURCES)
{
    PlaySound(clip);
}
```

---

## 🖥️ CPU Optimization

### Cache GetComponent Calls

```csharp
// Bad: Slow
void Update()
{
    GetComponent<Rigidbody>().velocity = Vector3.forward;
}

// Good: Fast
private Rigidbody rb;

void Start()
{
    rb = GetComponent<Rigidbody>();
}

void Update()
{
    rb.velocity = Vector3.forward;
}
```

### Use Coroutines for Delays

```csharp
// Bad: Wastes CPU
void Update()
{
    if (Time.time - lastTime > 1f)
    {
        DoSomething();
        lastTime = Time.time;
    }
}

// Good: Efficient
IEnumerator DoSomethingDelayed()
{
    yield return new WaitForSeconds(1f);
    DoSomething();
}
```

### Batch UI Updates

```csharp
// Bad: Updates every frame
void Update()
{
    healthBar.fillAmount = currentHealth / maxHealth;
}

// Good: Update only when changed
void TakeDamage(float damage)
{
    currentHealth -= damage;
    UpdateHealthBar();
}

void UpdateHealthBar()
{
    healthBar.fillAmount = currentHealth / maxHealth;
}
```

### Optimize AI Calculations

```csharp
// Use spatial partitioning
// Cache distance calculations
// Use simplified pathfinding
// Update AI every N frames
```

---

## 📱 Mobile-Specific Optimizations

### Reduce Screen Resolution

```csharp
// In Start()
Screen.SetResolution(1080, 1920, true);
// Or lower for performance
Screen.SetResolution(720, 1280, true);
```

### Disable VSync

```csharp
// In Start()
QualitySettings.vSyncCount = 0; // Disable VSync
```

### Reduce Frame Rate

```csharp
// For battery saving
Application.targetFrameRate = 30; // Low-end
Application.targetFrameRate = 60; // High-end
```

### Use Quality Settings

```csharp
// In Edit → Project Settings → Quality
// Create profiles for different devices
// Low: Shadows off, simple materials
// Medium: Shadows on, medium quality
// High: All features enabled
```

---

## 🔍 Profiling Checklist

### CPU Profiling

- [ ] Check CPU usage
- [ ] Identify hot spots
- [ ] Look for spikes
- [ ] Check script execution time
- [ ] Optimize bottlenecks

### Memory Profiling

- [ ] Check total memory
- [ ] Look for leaks
- [ ] Monitor allocations
- [ ] Check garbage collection
- [ ] Optimize allocations

### Rendering Profiling

- [ ] Check draw calls
- [ ] Monitor batch count
- [ ] Look for overdraw
- [ ] Check fill rate
- [ ] Optimize rendering

### Physics Profiling

- [ ] Check physics time
- [ ] Monitor collision checks
- [ ] Look for expensive operations
- [ ] Optimize physics updates
- [ ] Reduce rigidbody count

---

## 📊 Before & After Optimization

### Example: Enemy AI Optimization

**Before**:
- 10 enemies
- AI updates every frame
- 45 FPS average
- 250 MB memory

**Optimization**:
- Reduce to 8 enemies
- AI updates every 2 frames
- Cache distance calculations
- Use object pooling

**After**:
- 8 enemies
- AI updates every 2 frames
- 60 FPS average
- 200 MB memory

**Improvement**: +15 FPS, -50 MB memory

---

## 🎯 Optimization Strategy

### Phase 1: Measure
1. Profile current performance
2. Identify bottlenecks
3. Set optimization targets

### Phase 2: Optimize
1. Fix highest impact issues
2. Test improvements
3. Measure results

### Phase 3: Verify
1. Test on target devices
2. Verify performance
3. Check for regressions

### Phase 4: Monitor
1. Continuous profiling
2. Monitor metrics
3. Plan future optimizations

---

## 📋 Optimization Checklist

### Graphics
- [ ] Disable shadows
- [ ] Use simple materials
- [ ] Batch rendering
- [ ] Compress textures
- [ ] Reduce draw calls

### Memory
- [ ] Implement object pooling
- [ ] Cache references
- [ ] Avoid allocations in Update
- [ ] Unload unused assets
- [ ] Monitor memory usage

### Physics
- [ ] Disable unnecessary colliders
- [ ] Use kinematic rigidbodies
- [ ] Reduce physics update rate
- [ ] Simplify collision shapes

### Audio
- [ ] Compress audio
- [ ] Use audio pooling
- [ ] Limit simultaneous sounds

### CPU
- [ ] Cache GetComponent calls
- [ ] Use coroutines
- [ ] Batch UI updates
- [ ] Optimize AI calculations

### Mobile
- [ ] Reduce screen resolution
- [ ] Disable VSync
- [ ] Reduce frame rate
- [ ] Use quality settings

---

## 🚀 Performance Targets by Device

### Low-End Device (1 GB RAM)
- FPS: 30-45
- Memory: < 150 MB
- Enemies: 3-5
- Draw Calls: < 50

### Mid-Range Device (2-3 GB RAM)
- FPS: 45-60
- Memory: < 250 MB
- Enemies: 5-10
- Draw Calls: < 100

### High-End Device (4+ GB RAM)
- FPS: 60
- Memory: < 300 MB
- Enemies: 10-15
- Draw Calls: < 150

---

## 📞 Performance Tips

1. **Profile First** - Measure before optimizing
2. **Focus on Bottlenecks** - Fix biggest issues first
3. **Test on Real Devices** - Editor performance differs
4. **Monitor Continuously** - Keep performance in check
5. **Balance Quality** - Don't sacrifice too much for performance

---

**Last Updated**: May 23, 2026  
**Version**: 1.0.0
