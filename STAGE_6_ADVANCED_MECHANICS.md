# Stage 6: Advanced Mechanics

**Complete guide for implementing advanced game mechanics including skill trees, talent systems, dungeons, and boss battles.**

---

## 📋 Stage 6 Overview

**Duration**: Month 6-7  
**Goal**: Add depth and complexity to gameplay

### Key Objectives
- ✅ 3 Skill Trees (Warrior, Ranger, Mage)
- ✅ 30 Unique Skills
- ✅ 5 Dungeons (5-15 floors each)
- ✅ 5 Boss Encounters
- ✅ Talent System
- ✅ Skill Point Progression
- ✅ Dungeon Rewards

---

## 🌳 SKILL TREE SYSTEM

### Overview

Three distinct skill trees, each with 10 unique skills:

| Tree | Focus | Playstyle |
|------|-------|-----------|
| **Warrior** | Melee Combat | Tank/Damage |
| **Ranger** | Speed & Ranged | Agility/Evasion |
| **Mage** | Magic & Control | AoE/Crowd Control |

### Warrior Tree (10 Skills)

**Tier 1**
1. **Power Strike** (Lvl 1) — +20% Damage
2. **Iron Skin** (Lvl 5) — +15% Armor
3. **Whirlwind Attack** (Lvl 10) — AOE attack, +30% Damage

**Tier 2**
4. **Berserker Rage** (Lvl 15) — +50% Damage for 10s
5. **Fortress** (Lvl 20) — -30% Damage taken
6. **Cleave** (Lvl 25) — Multi-target strike, +40% Damage

**Tier 3**
7. **Last Stand** (Lvl 30) — Survive lethal damage once
8. **Shockwave** (Lvl 35) — Knockback all enemies
9. **Immortal** (Lvl 40) — Immunity for 5s
10. **God of War** (Lvl 50) — Ultimate: +100% DMG, +50% ARM

### Ranger Tree (10 Skills)

**Tier 1**
1. **Quick Shot** (Lvl 1) — +20% Attack Speed
2. **Evasion** (Lvl 5) — +15% Dodge Chance
3. **Multi Shot** (Lvl 10) — Fire multiple arrows, +25% Damage

**Tier 2**
4. **Piercing Shot** (Lvl 15) — Arrows pierce, +35% Damage
5. **Shadow Clone** (Lvl 20) — Create clone to fight
6. **Ricochet** (Lvl 25) — Arrows bounce, +28% Damage

**Tier 3**
7. **Blink** (Lvl 30) — Teleport short distance
8. **Explosive Arrow** (Lvl 35) — Arrows explode, +40% Damage
9. **Phantom** (Lvl 40) — Invisibility for 8s
10. **Deadeye** (Lvl 50) — Ultimate: +80% DMG, +40% SPD

### Mage Tree (10 Skills)

**Tier 1**
1. **Fireball** (Lvl 1) — Area fire damage, +25% Damage
2. **Frost Bolt** (Lvl 5) — Freeze enemies, +20% Damage
3. **Lightning Storm** (Lvl 10) — Chain lightning, +35% Damage

**Tier 2**
4. **Meteor** (Lvl 15) — Call meteors, +50% Damage
5. **Mana Shield** (Lvl 20) — Absorb damage with mana
6. **Time Warp** (Lvl 25) — Slow time for 5s

**Tier 3**
7. **Teleport** (Lvl 30) — Teleport anywhere
8. **Inferno** (Lvl 35) — Massive fire explosion, +60% Damage
9. **Arcane Mastery** (Lvl 40) — Master all magic
10. **Archmage** (Lvl 50) — Ultimate: +70% DMG, +80% Mana

### Skill Point System

```
Gain 1 Skill Point per Level
Warrior Skills: 1-3 points each
Ranger Skills: 1-3 points each
Mage Skills: 1-3 points each

Total Available: 50 points at Level 50
```

### Implementation

```csharp
// Unlock a skill
SkillTreeManager.Instance.UnlockSkill(skillId);

// Get available skill points
int points = SkillTreeManager.Instance.GetAvailableSkillPoints();

// Get all unlocked skills
List<Skill> unlocked = SkillTreeManager.Instance.GetUnlockedSkills();

// Get skills for a tree
List<Skill> warrior = SkillTreeManager.Instance.GetSkillsForTree(SkillTreePath.Warrior);
```

---

## 🏰 DUNGEON SYSTEM

### Overview

5 dungeons with 5-15 floors each, progressive difficulty, and boss encounters.

### Dungeons

