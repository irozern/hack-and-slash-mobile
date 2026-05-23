using UnityEngine;
using System.Collections.Generic;
using System;

namespace HackSlash.PvP
{
    /// <summary>
    /// Manages PvP (Player vs Player) combat system with matchmaking,
    /// rankings, and seasonal rewards.
    /// </summary>
    public class PvPManager : MonoBehaviour
    {
        public static PvPManager Instance { get; private set; }

        public enum PvPMode
        {
            Casual,      // No ranking
            Ranked,      // Ranked matches
            Tournament   // Tournament brackets
        }

        public enum MatchResult
        {
            Win,
            Loss,
            Draw
        }

        [System.Serializable]
        public class PvPPlayer
        {
            public int playerId;
            public string playerName;
            public int rating;
            public int wins;
            public int losses;
            public int draws;
            public float winRate;
            public int currentRank;
            public int seasonalPoints;
            public List<int> matchHistory = new();
        }

        [System.Serializable]
        public class PvPMatch
        {
            public int matchId;
            public int player1Id;
            public int player2Id;
            public int winnerId;
            public int ratingChange;
            public long timestamp;
            public int duration;
            public MatchResult result;
            public List<string> highlights = new();
        }

        [System.Serializable]
        public class Rank
        {
            public int rankId;
            public string rankName;
            public int minRating;
            public int maxRating;
            public string icon;
            public int rewardGold;
            public int rewardPremium;
        }

        [System.Serializable]
        public class SeasonalReward
        {
            public int rewardId;
            public string rewardName;
            public int minRank;
            public int goldReward;
            public int premiumReward;
            public List<int> itemRewards = new();
        }

        private Dictionary<int, PvPPlayer> pvpPlayers = new();
        private Dictionary<int, PvPMatch> matchHistory = new();
        private Dictionary<int, Rank> ranks = new();
        private Dictionary<int, SeasonalReward> seasonalRewards = new();
        private int matchIdCounter = 1;
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
            InitializePvP();
        }

        /// <summary>
        /// Initialize PvP system.
        /// </summary>
        private void InitializePvP()
        {
            InitializeRanks();
            InitializeSeasonalRewards();
            CreatePlayerProfile();
            Debug.Log("PvP system initialized");
        }

        /// <summary>
        /// Initialize rank system.
        /// </summary>
        private void InitializeRanks()
        {
            ranks[1] = new Rank { rankId = 1, rankName = "Bronze", minRating = 0, maxRating = 1000, rewardGold = 100, rewardPremium = 10 };
            ranks[2] = new Rank { rankId = 2, rankName = "Silver", minRating = 1000, maxRating = 1500, rewardGold = 200, rewardPremium = 20 };
            ranks[3] = new Rank { rankId = 3, rankName = "Gold", minRating = 1500, maxRating = 2000, rewardGold = 300, rewardPremium = 30 };
            ranks[4] = new Rank { rankId = 4, rankName = "Platinum", minRating = 2000, maxRating = 2500, rewardGold = 400, rewardPremium = 40 };
            ranks[5] = new Rank { rankId = 5, rankName = "Diamond", minRating = 2500, maxRating = 3000, rewardGold = 500, rewardPremium = 50 };
            ranks[6] = new Rank { rankId = 6, rankName = "Master", minRating = 3000, maxRating = 3500, rewardGold = 600, rewardPremium = 60 };
            ranks[7] = new Rank { rankId = 7, rankName = "Grandmaster", minRating = 3500, maxRating = 4000, rewardGold = 700, rewardPremium = 70 };
            ranks[8] = new Rank { rankId = 8, rankName = "Legend", minRating = 4000, maxRating = 5000, rewardGold = 1000, rewardPremium = 100 };
        }

