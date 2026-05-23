# Stage 4: Quest System Expansion

**Complete guide for implementing Daily, Weekly, Story, and Bounty quest systems.**

---

## 📋 Stage 4 Overview

**Duration**: Month 3  
**Goal**: Implement comprehensive quest system with 100+ quests

### Key Objectives
- ✅ Advanced Quest Manager (Daily, Weekly, Story, Bounty)
- ✅ 50 Story Quests (tied to chapters)
- ✅ 30 Daily Quests (reset daily)
- ✅ 20 Weekly Quests (reset weekly)
- ✅ 100+ Bounties (dynamic)
- ✅ Quest UI and tracking
- ✅ Reward system

---

## 🎯 QUEST TYPES

### 1. Story Quests (50)

**Purpose**: Progress through story chapters  
**Reset**: Never  
**Rewards**: XP, Gold, Items

```
Chapter 1: 5 quests
- Kill 10 enemies
- Kill 20 enemies
- Kill 30 enemies
- Kill 40 enemies
- Kill 50 enemies

Chapter 2-10: 5 quests each
- Scaling difficulty
- Increasing rewards
```

**Progression**:
```
Chapter 1 (Level 1): 500 XP, 100 gold
Chapter 2 (Level 5): 1000 XP, 250 gold
Chapter 3 (Level 10): 1500 XP, 500 gold
...
Chapter 10 (Level 50): 10000 XP, 5000 gold
```

### 2. Daily Quests (30)

**Purpose**: Daily engagement and rewards  
**Reset**: Every 24 hours at midnight  
**Rewards**: XP, Gold, Premium Currency

```
Examples:
- Daily Grind: Kill 50 enemies
- Monster Slayer: Defeat 5 bosses
- Loot Collector: Collect 20 items
- Boss Hunter: Defeat 3 bosses
- Survivor: Complete 10 battles
- Combat Master: Deal 1000 damage
- Speed Runner: Complete level in 5 minutes
- Treasure Seeker: Find 5 rare items
- Enemy Exterminator: Kill 100 enemies
- Gold Rush: Collect 500 gold
```

**Rewards Scale**:
```
Quest 1: 200 XP, 100 gold, 0 premium
Quest 2: 210 XP, 105 gold, 0 premium
...
Quest 5: 250 XP, 125 gold, 10 premium
Quest 10: 300 XP, 150 gold, 10 premium
...
Quest 30: 500 XP, 250 gold, 10 premium
```

### 3. Weekly Quests (20)

**Purpose**: Long-term engagement  
**Reset**: Every Monday at midnight  
**Rewards**: XP, Gold, Premium Currency (higher than daily)

```
Examples:
- Weekly Champion: Kill 500 enemies
- Boss Conqueror: Defeat 20 bosses
- Legendary Hunter: Find 10 legendary items
- Wealth Accumulator: Collect 5000 gold
- Master Warrior: Deal 10000 damage
- Elite Slayer: Defeat 10 elite enemies
- Rare Collector: Find 20 rare items
- Challenge Master: Complete 50 battles
- Victory Streak: Win 30 battles in a row
- Power Leveler: Reach level 50
```

**Rewards Scale**:
```
Quest 1: 1000 XP, 500 gold, 50 premium
Quest 2: 1050 XP, 525 gold, 55 premium
...
Quest 20: 1950 XP, 975 gold, 150 premium
```

### 4. Bounties (100+)

**Purpose**: Dynamic, repeatable content  
**Reset**: Every 24 hours  
**Rewards**: XP, Gold, Premium Currency (scaling with difficulty)

```
Examples:
- Goblin Extermination
- Skeleton Hunting
- Orc Elimination
- Troll Slaying
- Demon Hunting
- Wraith Banishing
- Dragon Slaying
- Lich Destruction
- Titan Hunting
- Beast Slaying
```

**Difficulty Scaling**:
```
Bounty 1-10 (Level 1): 50 XP, 25 gold
Bounty 11-20 (Level 5): 100 XP, 50 gold
Bounty 21-30 (Level 10): 150 XP, 75 gold
...
Bounty 91-100 (Level 50): 500 XP, 250 gold, 5 premium
```

---

## 🔧 QUEST SYSTEM ARCHITECTURE

### Quest Structure

