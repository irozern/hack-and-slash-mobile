using UnityEngine;
using System.Collections.Generic;
using System;

namespace HackSlash.Quests
{
    /// <summary>
    /// Advanced quest manager handling Daily, Weekly, Story, and Bounty quests.
    /// Manages quest progression, rewards, and tracking.
    /// </summary>
    public class AdvancedQuestManager : MonoBehaviour
    {
        public static AdvancedQuestManager Instance { get; private set; }

        public enum QuestType
        {
            Story,      // Story progression quests
            Daily,      // Reset daily
            Weekly,     // Reset weekly
            Seasonal,   // Seasonal events
            Repeatable, // Unlimited
            Bounty      // Dynamic bounties
        }

        public enum ObjectiveType
        {
            KillEnemies,
            CollectItems,
            ReachLocation,
            DefeatBoss,
            CompleteChallenge,
            GatherResources
        }

        [System.Serializable]
        public class Quest
        {
            public string questId;
            public QuestType questType;
            public string title;
            public string description;
            public List<QuestObjective> objectives = new();
            public QuestReward reward;
            public int requiredLevel;
            public DateTime expiresAt;
            public bool completed;
            public bool claimed;
        }

        [System.Serializable]
        public class QuestObjective
        {
            public string objectiveId;
            public ObjectiveType type;
            public int targetCount;
            public int currentCount;
            public string description;
        }

        [System.Serializable]
        public class QuestReward
        {
            public int xpReward;
            public int goldReward;
            public List<int> itemRewards = new();
            public int premiumCurrencyReward;
        }

        private Dictionary<string, Quest> allQuests = new();
        private List<Quest> activeQuests = new();
        private List<Quest> completedQuests = new();
        private List<Quest> dailyQuests = new();
        private List<Quest> weeklyQuests = new();
        private List<Quest> bounties = new();

        private DateTime lastDailyReset;
        private DateTime lastWeeklyReset;

        void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeQuests();
        }

        void Update()
        {
            CheckDailyReset();
            CheckWeeklyReset();
            UpdateQuestProgress();
        }

        /// <summary>
        /// Initialize all quests.
        /// </summary>
        private void InitializeQuests()
        {
            // Story Quests (50)
            InitializeStoryQuests();
            
            // Daily Quests (30)
            InitializeDailyQuests();
            
            // Weekly Quests (20)
            InitializeWeeklyQuests();
            
            // Bounties (100+)
            InitializeBounties();

            lastDailyReset = DateTime.Now;
            lastWeeklyReset = DateTime.Now;

            Debug.Log("Quests initialized");
        }

        /// <summary>
        /// Initialize story quests (tied to chapters).
        /// </summary>
        private void InitializeStoryQuests()
        {
            // Chapter 1 Quests
            for (int i = 1; i <= 5; i++)
            {
                Quest quest = new Quest
                {
                    questId = $"story_ch1_{i}",
                    questType = QuestType.Story,
                    title = $"Chapter 1 Quest {i}",
                    description = "Progress through Chapter 1",
                    requiredLevel = 1,
                    reward = new QuestReward { xpReward = 100, goldReward = 50 }
                };
                quest.objectives.Add(new QuestObjective
                {
                    objectiveId = $"obj_ch1_{i}",
                    type = ObjectiveType.KillEnemies,
                    targetCount = 10 * i,
                    description = $"Kill {10 * i} enemies"
                });
                allQuests[quest.questId] = quest;
            }

            // Chapter 2-10 Quests (5 per chapter)
            for (int chapter = 2; chapter <= 10; chapter++)
            {
                for (int i = 1; i <= 5; i++)
                {
                    Quest quest = new Quest
                    {
                        questId = $"story_ch{chapter}_{i}",
                        questType = QuestType.Story,
                        title = $"Chapter {chapter} Quest {i}",
                        description = $"Progress through Chapter {chapter}",
                        requiredLevel = chapter * 5,
                        reward = new QuestReward 
                        { 
                            xpReward = 100 * chapter, 
                            goldReward = 50 * chapter 
                        }
                    };
                    quest.objectives.Add(new QuestObjective
                    {
                        objectiveId = $"obj_ch{chapter}_{i}",
                        type = ObjectiveType.KillEnemies,
                        targetCount = 10 * i * chapter,
                        description = $"Kill {10 * i * chapter} enemies"
                    });
                    allQuests[quest.questId] = quest;
                }
            }
        }