| Dungeon | Floors | Min Lvl | Max Lvl | Difficulty | Reward |
|---------|--------|---------|---------|------------|--------|
| **Forest Crypt** | 5 | 10 | 20 | 1.5x | 1000 XP, 500 G |
| **Cavern of Shadows** | 7 | 20 | 30 | 2.0x | 2000 XP, 1000 G |
| **Volcanic Depths** | 10 | 30 | 40 | 2.5x | 3000 XP, 1500 G |
| **Frozen Abyss** | 12 | 40 | 50 | 3.0x | 4000 XP, 2000 G |
| **Divine Tower** | 15 | 50 | 60 | 3.5x | 5000 XP, 2500 G |

### Boss Encounters

| Boss | Dungeon | HP | DMG | Armor | Special Attacks |
|------|---------|----|----|-------|-----------------|
| **Forest Guardian** | Forest Crypt | 200 | 20 | 0.3 | Root Attack, Summon |
| **Shadow Lord** | Cavern | 300 | 30 | 0.4 | Shadow Bolt, Clone |
| **Magma Lord** | Volcanic | 400 | 40 | 0.5 | Lava Burst, Inferno |
| **Frost King** | Frozen | 500 | 45 | 0.6 | Blizzard, Freeze |
| **Sky Deity** | Divine | 600 | 50 | 0.7 | Divine Wrath, Beam |

### Dungeon Progression

```
Floor 1-3: Regular enemies (5-9 per floor)
Floor 4-7: Harder enemies (10-15 per floor)
Floor 8-12: Elite enemies (15-20 per floor)
Final Floor: Boss encounter (1 boss)

Difficulty: +10-15% per floor
```

### Rewards

```
Per Floor: 100-500 XP, 50-250 Gold
Boss Floor: 500-1000 XP, 250-500 Gold, Rare Item
Dungeon Complete: 1000-5000 XP, 500-2500 Gold, 50-250 Premium
```

### Implementation

```csharp
// Start a dungeon
DungeonManager.Instance.StartDungeon(dungeonId);

// Complete a floor
DungeonManager.Instance.CompleteFloor();

// Get current dungeon
Dungeon current = DungeonManager.Instance.GetCurrentDungeon();

// Get available dungeons
List<Dungeon> available = DungeonManager.Instance.GetAvailableDungeons();

// Check if in dungeon
bool inDungeon = DungeonManager.Instance.IsDungeonInProgress();
```

---

## 🎯 TALENT SYSTEM

### Overview

Passive talents that provide permanent bonuses.

### Talent Categories

**Combat Talents**
- Sharpness: +5% Crit Chance
- Bloodlust: +10% Damage vs low HP enemies
- Executioner: +50% Damage on finishing blow

**Defense Talents**
- Resilience: +10% Armor
- Regeneration: +1% HP per second
- Fortitude: +15% Max HP

**Utility Talents**
- Swiftness: +10% Movement Speed
- Perception: +15% Loot drop rate
- Prosperity: +20% Gold gain

### Implementation

```csharp
// Unlock a talent
TalentManager.Instance.UnlockTalent(talentId);

// Get active talents
List<Talent> active = TalentManager.Instance.GetActiveTalents();

// Get talent bonus
float bonus = TalentManager.Instance.GetTalentBonus("Sharpness");
```

---

## 📊 PROGRESSION

### Skill Point Gain

```
Level 1: 1 point
Level 5: 5 points total
Level 10: 10 points total
Level 20: 20 points total
Level 30: 30 points total
Level 40: 40 points total
Level 50: 50 points total
```

### Dungeon Unlock

```
Level 10: Forest Crypt
Level 20: Cavern of Shadows
Level 30: Volcanic Depths
Level 40: Frozen Abyss
Level 50: Divine Tower
```

### Stat Scaling

```
Warrior: +2 Armor per skill
Ranger: +1 Speed per skill
Mage: +5 Mana per skill
```

---

## ✅ STAGE 6 CHECKLIST

- [x] SkillTreeManager implemented
- [x] 3 skill trees created (30 skills)
- [x] DungeonManager implemented
- [x] 5 dungeons created (50 floors total)
- [x] 5 bosses created
- [ ] Talent system (next)
- [ ] Boss AI (next)
- [ ] Skill animations (next)
- [ ] Dungeon UI (next)
- [ ] Difficulty scaling (next)

---

## 🚀 NEXT STEPS

1. **Implement Talent System** — Add passive talents
2. **Create Boss AI** — Implement boss behaviors
3. **Add Animations** — Skill and boss animations
4. **Create UI** — Skill tree and dungeon UI
5. **Balance Difficulty** — Adjust dungeon difficulty

---

**Last Updated**: May 23, 2026  
**Version**: 1.0.0  
**Status**: Ready for Implementation