```csharp
public class Quest
{
    public string questId;           // Unique identifier
    public QuestType questType;      // Story, Daily, Weekly, Bounty
    public string title;             // Quest name
    public string description;       // Quest description
    public List<QuestObjective> objectives;  // Quest goals
    public QuestReward reward;       // Rewards
    public int requiredLevel;        // Minimum level
    public DateTime expiresAt;       // Expiration date
    public bool completed;           // Completion status
    public bool claimed;             // Reward claimed
}
```

### Objective Structure

```csharp
public class QuestObjective
{
    public string objectiveId;       // Unique identifier
    public ObjectiveType type;       // Kill, Collect, Reach, Defeat, Challenge
    public int targetCount;          // Goal count
    public int currentCount;         // Current progress
    public string description;       // Objective description
}
```

### Objective Types

```csharp
public enum ObjectiveType
{
    KillEnemies,        // Kill X enemies
    CollectItems,       // Collect X items
    ReachLocation,      // Reach location
    DefeatBoss,         // Defeat boss
    CompleteChallenge,  // Complete challenge
    GatherResources     // Gather resources
}
```

---

## 📊 QUEST PROGRESSION FLOW

```
1. Quest Available
   ↓
2. Player Accepts Quest
   ↓
3. Quest Becomes Active
   ↓
4. Player Completes Objectives
   ↓
5. Quest Marked Complete
   ↓
6. Player Claims Reward
   ↓
7. Reward Awarded
   ↓
8. Quest Archived
```

---

## 🎮 IMPLEMENTATION GUIDE

### Step 1: Initialize Quest Manager

```csharp
void Start()
{
    // Get quest manager instance
    AdvancedQuestManager questManager = AdvancedQuestManager.Instance;
    
    // Get available quests
    List<Quest> dailyQuests = questManager.GetDailyQuests();
    List<Quest> weeklyQuests = questManager.GetWeeklyQuests();
    List<Quest> storyQuests = questManager.GetQuestsByType(QuestType.Story);
    List<Quest> bounties = questManager.GetBounties();
}
```

### Step 2: Accept Quest

```csharp
void AcceptQuest(string questId)
{
    AdvancedQuestManager.Instance.AcceptQuest(questId);
}
```

### Step 3: Track Progress

```csharp
void OnEnemyKilled()
{
    // Update kill objective
    AdvancedQuestManager.Instance.UpdateObjective(
        questId: "daily_0",
        objectiveId: "daily_obj_0",
        progress: 1
    );
}

void OnItemCollected()
{
    // Update collection objective
    AdvancedQuestManager.Instance.UpdateObjective(
        questId: "story_ch1_1",
        objectiveId: "obj_ch1_1",
        progress: 1
    );
}
```

### Step 4: Complete Quest

```csharp
void CompleteQuest(string questId)
{
    AdvancedQuestManager.Instance.CompleteQuest(questId);
}
```

### Step 5: Claim Reward

```csharp
void ClaimReward(string questId)
{
    AdvancedQuestManager.Instance.ClaimReward(questId);
}
```

---

## 📱 QUEST UI IMPLEMENTATION

### Quest List UI

```csharp
public class QuestListUI : MonoBehaviour
{
    [SerializeField] private Transform questContainer;
    [SerializeField] private GameObject questItemPrefab;

    void Start()
    {
        DisplayQuests();
        AdvancedQuestManager.Instance.OnQuestAccepted += RefreshUI;
        AdvancedQuestManager.Instance.OnQuestCompleted += RefreshUI;
    }

    void DisplayQuests()
    {
        List<Quest> activeQuests = AdvancedQuestManager.Instance.GetActiveQuests();
        
        foreach (var quest in activeQuests)
        {
            GameObject questItem = Instantiate(questItemPrefab, questContainer);
            QuestItemUI questUI = questItem.GetComponent<QuestItemUI>();
            questUI.SetQuest(quest);
        }
    }

    void RefreshUI(Quest quest)
    {
        // Refresh UI when quest changes
        questContainer.Clear();
        DisplayQuests();
    }
}
```

### Quest Item UI

