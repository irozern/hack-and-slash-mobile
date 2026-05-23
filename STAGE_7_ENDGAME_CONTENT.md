# Stage 7: Endgame Content

**Complete guide for implementing endgame systems including PvP, guilds, leaderboards, and seasonal events.**

---

## 📋 Stage 7 Overview

**Duration**: Month 8-12  
**Goal**: Create long-term engagement and community features

### Key Objectives
- ✅ PvP System (Ranked, Casual, Tournament)
- ✅ Guild System (Creation, Wars, Perks)
- ✅ Leaderboard System (8 Categories)
- ✅ Seasonal Events (5 Event Types)
- ✅ Ranking System (8 Ranks)
- ✅ Reward System
- ✅ Social Features

---

## ⚔️ PVP SYSTEM

### Overview

Competitive player-vs-player combat with ranking, matchmaking, and seasonal rewards.

### Ranking System (8 Ranks)

| Rank | Rating Range | Rewards |
|------|--------------|---------|
| Bronze | 0-1000 | 100 Gold, 10 Premium |
| Silver | 1000-1500 | 200 Gold, 20 Premium |
| Gold | 1500-2000 | 300 Gold, 30 Premium |
| Platinum | 2000-2500 | 400 Gold, 40 Premium |
| Diamond | 2500-3000 | 500 Gold, 50 Premium |
| Master | 3000-3500 | 600 Gold, 60 Premium |
| Grandmaster | 3500-4000 | 700 Gold, 70 Premium |
| Legend | 4000-5000 | 1000 Gold, 100 Premium |

### Match Types

**Casual**
- No ranking impact
- Quick matchmaking
- Rewards: 50-200 XP, 25-100 Gold

**Ranked**
- Rating changes
- Skill-based matchmaking
- Rewards: 100-500 XP, 50-250 Gold, 5-25 Premium

**Tournament**
- Bracket-based
- Best-of-3 matches
- Rewards: 500-2000 XP, 250-1000 Gold, 50-200 Premium

### ELO System

```
Base Change: 32 points
Rating Difference Bonus:
- >200 rating difference: 16 points (half)
- <-200 rating difference: 64 points (double)
- Normal: 32 points
```

### Implementation

```csharp
// Find opponent
PvPPlayer opponent = PvPManager.Instance.FindOpponent(playerId);

// Complete match
PvPManager.Instance.CompleteMatch(player1Id, player2Id, winnerId);

// Get leaderboard
List<PvPPlayer> top = PvPManager.Instance.GetLeaderboard(100);

// Claim seasonal rewards
PvPManager.Instance.ClaimSeasonalRewards(playerId);
```

---

## 🏰 GUILD SYSTEM

### Overview

Player-created guilds with shared treasury, perks, and guild wars.

### Guild Features

**Guild Perks** (Upgradeable)
- Experience Boost: +10% XP (upgradeable)
- Gold Boost: +10% Gold (upgradeable)
- Loot Boost: +10% Loot (upgradeable)

**Guild Ranks**
- Member: Basic member
- Officer: Can manage members
- Leader: Full control

**Guild Treasury**
- Shared gold pool
- Used for perk upgrades
- Funded by member contributions

### Guild Wars

```
Duration: 24 hours
Scoring: Based on boss kills, player kills
Rewards: Gold, XP, Premium currency
```

### Implementation

```csharp
// Create guild
Guild guild = GuildManager.Instance.CreateGuild(name, description, leaderId);

// Join guild
GuildManager.Instance.JoinGuild(guildId, playerId);

// Contribute to guild
GuildManager.Instance.ContributeToGuild(guildId, playerId, amount);

// Start guild war
GuildWar war = GuildManager.Instance.StartGuildWar(guild1Id, guild2Id, duration);

// Upgrade perk
GuildManager.Instance.UpgradeGuildPerk(guildId, perkId);
```

---

## 📊 LEADERBOARD SYSTEM

### Overview

8 different leaderboards tracking various player statistics.

### Leaderboard Types

| Type | Metric | Reset |
|------|--------|-------|
| **Level** | Player Level | Never |
| **Experience** | Total XP | Seasonal |
| **PvP Rating** | PvP Rating | Seasonal |
| **Gold** | Total Gold Earned | Seasonal |
| **Boss Kills** | Total Bosses Defeated | Seasonal |
| **Dungeon Clears** | Total Dungeons Completed | Seasonal |
| **Play Time** | Total Playtime | Seasonal |
| **Achievements** | Total Achievements | Seasonal |

### Leaderboard Features

```
Top 100 Players
Player Rankings
Nearby Players (±5 positions)
Statistics (Average, Median, Top)
Seasonal Resets
```

### Implementation

