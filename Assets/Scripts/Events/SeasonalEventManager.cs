using UnityEngine;
using System.Collections.Generic;
using System;

namespace HackSlash.Events
{
    /// <summary>
    /// Manages seasonal events, limited-time challenges, and event rewards.
    /// Handles event progression, rewards, and seasonal content.
    /// </summary>
    public class SeasonalEventManager : MonoBehaviour
    {
        public static SeasonalEventManager Instance { get; private set; }

        public enum EventType
        {
            BossRush,
            DungeonChallenge,
            PvPTournament,
            CollectionEvent,
            SurvivalMode,
            SpecialQuest
        }

        public enum EventStatus
        {
            Upcoming,
            Active,
            Completed,
            Archived
        }

        [System.Serializable]
        public class SeasonalEvent
        {
            public int eventId;
            public string eventName;
            public string description;
            public EventType eventType;
            public EventStatus status;
            public DateTime startDate;
            public DateTime endDate;
            public int minLevel;
            public List<EventChallenge> challenges = new();
            public EventReward reward;
            public int participantCount;
        }

        [System.Serializable]
        public class EventChallenge
        {
            public int challengeId;
            public string challengeName;
            public string description;
            public int targetValue;
            public int currentProgress;
            public bool completed;
            public EventChallengeReward reward;
        }

        [System.Serializable]
        public class EventChallengeReward
        {
            public int xpReward;
            public int goldReward;
            public int premiumReward;
            public List<int> itemRewards = new();
        }

        [System.Serializable]
        public class EventReward
        {
            public int rewardId;
            public string rewardName;
            public int xpReward;
            public int goldReward;
            public int premiumReward;
            public List<int> itemRewards = new();
            public string cosmetic;
        }

        [System.Serializable]
        public class PlayerEventProgress
        {
            public int playerId;
            public int eventId;
            public List<int> completedChallenges = new();
            public int totalProgress;
            public bool eventCompleted;
            public DateTime joinDate;
        }

        private Dictionary<int, SeasonalEvent> events = new();
        private Dictionary<int, PlayerEventProgress> playerProgress = new();
        private int eventIdCounter = 1;
        private int currentSeason = 1;

        void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeSeasonalEvents();
        }

        /// <summary>
        /// Initialize seasonal events.
        /// </summary>
        private void InitializeSeasonalEvents()
        {
            CreateSeasonalEvents();
            Debug.Log("Seasonal events initialized");
        }

