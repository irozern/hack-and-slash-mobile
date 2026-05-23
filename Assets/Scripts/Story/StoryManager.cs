using UnityEngine;
using System.Collections.Generic;
using System;

namespace HackSlash.Story
{
    /// <summary>
    /// Manages story progression, chapters, and narrative flow.
    /// Handles story state, chapter progression, and story-related quests.
    /// </summary>
    public class StoryManager : MonoBehaviour
    {
        public static StoryManager Instance { get; private set; }

        [System.Serializable]
        public class StoryChapter
        {
            public int chapterId;
            public string title;
            public string description;
            public int requiredLevel;
            public List<StoryScene> scenes = new();
            public List<int> questIds = new();
            public StoryReward reward;
            public bool completed;
        }

        [System.Serializable]
        public class StoryScene
        {
            public int sceneId;
            public string sceneTitle;
            public string narrative;
            public List<DialogueChoice> choices = new();
            public List<CutsceneData> cutscenes = new();
            public Action onSceneComplete;
        }

        [System.Serializable]
        public class DialogueChoice
        {
            public int choiceId;
            public string choiceText;
            public int nextSceneId;
            public int questRewardId;
        }

        [System.Serializable]
        public class CutsceneData
        {
            public int cutsceneId;
            public string cutsceneName;
            public float duration;
            public string narration;
        }

        [System.Serializable]
        public class StoryReward
        {
            public int xpReward;
            public int goldReward;
            public List<int> itemRewards = new();
        }

        private Dictionary<int, StoryChapter> chapters = new();
        private int currentChapterId = 1;
        private int currentSceneId = 1;
        private bool storyInProgress = false;

        void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeStory();
        }

        /// <summary>
        /// Initialize all story chapters and scenes.
        /// </summary>
        private void InitializeStory()
        {
            // Chapter 1: The Beginning
            chapters[1] = new StoryChapter
            {
                chapterId = 1,
                title = "The Beginning",
                description = "Your journey starts in a small village under attack.",
                requiredLevel = 1,
                reward = new StoryReward { xpReward = 500, goldReward = 100 }
            };

            // Chapter 2: The Village
            chapters[2] = new StoryChapter
            {
                chapterId = 2,
                title = "The Village",
                description = "Explore the village and meet its inhabitants.",
                requiredLevel = 5,
                reward = new StoryReward { xpReward = 1000, goldReward = 250 }
            };

            // Chapter 3: The Forest
            chapters[3] = new StoryChapter
            {
                chapterId = 3,
                title = "The Forest",
                description = "Venture into the dark forest to find answers.",
                requiredLevel = 10,
                reward = new StoryReward { xpReward = 1500, goldReward = 500 }
            };

            // Chapter 4: Ancient Ruins
            chapters[4] = new StoryChapter
            {
                chapterId = 4,
                title = "Ancient Ruins",
                description = "Discover the secrets of the ancient ruins.",
                requiredLevel = 15,
                reward = new StoryReward { xpReward = 2000, goldReward = 750 }
            };

            // Chapter 5: The Caverns
            chapters[5] = new StoryChapter
            {
                chapterId = 5,
                title = "The Caverns",
                description = "Descend into the deep caverns below.",
                requiredLevel = 20,
                reward = new StoryReward { xpReward = 2500, goldReward = 1000 }
            };

            // Chapter 6: Volcanic Wasteland
            chapters[6] = new StoryChapter
            {
                chapterId = 6,
                title = "Volcanic Wasteland",
                description = "Cross the dangerous volcanic wasteland.",
                requiredLevel = 25,
                reward = new StoryReward { xpReward = 3000, goldReward = 1250 }
            };

            // Chapter 7: Frozen Tundra
            chapters[7] = new StoryChapter
            {
                chapterId = 7,
                title = "Frozen Tundra",
                description = "Survive the frozen tundra's harsh conditions.",
                requiredLevel = 30,
                reward = new StoryReward { xpReward = 3500, goldReward = 1500 }
            };

            // Chapter 8: Sky Temple
            chapters[8] = new StoryChapter
            {
                chapterId = 8,
                title = "Sky Temple",
                description = "Reach the mystical sky temple.",
                requiredLevel = 35,
                reward = new StoryReward { xpReward = 4000, goldReward = 2000 }
            };

            // Chapter 9: The Dark Lord's Lair
            chapters[9] = new StoryChapter
            {
                chapterId = 9,
                title = "The Dark Lord's Lair",
                description = "Enter the lair of the dark lord.",
                requiredLevel = 40,
                reward = new StoryReward { xpReward = 5000, goldReward = 2500 }
            };

            // Chapter 10: The Final Battle
            chapters[10] = new StoryChapter
            {
                chapterId = 10,
                title = "The Final Battle",
                description = "Face the final boss and save the world.",
                requiredLevel = 50,
                reward = new StoryReward { xpReward = 10000, goldReward = 5000 }
            };

            Debug.Log("Story initialized with 10 chapters");
        }