```csharp
// Update player stats
LeaderboardManager.Instance.UpdatePlayerStats(playerId, name, level, xp, rating, gold, bossKills, dungeonClears, playTime, achievements);

// Get leaderboard
List<LeaderboardEntry> top = LeaderboardManager.Instance.GetLeaderboard(LeaderboardType.Level, 100);

// Get player rank
int rank = LeaderboardManager.Instance.GetPlayerRank(LeaderboardType.Level, playerId);

// Get nearby players
List<LeaderboardEntry> nearby = LeaderboardManager.Instance.GetNearbyPlayers(LeaderboardType.Level, playerId, 5);
```

---

## 🎉 SEASONAL EVENTS

### Overview

5 rotating seasonal events with challenges and rewards.

### Event Types

**1. Boss Rush** (Duration: 7 days)
```
Challenges:
- Defeat 5 Bosses (500 XP, 250 Gold, 25 Premium)
- Defeat 10 Bosses (1000 XP, 500 Gold, 50 Premium)
- Defeat 20 Bosses (2000 XP, 1000 Gold, 100 Premium)

Completion Reward: Boss Slayer Cloak
```

**2. Dungeon Challenge** (Duration: 14 days)
```
Challenges:
- Complete Forest Crypt (1000 XP, 500 Gold, 50 Premium)
- Complete All Dungeons (5000 XP, 2500 Gold, 250 Premium)

Completion Reward: Dungeon Master Armor
```

**3. PvP Tournament** (Duration: 7 days)
```
Challenges:
- Win 5 PvP Matches (500 XP, 250 Gold, 50 Premium)
- Win 20 PvP Matches (2000 XP, 1000 Gold, 200 Premium)

Completion Reward: Champion Crown
```

**4. Collection Event** (Duration: 21 days)
```
Challenges:
- Collect 10 Rare Items (1000 XP, 500 Gold, 50 Premium)
- Collect 50 Rare Items (5000 XP, 2500 Gold, 250 Premium)

Completion Reward: Collector's Robe
```

**5. Survival Mode** (Duration: 7 days)
```
Challenges:
- Survive 10 Waves (1000 XP, 500 Gold, 50 Premium)
- Survive 50 Waves (5000 XP, 2500 Gold, 250 Premium)

Completion Reward: Survivor's Armor
```

### Implementation

```csharp
// Join event
SeasonalEventManager.Instance.JoinEvent(playerId, eventId);

// Complete challenge
SeasonalEventManager.Instance.CompleteChallenge(playerId, eventId, challengeId);

// Get active events
List<SeasonalEvent> active = SeasonalEventManager.Instance.GetActiveEvents();

// Start new season
SeasonalEventManager.Instance.StartNewSeason();
```

---

## 📊 PROGRESSION

### Seasonal Cycle

```
Week 1-2: Boss Rush + Dungeon Challenge
Week 2-3: PvP Tournament + Collection Event
Week 3-4: Survival Mode
Week 4: Seasonal Reset
```

### Reward Progression

```
Level 1-20: 50-500 XP per event
Level 20-40: 500-2000 XP per event
Level 40-60: 2000-10000 XP per event
```

### Guild Progression

```
Level 1: 50 members max
Level 2: 75 members max
Level 3: 100 members max
Level 4: 150 members max
Level 5: 200 members max
```

---

## 💰 ECONOMY

### PvP Rewards

```
Casual Win: 50-200 XP, 25-100 Gold
Ranked Win: 100-500 XP, 50-250 Gold, 5-25 Premium
Tournament Win: 500-2000 XP, 250-1000 Gold, 50-200 Premium
```

### Guild Rewards

```
Guild War Win: 1000 XP, 500 Gold, 50 Premium per member
Perk Upgrade: 100-1000 Gold cost
Guild Contribution: 1 point per 100 Gold
```

### Event Rewards

```
Challenge Completion: 500-5000 XP, 250-2500 Gold, 25-250 Premium
Event Completion: 5000-10000 XP, 2500-5000 Gold, 250-500 Premium
```

---

## ✅ STAGE 7 CHECKLIST

- [x] PvPManager implemented
- [x] 8 rank system created
- [x] GuildManager implemented
- [x] Guild war system created
- [x] LeaderboardManager implemented
- [x] 8 leaderboard types created
- [x] SeasonalEventManager implemented
- [x] 5 seasonal events created
- [ ] PvP matchmaking UI (next)
- [ ] Guild UI (next)
- [ ] Leaderboard UI (next)
- [ ] Event UI (next)

---

## 🚀 NEXT STEPS

1. **Create UI** — PvP, Guild, Leaderboard, Event UIs
2. **Add Matchmaking** — Skill-based matchmaking
3. **Add Notifications** — Event and achievement notifications
4. **Add Social** — Friend lists, messaging
5. **Add Analytics** — Track player engagement

---

**Last Updated**: May 23, 2026  
**Version**: 1.0.0  
**Status**: Ready for Implementation