        /// <summary>
        /// Create seasonal events for the current season.
        /// </summary>
        private void CreateSeasonalEvents()
        {
            // EVENT 1: Boss Rush
            SeasonalEvent bossRush = new SeasonalEvent
            {
                eventId = eventIdCounter++,
                eventName = "Boss Rush",
                description = "Defeat as many bosses as possible in 30 minutes",
                eventType = EventType.BossRush,
                status = EventStatus.Active,
                startDate = DateTime.Now,
                endDate = DateTime.Now.AddDays(7),
                minLevel = 20
            };

            bossRush.challenges.Add(new EventChallenge
            {
                challengeId = 1,
                challengeName = "Defeat 5 Bosses",
                targetValue = 5,
                reward = new EventChallengeReward { xpReward = 500, goldReward = 250, premiumReward = 25 }
            });

            bossRush.challenges.Add(new EventChallenge
            {
                challengeId = 2,
                challengeName = "Defeat 10 Bosses",
                targetValue = 10,
                reward = new EventChallengeReward { xpReward = 1000, goldReward = 500, premiumReward = 50 }
            });

            bossRush.challenges.Add(new EventChallenge
            {
                challengeId = 3,
                challengeName = "Defeat 20 Bosses",
                targetValue = 20,
                reward = new EventChallengeReward { xpReward = 2000, goldReward = 1000, premiumReward = 100 }
            });

            bossRush.reward = new EventReward
            {
                rewardId = 1,
                rewardName = "Boss Rush Cosmetic",
                xpReward = 5000,
                goldReward = 2500,
                premiumReward = 250,
                cosmetic = "Boss Slayer Cloak"
            };

            events[bossRush.eventId] = bossRush;

            // EVENT 2: Dungeon Challenge
            SeasonalEvent dungeonChallenge = new SeasonalEvent
            {
                eventId = eventIdCounter++,
                eventName = "Dungeon Challenge",
                description = "Complete all dungeons with specific conditions",
                eventType = EventType.DungeonChallenge,
                status = EventStatus.Active,
                startDate = DateTime.Now,
                endDate = DateTime.Now.AddDays(14),
                minLevel = 30
            };

            dungeonChallenge.challenges.Add(new EventChallenge
            {
                challengeId = 4,
                challengeName = "Complete Forest Crypt",
                targetValue = 1,
                reward = new EventChallengeReward { xpReward = 1000, goldReward = 500, premiumReward = 50 }
            });

            dungeonChallenge.challenges.Add(new EventChallenge
            {
                challengeId = 5,
                challengeName = "Complete All Dungeons",
                targetValue = 5,
                reward = new EventChallengeReward { xpReward = 5000, goldReward = 2500, premiumReward = 250 }
            });

            dungeonChallenge.reward = new EventReward
            {
                rewardId = 2,
                rewardName = "Dungeon Master Cosmetic",
                xpReward = 10000,
                goldReward = 5000,
                premiumReward = 500,
                cosmetic = "Dungeon Master Armor"
            };

            events[dungeonChallenge.eventId] = dungeonChallenge;

            // EVENT 3: PvP Tournament
            SeasonalEvent pvpTournament = new SeasonalEvent
            {
                eventId = eventIdCounter++,
                eventName = "PvP Tournament",
                description = "Compete in ranked PvP matches for glory",
                eventType = EventType.PvPTournament,
                status = EventStatus.Active,
                startDate = DateTime.Now,
                endDate = DateTime.Now.AddDays(7),
                minLevel = 40
            };

            pvpTournament.challenges.Add(new EventChallenge
            {
                challengeId = 6,
                challengeName = "Win 5 PvP Matches",
                targetValue = 5,
                reward = new EventChallengeReward { xpReward = 500, goldReward = 250, premiumReward = 50 }
            });

            pvpTournament.challenges.Add(new EventChallenge
            {
                challengeId = 7,
                challengeName = "Win 20 PvP Matches",
                targetValue = 20,
                reward = new EventChallengeReward { xpReward = 2000, goldReward = 1000, premiumReward = 200 }
            });

            pvpTournament.reward = new EventReward
            {
                rewardId = 3,
                rewardName = "PvP Champion Cosmetic",
                xpReward = 5000,
                goldReward = 2500,
                premiumReward = 500,
                cosmetic = "Champion Crown"
            };

            events[pvpTournament.eventId] = pvpTournament;

            // EVENT 4: Collection Event
            SeasonalEvent collectionEvent = new SeasonalEvent
            {
                eventId = eventIdCounter++,
                eventName = "Collection Event",
                description = "Collect rare items and complete sets",
                eventType = EventType.CollectionEvent,
                status = EventStatus.Active,
                startDate = DateTime.Now,
                endDate = DateTime.Now.AddDays(21),
                minLevel = 10
            };

            collectionEvent.challenges.Add(new EventChallenge
            {
                challengeId = 8,
                challengeName = "Collect 10 Rare Items",
                targetValue = 10,
                reward = new EventChallengeReward { xpReward = 1000, goldReward = 500, premiumReward = 50 }
            });

            collectionEvent.challenges.Add(new EventChallenge
            {
                challengeId = 9,
                challengeName = "Collect 50 Rare Items",
                targetValue = 50,
                reward = new EventChallengeReward { xpReward = 5000, goldReward = 2500, premiumReward = 250 }
            });

            collectionEvent.reward = new EventReward
            {
                rewardId = 4,
                rewardName = "Collector's Cosmetic",
                xpReward = 10000,
                goldReward = 5000,
                premiumReward = 500,
                cosmetic = "Collector's Robe"
            };

            events[collectionEvent.eventId] = collectionEvent;

            // EVENT 5: Survival Mode
            SeasonalEvent survivalMode = new SeasonalEvent
            {
                eventId = eventIdCounter++,
                eventName = "Survival Mode",
                description = "Survive endless waves of enemies",
                eventType = EventType.SurvivalMode,
                status = EventStatus.Active,
                startDate = DateTime.Now,
                endDate = DateTime.Now.AddDays(7),
                minLevel = 35
            };

            survivalMode.challenges.Add(new EventChallenge
            {
                challengeId = 10,
                challengeName = "Survive 10 Waves",
                targetValue = 10,
                reward = new EventChallengeReward { xpReward = 1000, goldReward = 500, premiumReward = 50 }
            });

            survivalMode.challenges.Add(new EventChallenge
            {
                challengeId = 11,
                challengeName = "Survive 50 Waves",
                targetValue = 50,
                reward = new EventChallengeReward { xpReward = 5000, goldReward = 2500, premiumReward = 250 }
            });

            survivalMode.reward = new EventReward
            {
                rewardId = 5,
                rewardName = "Survivor Cosmetic",
                xpReward = 10000,
                goldReward = 5000,
                premiumReward = 500,
                cosmetic = "Survivor's Armor"
            };

            events[survivalMode.eventId] = survivalMode;
        }