        /// <summary>
        /// Initialize seasonal rewards.
        /// </summary>
        private void InitializeSeasonalRewards()
        {
            seasonalRewards[1] = new SeasonalReward
            {
                rewardId = 1,
                rewardName = "Bronze Reward",
                minRank = 1,
                goldReward = 500,
                premiumReward = 50,
                itemRewards = new List<int> { 1, 2 }
            };

            seasonalRewards[2] = new SeasonalReward
            {
                rewardId = 2,
                rewardName = "Silver Reward",
                minRank = 2,
                goldReward = 1000,
                premiumReward = 100,
                itemRewards = new List<int> { 3, 4, 5 }
            };

            seasonalRewards[3] = new SeasonalReward
            {
                rewardId = 3,
                rewardName = "Gold Reward",
                minRank = 3,
                goldReward = 2000,
                premiumReward = 200,
                itemRewards = new List<int> { 6, 7, 8 }
            };

            seasonalRewards[4] = new SeasonalReward
            {
                rewardId = 4,
                rewardName = "Platinum Reward",
                minRank = 4,
                goldReward = 3000,
                premiumReward = 300,
                itemRewards = new List<int> { 10, 11, 12 }
            };

            seasonalRewards[5] = new SeasonalReward
            {
                rewardId = 5,
                rewardName = "Diamond Reward",
                minRank = 5,
                goldReward = 5000,
                premiumReward = 500,
                itemRewards = new List<int> { 15, 16, 17 }
            };

            seasonalRewards[6] = new SeasonalReward
            {
                rewardId = 6,
                rewardName = "Master Reward",
                minRank = 6,
                goldReward = 7000,
                premiumReward = 700,
                itemRewards = new List<int> { 20, 21, 22 }
            };

            seasonalRewards[7] = new SeasonalReward
            {
                rewardId = 7,
                rewardName = "Grandmaster Reward",
                minRank = 7,
                goldReward = 10000,
                premiumReward = 1000,
                itemRewards = new List<int> { 25, 26, 27 }
            };

            seasonalRewards[8] = new SeasonalReward
            {
                rewardId = 8,
                rewardName = "Legend Reward",
                minRank = 8,
                goldReward = 15000,
                premiumReward = 1500,
                itemRewards = new List<int> { 30, 31, 32 }
            };
        }

        /// <summary>
        /// Create player PvP profile.
        /// </summary>
        private void CreatePlayerProfile()
        {
            int playerId = PlayerStats.Instance.PlayerId;
            if (!pvpPlayers.ContainsKey(playerId))
            {
                pvpPlayers[playerId] = new PvPPlayer
                {
                    playerId = playerId,
                    playerName = PlayerStats.Instance.PlayerName,
                    rating = 1000,
                    wins = 0,
                    losses = 0,
                    draws = 0,
                    currentRank = 1
                };
            }
        }

        /// <summary>
        /// Find opponent for match.
        /// </summary>
        public PvPPlayer FindOpponent(int playerId)
        {
            if (!pvpPlayers.ContainsKey(playerId))
                return null;

            PvPPlayer player = pvpPlayers[playerId];
            PvPPlayer bestOpponent = null;
            int smallestRatingDiff = int.MaxValue;

            foreach (var opponent in pvpPlayers.Values)
            {
                if (opponent.playerId == playerId)
                    continue;

                int ratingDiff = Mathf.Abs(opponent.rating - player.rating);
                if (ratingDiff < smallestRatingDiff)
                {
                    smallestRatingDiff = ratingDiff;
                    bestOpponent = opponent;
                }
            }

            return bestOpponent;
        }

        /// <summary>
        /// Complete a PvP match.
        /// </summary>
        public void CompleteMatch(int player1Id, int player2Id, int winnerId)
        {
            if (!pvpPlayers.ContainsKey(player1Id) || !pvpPlayers.ContainsKey(player2Id))
                return;

            PvPPlayer player1 = pvpPlayers[player1Id];
            PvPPlayer player2 = pvpPlayers[player2Id];

            // Calculate rating change (simplified ELO)
            int ratingChange = CalculateRatingChange(player1.rating, player2.rating);

            // Update player stats
            if (winnerId == player1Id)
            {
                player1.wins++;
                player1.rating += ratingChange;
                player2.losses++;
                player2.rating -= ratingChange;
            }
            else if (winnerId == player2Id)
            {
                player2.wins++;
                player2.rating += ratingChange;
                player1.losses++;
                player1.rating -= ratingChange;
            }
            else
            {
                player1.draws++;
                player2.draws++;
            }

            // Update win rate
            player1.winRate = (float)player1.wins / (player1.wins + player1.losses + player1.draws);
            player2.winRate = (float)player2.wins / (player2.wins + player2.losses + player2.draws);

            // Update rank
            player1.currentRank = GetRankFromRating(player1.rating);
            player2.currentRank = GetRankFromRating(player2.rating);

            // Create match record
            PvPMatch match = new PvPMatch
            {
                matchId = matchIdCounter++,
                player1Id = player1Id,
                player2Id = player2Id,
                winnerId = winnerId,
                ratingChange = ratingChange,
                timestamp = DateTime.Now.Ticks,
                result = winnerId == player1Id ? MatchResult.Win : (winnerId == player2Id ? MatchResult.Loss : MatchResult.Draw)
            };

            matchHistory[match.matchId] = match;

            Debug.Log($"Match completed: {player1.playerName} vs {player2.playerName}. Winner: {winnerId}");
            OnMatchCompleted?.Invoke(match);
        }