        /// <summary>
        /// Initialize daily quests.
        /// </summary>
        private void InitializeDailyQuests()
        {
            string[] dailyTitles = new[]
            {
                "Daily Grind", "Monster Slayer", "Loot Collector", "Boss Hunter", "Survivor",
                "Combat Master", "Speed Runner", "Treasure Seeker", "Enemy Exterminator", "Gold Rush",
                "Item Collector", "Level Up", "Damage Dealer", "Perfect Run", "Endless Battle",
                "Rare Finder", "Boss Slayer", "Combo Master", "Dodge Master", "Critical Strike",
                "Area Clearer", "Wave Survivor", "Elite Hunter", "Legendary Finder", "Streak Builder",
                "Skill Master", "Resource Gatherer", "Challenge Accepted", "Victory Rush", "Ultimate Fighter"
            };

            for (int i = 0; i < 30; i++)
            {
                Quest quest = new Quest
                {
                    questId = $"daily_{i}",
                    questType = QuestType.Daily,
                    title = dailyTitles[i],
                    description = "Complete today's challenge",
                    requiredLevel = 1,
                    expiresAt = DateTime.Now.AddDays(1),
                    reward = new QuestReward 
                    { 
                        xpReward = 200 + (i * 10), 
                        goldReward = 100 + (i * 5),
                        premiumCurrencyReward = i % 5 == 0 ? 10 : 0
                    }
                };

                // Vary objectives
                ObjectiveType[] objectiveTypes = new[] 
                { 
                    ObjectiveType.KillEnemies, 
                    ObjectiveType.CollectItems, 
                    ObjectiveType.DefeatBoss,
                    ObjectiveType.CompleteChallenge
                };

                quest.objectives.Add(new QuestObjective
                {
                    objectiveId = $"daily_obj_{i}",
                    type = objectiveTypes[i % objectiveTypes.Length],
                    targetCount = 10 + (i * 5),
                    description = $"Complete objective ({i + 1}/30)"
                });

                allQuests[quest.questId] = quest;
                dailyQuests.Add(quest);
            }
        }

        /// <summary>
        /// Initialize weekly quests.
        /// </summary>
        private void InitializeWeeklyQuests()
        {
            string[] weeklyTitles = new[]
            {
                "Weekly Champion", "Boss Conqueror", "Legendary Hunter", "Wealth Accumulator", "Master Warrior",
                "Elite Slayer", "Rare Collector", "Challenge Master", "Victory Streak", "Power Leveler",
                "Gold Magnate", "Item Hoarder", "Boss Killer", "Combo Expert", "Survival Expert",
                "Speed Demon", "Damage Master", "Dodge Expert", "Critical Master", "Perfect Warrior",
                "Area Dominator", "Wave Master", "Elite Finder", "Legendary Seeker", "Ultimate Champion"
            };

            for (int i = 0; i < 20; i++)
            {
                Quest quest = new Quest
                {
                    questId = $"weekly_{i}",
                    questType = QuestType.Weekly,
                    title = weeklyTitles[i],
                    description = "Complete this week's challenge",
                    requiredLevel = 1,
                    expiresAt = DateTime.Now.AddDays(7),
                    reward = new QuestReward 
                    { 
                        xpReward = 1000 + (i * 50), 
                        goldReward = 500 + (i * 25),
                        premiumCurrencyReward = 50 + (i * 5)
                    }
                };

                quest.objectives.Add(new QuestObjective
                {
                    objectiveId = $"weekly_obj_{i}",
                    type = ObjectiveType.KillEnemies,
                    targetCount = 100 + (i * 20),
                    description = $"Kill {100 + (i * 20)} enemies"
                });

                allQuests[quest.questId] = quest;
                weeklyQuests.Add(quest);
            }
        }