        /// <summary>
        /// Join an event.
        /// </summary>
        public bool JoinEvent(int playerId, int eventId)
        {
            if (!events.ContainsKey(eventId))
                return false;

            SeasonalEvent sevent = events[eventId];

            if (sevent.status != EventStatus.Active)
            {
                Debug.LogWarning("Event is not active");
                return false;
            }

            int playerLevel = PlayerStats.Instance.Level;
            if (playerLevel < sevent.minLevel)
            {
                Debug.LogWarning($"Player level {playerLevel} is below minimum {sevent.minLevel}");
                return false;
            }

            int progressKey = playerId * 10000 + eventId;
            if (!playerProgress.ContainsKey(progressKey))
            {
                playerProgress[progressKey] = new PlayerEventProgress
                {
                    playerId = playerId,
                    eventId = eventId,
                    joinDate = DateTime.Now
                };
                sevent.participantCount++;
            }

            Debug.Log($"Player {playerId} joined event {eventId}");
            OnEventJoined?.Invoke(sevent);

            return true;
        }

        /// <summary>
        /// Complete a challenge.
        /// </summary>
        public void CompleteChallenge(int playerId, int eventId, int challengeId)
        {
            if (!events.ContainsKey(eventId))
                return;

            int progressKey = playerId * 10000 + eventId;
            if (!playerProgress.ContainsKey(progressKey))
                return;

            PlayerEventProgress progress = playerProgress[progressKey];
            SeasonalEvent sevent = events[eventId];
            EventChallenge challenge = sevent.challenges.Find(c => c.challengeId == challengeId);

            if (challenge == null || challenge.completed)
                return;

            challenge.completed = true;
            progress.completedChallenges.Add(challengeId);
            progress.totalProgress++;

            // Award challenge reward
            EventChallengeReward reward = challenge.reward;
            PlayerStats.Instance.AddExperience(reward.xpReward);
            PlayerStats.Instance.AddGold(reward.goldReward);

            Debug.Log($"Challenge completed: {challenge.challengeName}");
            OnChallengeCompleted?.Invoke(challenge);

            // Check if event is completed
            if (progress.completedChallenges.Count == sevent.challenges.Count)
            {
                CompleteEvent(playerId, eventId);
            }
        }

        /// <summary>
        /// Complete an event.
        /// </summary>
        public void CompleteEvent(int playerId, int eventId)
        {
            if (!events.ContainsKey(eventId))
                return;

            int progressKey = playerId * 10000 + eventId;
            if (!playerProgress.ContainsKey(progressKey))
                return;

            PlayerEventProgress progress = playerProgress[progressKey];
            SeasonalEvent sevent = events[eventId];

            if (progress.eventCompleted)
                return;

            progress.eventCompleted = true;

            // Award event reward
            EventReward reward = sevent.reward;
            PlayerStats.Instance.AddExperience(reward.xpReward);
            PlayerStats.Instance.AddGold(reward.goldReward);

            Debug.Log($"Event completed: {sevent.eventName}");
            OnEventCompleted?.Invoke(sevent);
        }

        /// <summary>
        /// Get active events.
        /// </summary>
        public List<SeasonalEvent> GetActiveEvents()
        {
            List<SeasonalEvent> active = new();
            DateTime now = DateTime.Now;

            foreach (var sevent in events.Values)
            {
                if (sevent.status == EventStatus.Active && now >= sevent.startDate && now <= sevent.endDate)
                    active.Add(sevent);
            }

            return active;
        }

        /// <summary>
        /// Get event by ID.
        /// </summary>
        public SeasonalEvent GetEvent(int eventId)
        {
            if (events.ContainsKey(eventId))
                return events[eventId];
            return null;
        }

        /// <summary>
        /// Get player event progress.
        /// </summary>
        public PlayerEventProgress GetPlayerEventProgress(int playerId, int eventId)
        {
            int progressKey = playerId * 10000 + eventId;
            if (playerProgress.ContainsKey(progressKey))
                return playerProgress[progressKey];
            return null;
        }

        /// <summary>
        /// Start new season.
        /// </summary>
        public void StartNewSeason()
        {
            currentSeason++;
            events.Clear();
            playerProgress.Clear();
            eventIdCounter = 1;
            CreateSeasonalEvents();
            Debug.Log($"New season started: Season {currentSeason}");
            OnNewSeasonStarted?.Invoke(currentSeason);
        }

        // Events
        public event Action<SeasonalEvent> OnEventJoined;
        public event Action<EventChallenge> OnChallengeCompleted;
        public event Action<SeasonalEvent> OnEventCompleted;
        public event Action<int> OnNewSeasonStarted;
    }
}
