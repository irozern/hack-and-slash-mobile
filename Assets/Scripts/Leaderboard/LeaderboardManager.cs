using UnityEngine;
using System.Collections.Generic;
using System;

namespace HackSlash.Leaderboard
{
    /// <summary>
    /// Manages leaderboards for different categories.
    /// Tracks player rankings, achievements, and statistics.
    /// </summary>
    public class LeaderboardManager : MonoBehaviour
    {
        public static LeaderboardManager Instance { get; private set; }

        public enum LeaderboardType
        {
            Level,
            Experience,
            PvPRating,
            Gold,
            BossKills,
            DungeonClears,
            PlayTime,
            Achievements
        }

        [System.Serializable]
        public class LeaderboardEntry
        {
            public int rank;
            public int playerId;
            public string playerName;
            public long value;
            public DateTime lastUpdated;
        }

        [System.Serializable]
        public class PlayerStats
        {
            public int playerId;
            public string playerName;
            public int level;
            public long experience;
            public int pvpRating;
            public long totalGold;
            public int bossKills;
            public int dungeonClears;
            public long playTime;
            public int achievementCount;
        }

        private Dictionary<LeaderboardType, List<LeaderboardEntry>> leaderboards = new();
        private Dictionary<int, PlayerStats> playerStats = new();

        void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeLeaderboards();
        }

        /// <summary>
        /// Initialize all leaderboards.
        /// </summary>
        private void InitializeLeaderboards()
        {
            foreach (LeaderboardType type in System.Enum.GetValues(typeof(LeaderboardType)))
            {
                leaderboards[type] = new List<LeaderboardEntry>();
            }
            Debug.Log("Leaderboards initialized");
        }

        /// <summary>
        /// Update player stats.
        /// </summary>
        public void UpdatePlayerStats(int playerId, string playerName, int level, long experience, int pvpRating, long totalGold, int bossKills, int dungeonClears, long playTime, int achievementCount)
        {
            if (!playerStats.ContainsKey(playerId))
            {
                playerStats[playerId] = new PlayerStats();
            }

            PlayerStats stats = playerStats[playerId];
            stats.playerId = playerId;
            stats.playerName = playerName;
            stats.level = level;
            stats.experience = experience;
            stats.pvpRating = pvpRating;
            stats.totalGold = totalGold;
            stats.bossKills = bossKills;
            stats.dungeonClears = dungeonClears;
            stats.playTime = playTime;
            stats.achievementCount = achievementCount;

            // Update leaderboards
            UpdateLeaderboard(LeaderboardType.Level, playerId, playerName, level);
            UpdateLeaderboard(LeaderboardType.Experience, playerId, playerName, (long)experience);
            UpdateLeaderboard(LeaderboardType.PvPRating, playerId, playerName, pvpRating);
            UpdateLeaderboard(LeaderboardType.Gold, playerId, playerName, totalGold);
            UpdateLeaderboard(LeaderboardType.BossKills, playerId, playerName, bossKills);
            UpdateLeaderboard(LeaderboardType.DungeonClears, playerId, playerName, dungeonClears);
            UpdateLeaderboard(LeaderboardType.PlayTime, playerId, playerName, playTime);
            UpdateLeaderboard(LeaderboardType.Achievements, playerId, playerName, achievementCount);

            OnPlayerStatsUpdated?.Invoke(playerId);
        }

        /// <summary>
        /// Update a specific leaderboard.
        /// </summary>
        private void UpdateLeaderboard(LeaderboardType type, int playerId, string playerName, long value)
        {
            List<LeaderboardEntry> leaderboard = leaderboards[type];

            // Find existing entry
            LeaderboardEntry entry = leaderboard.Find(e => e.playerId == playerId);

            if (entry != null)
            {
                entry.value = value;
                entry.lastUpdated = DateTime.Now;
            }
            else
            {
                entry = new LeaderboardEntry
                {
                    playerId = playerId,
                    playerName = playerName,
                    value = value,
                    lastUpdated = DateTime.Now
                };
                leaderboard.Add(entry);
            }

            // Sort leaderboard
            leaderboard.Sort((a, b) => b.value.CompareTo(a.value));

            // Update ranks
            for (int i = 0; i < leaderboard.Count; i++)
            {
                leaderboard[i].rank = i + 1;
            }
        }