        /// <summary>
        /// Initialize bounties (dynamic).
        /// </summary>
        private void InitializeBounties()
        {
            string[] bountyNames = new[]
            {
                "Goblin Extermination", "Skeleton Hunting", "Orc Elimination", "Troll Slaying",
                "Demon Hunting", "Wraith Banishing", "Dragon Slaying", "Lich Destruction",
                "Titan Hunting", "Beast Slaying", "Monster Hunting", "Enemy Extermination",
                "Creature Hunting", "Abomination Slaying", "Nightmare Hunting"
            };

            for (int i = 0; i < 100; i++)
            {
                Quest quest = new Quest
                {
                    questId = $"bounty_{i}",
                    questType = QuestType.Bounty,
                    title = bountyNames[i % bountyNames.Length],
                    description = "Complete this bounty for rewards",
                    requiredLevel = 1 + (i / 10),
                    expiresAt = DateTime.Now.AddHours(24),
                    reward = new QuestReward 
                    { 
                        xpReward = 50 + (i * 10), 
                        goldReward = 25 + (i * 5),
                        premiumCurrencyReward = i % 10 == 0 ? 5 : 0
                    }
                };

                quest.objectives.Add(new QuestObjective
                {
                    objectiveId = $"bounty_obj_{i}",
                    type = ObjectiveType.DefeatBoss,
                    targetCount = 1,
                    description = "Defeat the target"
                });

                allQuests[quest.questId] = quest;
                bounties.Add(quest);
            }
        }

        /// <summary>
        /// Accept a quest.
        /// </summary>
        public void AcceptQuest(string questId)
        {
            if (!allQuests.ContainsKey(questId))
            {
                Debug.LogError($"Quest {questId} not found");
                return;
            }

            Quest quest = allQuests[questId];
            if (!activeQuests.Contains(quest))
            {
                activeQuests.Add(quest);
                Debug.Log($"Quest accepted: {quest.title}");
                OnQuestAccepted?.Invoke(quest);
            }
        }

        /// <summary>
        /// Complete a quest.
        /// </summary>
        public void CompleteQuest(string questId)
        {
            if (!allQuests.ContainsKey(questId))
                return;

            Quest quest = allQuests[questId];
            
            // Check if all objectives are complete
            foreach (var objective in quest.objectives)
            {
                if (objective.currentCount < objective.targetCount)
                {
                    Debug.LogWarning($"Quest objective not complete: {objective.description}");
                    return;
                }
            }

            quest.completed = true;
            activeQuests.Remove(quest);
            completedQuests.Add(quest);

            Debug.Log($"Quest completed: {quest.title}");
            OnQuestCompleted?.Invoke(quest);
        }

        /// <summary>
        /// Claim quest rewards.
        /// </summary>
        public void ClaimReward(string questId)
        {
            if (!allQuests.ContainsKey(questId))
                return;

            Quest quest = allQuests[questId];
            if (!quest.completed || quest.claimed)
                return;

            // Award rewards
            PlayerStats.Instance.AddExperience(quest.reward.xpReward);
            PlayerStats.Instance.AddGold(quest.reward.goldReward);

            if (quest.reward.premiumCurrencyReward > 0)
            {
                // Award premium currency
                Debug.Log($"Awarded {quest.reward.premiumCurrencyReward} premium currency");
            }

            quest.claimed = true;
            Debug.Log($"Quest reward claimed: {quest.title}");
            OnRewardClaimed?.Invoke(quest);
        }

        /// <summary>
        /// Update quest progress.
        /// </summary>
        private void UpdateQuestProgress()
        {
            foreach (var quest in activeQuests)
            {
                foreach (var objective in quest.objectives)
                {
                    // Update objective progress based on game events
                    // This would be called from combat system, loot system, etc.
                }
            }
        }

