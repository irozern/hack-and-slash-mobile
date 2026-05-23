using UnityEngine;
using System.Collections.Generic;
using System;

namespace HackSlash.Social
{
    /// <summary>
    /// Manages guilds, guild wars, and social features.
    /// Handles guild creation, membership, and guild-wide events.
    /// </summary>
    public class GuildManager : MonoBehaviour
    {
        public static GuildManager Instance { get; private set; }

        public enum GuildRank
        {
            Member,
            Officer,
            Leader
        }

        [System.Serializable]
        public class Guild
        {
            public int guildId;
            public string guildName;
            public string description;
            public int leaderId;
            public int level;
            public int memberCount;
            public int maxMembers;
            public long gold;
            public long treasury;
            public List<GuildMember> members = new();
            public List<GuildPerk> perks = new();
            public DateTime createdDate;
            public int wins;
            public int losses;
        }

        [System.Serializable]
        public class GuildMember
        {
            public int memberId;
            public string memberName;
            public int level;
            public GuildRank rank;
            public long joinDate;
            public int contributionPoints;
        }

        [System.Serializable]
        public class GuildPerk
        {
            public int perkId;
            public string perkName;
            public string description;
            public int level;
            public float bonus;
            public int costToUpgrade;
        }

        [System.Serializable]
        public class GuildWar
        {
            public int warId;
            public int guild1Id;
            public int guild2Id;
            public int guild1Score;
            public int guild2Score;
            public int winnerId;
            public long startTime;
            public long endTime;
            public int duration;
        }

        private Dictionary<int, Guild> guilds = new();
        private Dictionary<int, GuildWar> guildWars = new();
        private int guildIdCounter = 1;
        private int warIdCounter = 1;

        void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeGuildSystem();
        }

        /// <summary>
        /// Initialize guild system.
        /// </summary>
        private void InitializeGuildSystem()
        {
            Debug.Log("Guild system initialized");
        }

        /// <summary>
        /// Create a new guild.
        /// </summary>
        public Guild CreateGuild(string guildName, string description, int leaderId)
        {
            Guild newGuild = new Guild
            {
                guildId = guildIdCounter++,
                guildName = guildName,
                description = description,
                leaderId = leaderId,
                level = 1,
                memberCount = 1,
                maxMembers = 50,
                gold = 0,
                treasury = 0,
                createdDate = DateTime.Now,
                wins = 0,
                losses = 0
            };

            // Add leader as first member
            newGuild.members.Add(new GuildMember
            {
                memberId = leaderId,
                memberName = PlayerStats.Instance.PlayerName,
                level = PlayerStats.Instance.Level,
                rank = GuildRank.Leader,
                joinDate = DateTime.Now.Ticks,
                contributionPoints = 0
            });

            // Add default perks
            newGuild.perks.Add(new GuildPerk { perkId = 1, perkName = "Experience Boost", bonus = 1.1f, level = 1 });
            newGuild.perks.Add(new GuildPerk { perkId = 2, perkName = "Gold Boost", bonus = 1.1f, level = 1 });
            newGuild.perks.Add(new GuildPerk { perkId = 3, perkName = "Loot Boost", bonus = 1.1f, level = 1 });

            guilds[newGuild.guildId] = newGuild;

            Debug.Log($"Guild created: {guildName}");
            OnGuildCreated?.Invoke(newGuild);

            return newGuild;
        }

        /// <summary>
        /// Join a guild.
        /// </summary>
        public bool JoinGuild(int guildId, int playerId)
        {
            if (!guilds.ContainsKey(guildId))
                return false;

            Guild guild = guilds[guildId];

            if (guild.memberCount >= guild.maxMembers)
            {
                Debug.LogWarning("Guild is full");
                return false;
            }

            guild.members.Add(new GuildMember
            {
                memberId = playerId,
                memberName = PlayerStats.Instance.PlayerName,
                level = PlayerStats.Instance.Level,
                rank = GuildRank.Member,
                joinDate = DateTime.Now.Ticks,
                contributionPoints = 0
            });

            guild.memberCount++;

            Debug.Log($"Player {playerId} joined guild {guildId}");
            OnMemberJoined?.Invoke(guild, playerId);

            return true;
        }

        /// <summary>
        /// Leave a guild.
        /// </summary>
        public bool LeaveGuild(int guildId, int playerId)
        {
            if (!guilds.ContainsKey(guildId))
                return false;

            Guild guild = guilds[guildId];

            GuildMember member = guild.members.Find(m => m.memberId == playerId);
            if (member == null)
                return false;

            if (member.rank == GuildRank.Leader)
            {
                Debug.LogWarning("Leader cannot leave guild");
                return false;
            }

            guild.members.Remove(member);
            guild.memberCount--;

            Debug.Log($"Player {playerId} left guild {guildId}");
            OnMemberLeft?.Invoke(guild, playerId);

            return true;
        }