        /// <summary>
        /// Get leaderboard.
        /// </summary>
        public List<LeaderboardEntry> GetLeaderboard(LeaderboardType type, int limit = 100)
        {
            if (!leaderboards.ContainsKey(type))
                return new List<LeaderboardEntry>();

            List<LeaderboardEntry> leaderboard = leaderboards[type];
            return leaderboard.GetRange(0, Mathf.Min(limit, leaderboard.Count));
        }

        /// <summary>
        /// Get player rank.
        /// </summary>
        public int GetPlayerRank(LeaderboardType type, int playerId)
        {
            if (!leaderboards.ContainsKey(type))
                return -1;

            LeaderboardEntry entry = leaderboards[type].Find(e => e.playerId == playerId);
            return entry != null ? entry.rank : -1;
        }

        /// <summary>
        /// Get player leaderboard position.
        /// </summary>
        public LeaderboardEntry GetPlayerPosition(LeaderboardType type, int playerId)
        {
            if (!leaderboards.ContainsKey(type))
                return null;

            return leaderboards[type].Find(e => e.playerId == playerId);
        }

        /// <summary>
        /// Get top players.
        /// </summary>
        public List<LeaderboardEntry> GetTopPlayers(LeaderboardType type, int count = 10)
        {
            return GetLeaderboard(type, count);
        }

        /// <summary>
        /// Get player stats.
        /// </summary>
        public PlayerStats GetPlayerStats(int playerId)
        {
            if (playerStats.ContainsKey(playerId))
                return playerStats[playerId];
            return null;
        }

        /// <summary>
        /// Get nearby players in leaderboard.
        /// </summary>
        public List<LeaderboardEntry> GetNearbyPlayers(LeaderboardType type, int playerId, int range = 5)
        {
            if (!leaderboards.ContainsKey(type))
                return new List<LeaderboardEntry>();

            int playerRank = GetPlayerRank(type, playerId);
            if (playerRank == -1)
                return new List<LeaderboardEntry>();

            int startRank = Mathf.Max(1, playerRank - range);
            int endRank = playerRank + range;

            List<LeaderboardEntry> nearby = new();
            foreach (var entry in leaderboards[type])
            {
                if (entry.rank >= startRank && entry.rank <= endRank)
                    nearby.Add(entry);
            }

            return nearby;
        }

        /// <summary>
        /// Get leaderboard statistics.
        /// </summary>
        public Dictionary<string, object> GetLeaderboardStats(LeaderboardType type)
        {
            List<LeaderboardEntry> leaderboard = GetLeaderboard(type);

            Dictionary<string, object> stats = new();
            stats["TotalPlayers"] = leaderboard.Count;

            if (leaderboard.Count > 0)
            {
                stats["TopValue"] = leaderboard[0].value;
                stats["AverageValue"] = CalculateAverageValue(leaderboard);
                stats["MedianValue"] = CalculateMedianValue(leaderboard);
            }

            return stats;
        }

        /// <summary>
        /// Calculate average value.
        /// </summary>
        private long CalculateAverageValue(List<LeaderboardEntry> entries)
        {
            if (entries.Count == 0)
                return 0;

            long sum = 0;
            foreach (var entry in entries)
            {
                sum += entry.value;
            }

            return sum / entries.Count;
        }

        /// <summary>
        /// Calculate median value.
        /// </summary>
        private long CalculateMedianValue(List<LeaderboardEntry> entries)
        {
            if (entries.Count == 0)
                return 0;

            int mid = entries.Count / 2;
            if (entries.Count % 2 == 0)
            {
                return (entries[mid - 1].value + entries[mid].value) / 2;
            }
            else
            {
                return entries[mid].value;
            }
        }

        /// <summary>
        /// Reset leaderboards (for new season).
        /// </summary>
        public void ResetLeaderboards()
        {
            foreach (var type in leaderboards.Keys)
            {
                leaderboards[type].Clear();
            }
            Debug.Log("Leaderboards reset");
            OnLeaderboardsReset?.Invoke();
        }

        // Events
        public event Action<int> OnPlayerStatsUpdated;
        public event Action OnLeaderboardsReset;
    }
}