        /// <summary>
        /// Start a specific chapter.
        /// </summary>
        public void StartChapter(int chapterId)
        {
            if (!chapters.ContainsKey(chapterId))
            {
                Debug.LogError($"Chapter {chapterId} not found");
                return;
            }

            StoryChapter chapter = chapters[chapterId];
            
            // Check if player meets level requirement
            if (PlayerStats.Instance.Level < chapter.requiredLevel)
            {
                Debug.LogWarning($"Player level {PlayerStats.Instance.Level} is below required level {chapter.requiredLevel}");
                return;
            }

            currentChapterId = chapterId;
            storyInProgress = true;

            Debug.Log($"Starting chapter {chapterId}: {chapter.title}");
            OnChapterStarted?.Invoke(chapter);
        }

        /// <summary>
        /// Progress to the next scene in current chapter.
        /// </summary>
        public void ProgressStory()
        {
            if (!storyInProgress)
            {
                Debug.LogWarning("No story in progress");
                return;
            }

            currentSceneId++;
            Debug.Log($"Progressing to scene {currentSceneId}");
            OnStoryProgressed?.Invoke(currentSceneId);
        }

        /// <summary>
        /// Complete the current chapter.
        /// </summary>
        public void CompleteChapter()
        {
            if (!chapters.ContainsKey(currentChapterId))
                return;

            StoryChapter chapter = chapters[currentChapterId];
            chapter.completed = true;
            storyInProgress = false;

            // Award rewards
            PlayerStats.Instance.AddExperience(chapter.reward.xpReward);
            PlayerStats.Instance.AddGold(chapter.reward.goldReward);

            Debug.Log($"Chapter {currentChapterId} completed!");
            OnChapterCompleted?.Invoke(chapter);
        }

        /// <summary>
        /// Get current chapter.
        /// </summary>
        public StoryChapter GetCurrentChapter()
        {
            if (chapters.ContainsKey(currentChapterId))
                return chapters[currentChapterId];
            return null;
        }

        /// <summary>
        /// Get chapter by ID.
        /// </summary>
        public StoryChapter GetChapter(int chapterId)
        {
            if (chapters.ContainsKey(chapterId))
                return chapters[chapterId];
            return null;
        }

        /// <summary>
        /// Get all chapters.
        /// </summary>
        public List<StoryChapter> GetAllChapters()
        {
            return new List<StoryChapter>(chapters.Values);
        }

        /// <summary>
        /// Get story progress (0-100%).
        /// </summary>
        public float GetStoryProgress()
        {
            int completedChapters = 0;
            foreach (var chapter in chapters.Values)
            {
                if (chapter.completed)
                    completedChapters++;
            }
            return (completedChapters / (float)chapters.Count) * 100f;
        }

        /// <summary>
        /// Check if story is complete.
        /// </summary>
        public bool IsStoryComplete()
        {
            return chapters[10].completed;
        }

        // Events
        public event Action<StoryChapter> OnChapterStarted;
        public event Action<StoryChapter> OnChapterCompleted;
        public event Action<int> OnStoryProgressed;
    }
}