```csharp
public class QuestItemUI : MonoBehaviour
{
    [SerializeField] private Text questTitle;
    [SerializeField] private Text questDescription;
    [SerializeField] private Slider progressSlider;
    [SerializeField] private Button acceptButton;
    [SerializeField] private Button completeButton;
    [SerializeField] private Button claimButton;

    private Quest currentQuest;

    void Start()
    {
        acceptButton.onClick.AddListener(OnAccept);
        completeButton.onClick.AddListener(OnComplete);
        claimButton.onClick.AddListener(OnClaim);
    }

    public void SetQuest(Quest quest)
    {
        currentQuest = quest;
        questTitle.text = quest.title;
        questDescription.text = quest.description;
        UpdateProgress();
    }

    void UpdateProgress()
    {
        if (currentQuest.objectives.Count == 0)
            return;

        float totalProgress = 0;
        foreach (var objective in currentQuest.objectives)
        {
            totalProgress += (float)objective.currentCount / objective.targetCount;
        }
        
        progressSlider.value = totalProgress / currentQuest.objectives.Count;
    }

    void OnAccept()
    {
        AdvancedQuestManager.Instance.AcceptQuest(currentQuest.questId);
    }

    void OnComplete()
    {
        AdvancedQuestManager.Instance.CompleteQuest(currentQuest.questId);
    }

    void OnClaim()
    {
        AdvancedQuestManager.Instance.ClaimReward(currentQuest.questId);
    }
}
```

---

## 🎯 QUEST TRACKING

### Track Kills

```csharp
public class EnemyController : MonoBehaviour
{
    void OnDeath()
    {
        // Notify quest system
        AdvancedQuestManager.Instance.UpdateObjective(
            questId: "daily_0",
            objectiveId: "daily_obj_0",
            progress: 1
        );
    }
}
```

### Track Loot

```csharp
public class LootManager : MonoBehaviour
{
    public void PickupItem(Item item)
    {
        // Notify quest system
        AdvancedQuestManager.Instance.UpdateObjective(
            questId: "daily_2",
            objectiveId: "daily_obj_2",
            progress: 1
        );
    }
}
```

### Track Bosses

```csharp
public class BossController : MonoBehaviour
{
    void OnDeath()
    {
        // Notify quest system
        AdvancedQuestManager.Instance.UpdateObjective(
            questId: "weekly_0",
            objectiveId: "weekly_obj_0",
            progress: 1
        );
    }
}
```

---

## 💰 REWARD SYSTEM

### XP Rewards

```
Daily Quest: 200-500 XP
Weekly Quest: 1000-1950 XP
Story Quest: 500-10000 XP
Bounty: 50-500 XP
```

### Gold Rewards

```
Daily Quest: 100-250 gold
Weekly Quest: 500-975 gold
Story Quest: 100-5000 gold
Bounty: 25-250 gold
```

### Premium Currency

```
Daily Quest: 0-10 premium (rare)
Weekly Quest: 50-150 premium
Story Quest: 0 premium
Bounty: 0-5 premium (rare)
```

### Item Rewards

```
Story Quest: 1-3 items
Daily Quest: 0-1 items
Weekly Quest: 1-2 items
Bounty: 0-1 items
```

---

## 📊 QUEST STATISTICS

### Total Quests: 200+

```
Story Quests: 50
Daily Quests: 30
Weekly Quests: 20
Bounties: 100+
```

### Completion Time

```
Story Quest: 5-10 minutes
Daily Quest: 5-15 minutes
Weekly Quest: 30-60 minutes
Bounty: 5-10 minutes
```

### Rewards Per Quest Type

```
Story: 500-10000 XP, 100-5000 gold
Daily: 200-500 XP, 100-250 gold, 0-10 premium
Weekly: 1000-1950 XP, 500-975 gold, 50-150 premium
Bounty: 50-500 XP, 25-250 gold, 0-5 premium
```

---

## ✅ STAGE 4 CHECKLIST

- [ ] AdvancedQuestManager implemented
- [ ] 50 Story Quests created
- [ ] 30 Daily Quests created
- [ ] 20 Weekly Quests created
- [ ] 100+ Bounties created
- [ ] Quest UI created
- [ ] Quest tracking implemented
- [ ] Reward system implemented
- [ ] Daily reset working
- [ ] Weekly reset working
- [ ] All quests tested

---

## 🚀 NEXT STEPS

1. **Implement UI** — Create quest list and tracking UI
2. **Test Quests** — Test all quest types
3. **Balance Rewards** — Adjust rewards for balance
4. **Add Notifications** — Notify player of quest progress
5. **Polish** — Add sounds and effects

---

**Last Updated**: May 23, 2026  
**Version**: 1.0.0  
**Status**: Ready for Implementation