        /// <summary>
        /// Check if daily quests need reset.
        /// </summary>
        private void CheckDailyReset()
        {
            if (DateTime.Now.Day != lastDailyReset.Day)
            {
                ResetDailyQuests();
                lastDailyReset = DateTime.Now;
            }
        }

        /// <summary>
        /// Check if weekly quests need reset.
        /// </summary>
        private void CheckWeeklyReset()
        {
            if (DateTime.Now.AddDays(-DateTime.Now.DayOfWeek + DayOfWeek.Monday).Date != 
                lastWeeklyReset.AddDays(-lastWeeklyReset.DayOfWeek + DayOfWeek.Monday).Date)
            {
                ResetWeeklyQuests();
                lastWeeklyReset = DateTime.Now;
            }
        }

        /// <summary>
        /// Reset daily quests.
        /// </summary>
        private void ResetDailyQuests()
        {
            foreach (var quest in dailyQuests)
            {
                quest.completed = false;
                quest.claimed = false;
                foreach (var objective in quest.objectives)
                {
                    objective.currentCount = 0;
                }
                quest.expiresAt = DateTime.Now.AddDays(1);
            }
            activeQuests.RemoveAll(q => q.questType == QuestType.Daily);
            Debug.Log("Daily quests reset");
            OnDailyQuestsReset?.Invoke();
        }

        /// <summary>
        /// Reset weekly quests.
        /// </summary>
        private void ResetWeeklyQuests()
        {
            foreach (var quest in weeklyQuests)
            {
                quest.completed = false;
                quest.claimed = false;
                foreach (var objective in quest.objectives)
                {
                    objective.currentCount = 0;
                }
                quest.expiresAt = DateTime.Now.AddDays(7);
            }
            activeQuests.RemoveAll(q => q.questType == QuestType.Weekly);
            Debug.Log("Weekly quests reset");
            OnWeeklyQuestsReset?.Invoke();
        }

        /// <summary>
        /// Get all quests of a type.
        /// </summary>
        public List<Quest> GetQuestsByType(QuestType type)
        {
            List<Quest> result = new();
            foreach (var quest in allQuests.Values)
            {
                if (quest.questType == type)
                    result.Add(quest);
            }
            return result;
        }

        /// <summary>
        /// Get active quests.
        /// </summary>
        public List<Quest> GetActiveQuests()
        {
            return new List<Quest>(activeQuests);
        }

        /// <summary>
        /// Get completed quests.
        /// </summary>
        public List<Quest> GetCompletedQuests()
        {
            return new List<Quest>(completedQuests);
        }

        /// <summary>
        /// Get daily quests.
        /// </summary>
        public List<Quest> GetDailyQuests()
        {
            return new List<Quest>(dailyQuests);
        }

        /// <summary>
        /// Get weekly quests.
        /// </summary>
        public List<Quest> GetWeeklyQuests()
        {
            return new List<Quest>(weeklyQuests);
        }

        /// <summary>
        /// Get bounties.
        /// </summary>
        public List<Quest> GetBounties()
        {
            return new List<Quest>(bounties);
        }

        /// <summary>
        /// Update objective progress.
        /// </summary>
        public void UpdateObjective(string questId, string objectiveId, int progress)
        {
            if (!allQuests.ContainsKey(questId))
                return;

            Quest quest = allQuests[questId];
            var objective = quest.objectives.Find(o => o.objectiveId == objectiveId);
            
            if (objective != null)
            {
                objective.currentCount += progress;
                OnObjectiveUpdated?.Invoke(quest, objective);
            }
        }

        // Events
        public event Action<Quest> OnQuestAccepted;
        public event Action<Quest> OnQuestCompleted;
        public event Action<Quest> OnRewardClaimed;
        public event Action<Quest, QuestObjective> OnObjectiveUpdated;
        public event Action OnDailyQuestsReset;
        public event Action OnWeeklyQuestsReset;
    }
}