        /// <summary>
        /// Contribute to guild treasury.
        /// </summary>
        public void ContributeToGuild(int guildId, int playerId, long amount)
        {
            if (!guilds.ContainsKey(guildId))
                return;

            Guild guild = guilds[guildId];
            guild.treasury += amount;

            GuildMember member = guild.members.Find(m => m.memberId == playerId);
            if (member != null)
            {
                member.contributionPoints += (int)(amount / 100);
            }

            Debug.Log($"Player {playerId} contributed {amount} to guild {guildId}");
            OnContributionMade?.Invoke(guild, playerId, amount);
        }

        /// <summary>
        /// Start a guild war.
        /// </summary>
        public GuildWar StartGuildWar(int guild1Id, int guild2Id, int duration)
        {
            if (!guilds.ContainsKey(guild1Id) || !guilds.ContainsKey(guild2Id))
                return null;

            GuildWar war = new GuildWar
            {
                warId = warIdCounter++,
                guild1Id = guild1Id,
                guild2Id = guild2Id,
                guild1Score = 0,
                guild2Score = 0,
                startTime = DateTime.Now.Ticks,
                duration = duration
            };

            guildWars[war.warId] = war;

            Debug.Log($"Guild war started: Guild {guild1Id} vs Guild {guild2Id}");
            OnGuildWarStarted?.Invoke(war);

            return war;
        }

        /// <summary>
        /// Complete a guild war.
        /// </summary>
        public void CompleteGuildWar(int warId)
        {
            if (!guildWars.ContainsKey(warId))
                return;

            GuildWar war = guildWars[warId];
            war.endTime = DateTime.Now.Ticks;

            Guild guild1 = guilds[war.guild1Id];
            Guild guild2 = guilds[war.guild2Id];

            if (war.guild1Score > war.guild2Score)
            {
                war.winnerId = war.guild1Id;
                guild1.wins++;
                guild2.losses++;
            }
            else if (war.guild2Score > war.guild1Score)
            {
                war.winnerId = war.guild2Id;
                guild2.wins++;
                guild1.losses++;
            }

            Debug.Log($"Guild war completed: Guild {war.winnerId} won");
            OnGuildWarCompleted?.Invoke(war);
        }

        /// <summary>
        /// Upgrade guild perk.
        /// </summary>
        public bool UpgradeGuildPerk(int guildId, int perkId)
        {
            if (!guilds.ContainsKey(guildId))
                return false;

            Guild guild = guilds[guildId];
            GuildPerk perk = guild.perks.Find(p => p.perkId == perkId);

            if (perk == null)
                return false;

            if (guild.treasury < perk.costToUpgrade)
            {
                Debug.LogWarning("Not enough guild treasury");
                return false;
            }

            guild.treasury -= perk.costToUpgrade;
            perk.level++;
            perk.bonus += 0.05f;
            perk.costToUpgrade = (int)(perk.costToUpgrade * 1.5f);

            Debug.Log($"Guild perk upgraded: {perk.perkName} to level {perk.level}");
            OnPerkUpgraded?.Invoke(guild, perk);

            return true;
        }

        /// <summary>
        /// Get guild by ID.
        /// </summary>
        public Guild GetGuild(int guildId)
        {
            if (guilds.ContainsKey(guildId))
                return guilds[guildId];
            return null;
        }

        /// <summary>
        /// Get all guilds.
        /// </summary>
        public List<Guild> GetAllGuilds()
        {
            return new List<Guild>(guilds.Values);
        }

        /// <summary>
        /// Get guild by player ID.
        /// </summary>
        public Guild GetGuildByPlayer(int playerId)
        {
            foreach (var guild in guilds.Values)
            {
                if (guild.members.Exists(m => m.memberId == playerId))
                    return guild;
            }
            return null;
        }

        /// <summary>
        /// Get guild war by ID.
        /// </summary>
        public GuildWar GetGuildWar(int warId)
        {
            if (guildWars.ContainsKey(warId))
                return guildWars[warId];
            return null;
        }

        /// <summary>
        /// Get active guild wars.
        /// </summary>
        public List<GuildWar> GetActiveGuildWars()
        {
            List<GuildWar> active = new();
            long currentTime = DateTime.Now.Ticks;

            foreach (var war in guildWars.Values)
            {
                if (war.endTime == 0 || (currentTime - war.startTime) < (war.duration * 10000000))
                    active.Add(war);
            }

            return active;
        }

        // Events
        public event Action<Guild> OnGuildCreated;
        public event Action<Guild, int> OnMemberJoined;
        public event Action<Guild, int> OnMemberLeft;
        public event Action<Guild, int, long> OnContributionMade;
        public event Action<GuildWar> OnGuildWarStarted;
        public event Action<GuildWar> OnGuildWarCompleted;
        public event Action<Guild, GuildPerk> OnPerkUpgraded;
    }
}
