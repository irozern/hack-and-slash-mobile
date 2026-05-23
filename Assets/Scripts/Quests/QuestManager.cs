using UnityEngine;
using System.Collections.Generic;
using System;

/// <summary>
/// Manages quest system with daily, weekly, and seasonal quests.
/// Tracks quest progress and rewards.
/// </summary>
public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance { get; private set; }

    [SerializeField] private List<Quest> activeQuests = new List<Quest>();
    [SerializeField] private List<Quest> completedQuests = new List<Quest>();

    // Events
    public event Action<Quest> OnQuestStarted;
    public event Action<Quest> OnQuestProgress;
    public event Action<Quest> OnQuestCompleted;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        LoadQuestData();
    }

    private void Update()
    {
        // Update quest progress
        foreach (var quest in activeQuests)
        {
            if (!quest.isCompleted)
            {
                UpdateQuestProgress(quest);
            }
        }
    }

    /// <summary>
    /// Starts a new quest.
    /// </summary>
    public void StartQuest(Quest quest)
    {
        if (activeQuests.Contains(quest))
            return;

        activeQuests.Add(quest);
        OnQuestStarted?.Invoke(quest);
        SaveQuestData();
    }

    /// <summary>
    /// Updates quest progress based on player actions.
    /// </summary>
    private void UpdateQuestProgress(Quest quest)
    {
        switch (quest.questType)
        {
            case QuestType.KillEnemies:
                UpdateKillQuestProgress(quest);
                break;
            case QuestType.CollectItems:
                UpdateCollectQuestProgress(quest);
                break;
            case QuestType.ReachLevel:
                UpdateLevelQuestProgress(quest);
                break;
        }
    }

    private void UpdateKillQuestProgress(Quest quest)
    {
        GameObject playerObj = GameManager.Instance.GetPlayer();
        if (playerObj == null) return;

        // This would be called from enemy death events
        // For now, it's a placeholder
    }

    private void UpdateCollectQuestProgress(Quest quest)
    {
        // Track inventory changes
        // This would be called from InventoryManager events
    }

    private void UpdateLevelQuestProgress(Quest quest)
    {
        GameObject playerObj = GameManager.Instance.GetPlayer();
        if (playerObj == null) return;

        PlayerStats playerStats = playerObj.GetComponent<PlayerStats>();
        if (playerStats != null)
        {
            quest.currentProgress = playerStats.GetLevel();
            CheckQuestCompletion(quest);
        }
    }

    /// <summary>
    /// Checks if a quest is completed and applies rewards.
    /// </summary>
    private void CheckQuestCompletion(Quest quest)
    {
        if (quest.currentProgress >= quest.targetProgress && !quest.isCompleted)
        {
            CompleteQuest(quest);
        }
    }

    /// <summary>
    /// Completes a quest and applies rewards.
    /// </summary>
    public void CompleteQuest(Quest quest)
    {
        quest.isCompleted = true;
        activeQuests.Remove(quest);
        completedQuests.Add(quest);

        ApplyQuestRewards(quest);
        OnQuestCompleted?.Invoke(quest);
        SaveQuestData();
    }

    /// <summary>
    /// Applies quest rewards to player.
    /// </summary>
    private void ApplyQuestRewards(Quest quest)
    {
        GameObject playerObj = GameManager.Instance.GetPlayer();
        if (playerObj == null) return;

        PlayerStats playerStats = playerObj.GetComponent<PlayerStats>();
        if (playerStats != null)
        {
            playerStats.AddGold(quest.goldReward);
            playerStats.GainExperience(quest.experienceReward);
        }

        // Add reward items
        if (quest.rewardItems != null)
        {
            foreach (var item in quest.rewardItems)
            {
                if (item != null)
                    InventoryManager.Instance.AddItem(item);
            }
        }

        Debug.Log($"Quest '{quest.questName}' completed! Rewards: {quest.goldReward} gold, {quest.experienceReward} XP");
    }

    /// <summary>
    /// Gets all active quests.
    /// </summary>
    public List<Quest> GetActiveQuests() => activeQuests;

    /// <summary>
    /// Gets all completed quests.
    /// </summary>
    public List<Quest> GetCompletedQuests() => completedQuests;

    // ==================== PERSISTENCE ====================

    private void SaveQuestData()
    {
        // TODO: Implement quest data serialization
    }

    private void LoadQuestData()
    {
        // TODO: Implement quest data deserialization
        // For now, generate default daily quests
        GenerateDailyQuests();
    }

    private void GenerateDailyQuests()
    {
        // Create sample quests
        Quest killQuest = new Quest
        {
            questId = 1,
            questName = "Slay 10 Enemies",
            questType = QuestType.KillEnemies,
            targetProgress = 10,
            goldReward = 100,
            experienceReward = 50
        };

        Quest levelQuest = new Quest
        {
            questId = 2,
            questName = "Reach Level 5",
            questType = QuestType.ReachLevel,
            targetProgress = 5,
            goldReward = 200,
            experienceReward = 100
        };

        StartQuest(killQuest);
        StartQuest(levelQuest);
    }
}

/// <summary>
/// Represents a quest.
/// </summary>
[System.Serializable]
public class Quest
{
    public int questId;
    public string questName;
    public string description;
    public QuestType questType;
    public float currentProgress;
    public float targetProgress;
    public long goldReward;
    public float experienceReward;
    public ItemData[] rewardItems;
    public bool isCompleted;
}

public enum QuestType
{
    KillEnemies,
    CollectItems,
    ReachLevel,
    DefeatBoss,
    Survive
}