        /// <summary>
        /// Calculate rating change based on ELO.
        /// </summary>
        private int CalculateRatingChange(int player1Rating, int player2Rating)
        {
            int ratingDiff = player1Rating - player2Rating;
            int baseChange = 32;

            if (ratingDiff > 200)
                return baseChange / 2;
            else if (ratingDiff < -200)
                return baseChange * 2;
            else
                return baseChange;
        }

        /// <summary>
        /// Get rank from rating.
        /// </summary>
        private int GetRankFromRating(int rating)
        {
            foreach (var rank in ranks.Values)
            {
                if (rating >= rank.minRating && rating < rank.maxRating)
                    return rank.rankId;
            }
            return 8; // Legend
        }

        /// <summary>
        /// Get PvP player.
        /// </summary>
        public PvPPlayer GetPvPPlayer(int playerId)
        {
            if (pvpPlayers.ContainsKey(playerId))
                return pvpPlayers[playerId];
            return null;
        }

        /// <summary>
        /// Get rank by ID.
        /// </summary>
        public Rank GetRank(int rankId)
        {
            if (ranks.ContainsKey(rankId))
                return ranks[rankId];
            return null;
        }

        /// <summary>
        /// Get leaderboard.
        /// </summary>
        public List<PvPPlayer> GetLeaderboard(int limit = 100)
        {
            List<PvPPlayer> leaderboard = new List<PvPPlayer>(pvpPlayers.Values);
            leaderboard.Sort((a, b) => b.rating.CompareTo(a.rating));
            return leaderboard.GetRange(0, Mathf.Min(limit, leaderboard.Count));
        }

        /// <summary>
        /// Get seasonal rewards.
        /// </summary>
        public SeasonalReward GetSeasonalReward(int rankId)
        {
            foreach (var reward in seasonalRewards.Values)
            {
                if (reward.minRank == rankId)
                    return reward;
            }
            return null;
        }

        /// <summary>
        /// Claim seasonal rewards.
        /// </summary>
        public void ClaimSeasonalRewards(int playerId)
        {
            PvPPlayer player = GetPvPPlayer(playerId);
            if (player == null)
                return;

            SeasonalReward reward = GetSeasonalReward(player.currentRank);
            if (reward == null)
                return;

            PlayerStats.Instance.AddGold(reward.goldReward);
            Debug.Log($"Seasonal rewards claimed: {reward.goldReward} gold, {reward.premiumReward} premium");
            OnSeasonalRewardsClaimed?.Invoke(reward);
        }

        /// <summary>
        /// Get current season.
        /// </summary>
        public int GetCurrentSeason()
        {
            return currentSeason;
        }

        /// <summary>
        /// Start new season.
        /// </summary>
        public void StartNewSeason()
        {
            currentSeason++;
            // Reset seasonal points
            foreach (var player in pvpPlayers.Values)
            {
                player.seasonalPoints = 0;
            }
            Debug.Log($"New season started: Season {currentSeason}");
            OnNewSeasonStarted?.Invoke(currentSeason);
        }

        // Events
        public event Action<PvPMatch> OnMatchCompleted;
        public event Action<SeasonalReward> OnSeasonalRewardsClaimed;
        public event Action<int> OnNewSeasonStarted;
    }
}
