using UnityEngine;
using System.Collections.Generic;
using System;

namespace HackSlash.Progression
{
    /// <summary>
    /// Manages skill trees with 3 different paths: Warrior, Ranger, Mage.
    /// Handles skill progression, unlocking, and stat bonuses.
    /// </summary>
    public class SkillTreeManager : MonoBehaviour
    {
        public static SkillTreeManager Instance { get; private set; }

        public enum SkillTreePath
        {
            Warrior,  // Melee combat focus
            Ranger,   // Speed and ranged focus
            Mage      // Magic and crowd control
        }

        [System.Serializable]
        public class Skill
        {
            public int skillId;
            public string skillName;
            public string description;
            public SkillTreePath path;
            public int requiredLevel;
            public int skillPointCost;
            public List<int> prerequisiteSkills = new();
            public SkillEffect effect;
            public bool unlocked;
        }

        [System.Serializable]
        public class SkillEffect
        {
            public int damageBonus;
            public int armorBonus;
            public int hpBonus;
            public int speedBonus;
            public float cooldownReduction;
            public int manaBonus;
            public string specialAbility;
        }

        [System.Serializable]
        public class SkillTree
        {
            public SkillTreePath path;
            public List<Skill> skills = new();
            public int totalSkillPoints;
            public int usedSkillPoints;
        }

        private Dictionary<SkillTreePath, SkillTree> skillTrees = new();
        private Dictionary<int, Skill> allSkills = new();
        private int totalSkillPoints = 0;

        void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeSkillTrees();
        }

        /// <summary>
        /// Initialize all skill trees.
        /// </summary>
        private void InitializeSkillTrees()
        {
            // WARRIOR TREE (10 skills)
            skillTrees[SkillTreePath.Warrior] = new SkillTree { path = SkillTreePath.Warrior };

            allSkills[1] = new Skill
            {
                skillId = 1,
                skillName = "Power Strike",
                description = "Increase damage by 20%",
                path = SkillTreePath.Warrior,
                requiredLevel = 1,
                skillPointCost = 1,
                effect = new SkillEffect { damageBonus = 20 }
            };

            allSkills[2] = new Skill
            {
                skillId = 2,
                skillName = "Iron Skin",
                description = "Increase armor by 15%",
                path = SkillTreePath.Warrior,
                requiredLevel = 5,
                skillPointCost = 1,
                prerequisiteSkills = new List<int> { 1 },
                effect = new SkillEffect { armorBonus = 15 }
            };

            allSkills[3] = new Skill
            {
                skillId = 3,
                skillName = "Whirlwind Attack",
                description = "AOE attack hitting all enemies",
                path = SkillTreePath.Warrior,
                requiredLevel = 10,
                skillPointCost = 2,
                prerequisiteSkills = new List<int> { 1 },
                effect = new SkillEffect { damageBonus = 30, specialAbility = "Whirlwind" }
            };

            allSkills[4] = new Skill
            {
                skillId = 4,
                skillName = "Berserker Rage",
                description = "Massive damage boost for 10 seconds",
                path = SkillTreePath.Warrior,
                requiredLevel = 15,
                skillPointCost = 2,
                prerequisiteSkills = new List<int> { 3 },
                effect = new SkillEffect { damageBonus = 50, specialAbility = "Berserker" }
            };

            allSkills[5] = new Skill
            {
                skillId = 5,
                skillName = "Fortress",
                description = "Reduce all damage by 30%",
                path = SkillTreePath.Warrior,
                requiredLevel = 20,
                skillPointCost = 2,
                prerequisiteSkills = new List<int> { 2 },
                effect = new SkillEffect { armorBonus = 30, specialAbility = "Fortress" }
            };

            allSkills[6] = new Skill
            {
                skillId = 6,
                skillName = "Cleave",
                description = "Strike multiple enemies in a line",
                path = SkillTreePath.Warrior,
                requiredLevel = 25,
                skillPointCost = 2,
                prerequisiteSkills = new List<int> { 3 },
                effect = new SkillEffect { damageBonus = 40, specialAbility = "Cleave" }
            };

            allSkills[7] = new Skill
            {
                skillId = 7,
                skillName = "Last Stand",
                description = "Survive lethal damage once",
                path = SkillTreePath.Warrior,
                requiredLevel = 30,
                skillPointCost = 3,
                prerequisiteSkills = new List<int> { 5 },
                effect = new SkillEffect { hpBonus = 50, specialAbility = "LastStand" }
            };

            allSkills[8] = new Skill
            {
                skillId = 8,
                skillName = "Shockwave",
                description = "Knock back all nearby enemies",
                path = SkillTreePath.Warrior,
                requiredLevel = 35,
                skillPointCost = 2,
                prerequisiteSkills = new List<int> { 4, 6 },
                effect = new SkillEffect { damageBonus = 35, specialAbility = "Shockwave" }
            };

            allSkills[9] = new Skill
            {
                skillId = 9,
                skillName = "Immortal",
                description = "Become immune to damage for 5 seconds",
                path = SkillTreePath.Warrior,
                requiredLevel = 40,
                skillPointCost = 3,
                prerequisiteSkills = new List<int> { 7 },
                effect = new SkillEffect { armorBonus = 100, specialAbility = "Immortal" }
            };

            allSkills[10] = new Skill
            {
                skillId = 10,
                skillName = "God of War",
                description = "Ultimate warrior ability",
                path = SkillTreePath.Warrior,
                requiredLevel = 50,
                skillPointCost = 3,
                prerequisiteSkills = new List<int> { 8, 9 },
                effect = new SkillEffect { damageBonus = 100, armorBonus = 50, specialAbility = "GodOfWar" }
            };

            // RANGER TREE (10 skills)
            skillTrees[SkillTreePath.Ranger] = new SkillTree { path = SkillTreePath.Ranger };

            allSkills[11] = new Skill
            {
                skillId = 11,
                skillName = "Quick Shot",
                description = "Increase attack speed by 20%",
                path = SkillTreePath.Ranger,
                requiredLevel = 1,
                skillPointCost = 1,
                effect = new SkillEffect { speedBonus = 20 }
            };

            allSkills[12] = new Skill
            {
                skillId = 12,
                skillName = "Evasion",
                description = "Increase dodge chance by 15%",
                path = SkillTreePath.Ranger,
                requiredLevel = 5,
                skillPointCost = 1,
                prerequisiteSkills = new List<int> { 11 },
                effect = new SkillEffect { speedBonus = 15, specialAbility = "Evasion" }
            };

            allSkills[13] = new Skill
            {
                skillId = 13,
                skillName = "Multi Shot",
                description = "Fire multiple arrows at once",
                path = SkillTreePath.Ranger,
                requiredLevel = 10,
                skillPointCost = 2,
                prerequisiteSkills = new List<int> { 11 },
                effect = new SkillEffect { damageBonus = 25, specialAbility = "MultiShot" }
            };

            allSkills[14] = new Skill
            {
                skillId = 14,
                skillName = "Piercing Shot",
                description = "Arrows pierce through enemies",
                path = SkillTreePath.Ranger,
                requiredLevel = 15,
                skillPointCost = 2,
                prerequisiteSkills = new List<int> { 13 },
                effect = new SkillEffect { damageBonus = 35, specialAbility = "PiercingShot" }
            };

            allSkills[15] = new Skill
            {
                skillId = 15,
                skillName = "Shadow Clone",
                description = "Create a shadow clone to fight",
                path = SkillTreePath.Ranger,
                requiredLevel = 20,
                skillPointCost = 2,
                prerequisiteSkills = new List<int> { 12 },
                effect = new SkillEffect { damageBonus = 30, specialAbility = "ShadowClone" }
            };

            allSkills[16] = new Skill
            {
                skillId = 16,
                skillName = "Ricochet",
                description = "Arrows bounce off walls",
                path = SkillTreePath.Ranger,
                requiredLevel = 25,
                skillPointCost = 2,
                prerequisiteSkills = new List<int> { 14 },
                effect = new SkillEffect { damageBonus = 28, specialAbility = "Ricochet" }
            };

            allSkills[17] = new Skill
            {
                skillId = 17,
                skillName = "Blink",
                description = "Teleport short distance",
                path = SkillTreePath.Ranger,
                requiredLevel = 30,
                skillPointCost = 2,
                prerequisiteSkills = new List<int> { 12 },
                effect = new SkillEffect { speedBonus = 30, specialAbility = "Blink" }
            };

            allSkills[18] = new Skill
            {
                skillId = 18,
                skillName = "Explosive Arrow",
                description = "Arrows explode on impact",
                path = SkillTreePath.Ranger,
                requiredLevel = 35,
                skillPointCost = 2,
                prerequisiteSkills = new List<int> { 14, 16 },
                effect = new SkillEffect { damageBonus = 40, specialAbility = "ExplosiveArrow" }
            };

            allSkills[19] = new Skill
            {
                skillId = 19,
                skillName = "Phantom",
                description = "Become invisible for 8 seconds",
                path = SkillTreePath.Ranger,
                requiredLevel = 40,
                skillPointCost = 3,
                prerequisiteSkills = new List<int> { 17 },
                effect = new SkillEffect { speedBonus = 50, specialAbility = "Phantom" }
            };

            allSkills[20] = new Skill
            {
                skillId = 20,
                skillName = "Deadeye",
                description = "Ultimate ranger ability",
                path = SkillTreePath.Ranger,
                requiredLevel = 50,
                skillPointCost = 3,
                prerequisiteSkills = new List<int> { 18, 19 },
                effect = new SkillEffect { damageBonus = 80, speedBonus = 40, specialAbility = "Deadeye" }
            };

            // MAGE TREE (10 skills)
            skillTrees[SkillTreePath.Mage] = new SkillTree { path = SkillTreePath.Mage };

            allSkills[21] = new Skill
            {
                skillId = 21,
                skillName = "Fireball",
                description = "Cast fireball dealing area damage",
                path = SkillTreePath.Mage,
                requiredLevel = 1,
                skillPointCost = 1,
                effect = new SkillEffect { damageBonus = 25, manaBonus = 20, specialAbility = "Fireball" }
            };

            allSkills[22] = new Skill
            {
                skillId = 22,
                skillName = "Frost Bolt",
                description = "Freeze enemies in place",
                path = SkillTreePath.Mage,
                requiredLevel = 5,
                skillPointCost = 1,
                prerequisiteSkills = new List<int> { 21 },
                effect = new SkillEffect { damageBonus = 20, manaBonus = 15, specialAbility = "FrostBolt" }
            };

            allSkills[23] = new Skill
            {
                skillId = 23,
                skillName = "Lightning Storm",
                description = "Chain lightning to multiple enemies",
                path = SkillTreePath.Mage,
                requiredLevel = 10,
                skillPointCost = 2,
                prerequisiteSkills = new List<int> { 21 },
                effect = new SkillEffect { damageBonus = 35, manaBonus = 30, specialAbility = "LightningStorm" }
            };

            allSkills[24] = new Skill
            {
                skillId = 24,
                skillName = "Meteor",
                description = "Call meteors from the sky",
                path = SkillTreePath.Mage,
                requiredLevel = 15,
                skillPointCost = 2,
                prerequisiteSkills = new List<int> { 23 },
                effect = new SkillEffect { damageBonus = 50, manaBonus = 40, specialAbility = "Meteor" }
            };

            allSkills[25] = new Skill
            {
                skillId = 25,
                skillName = "Mana Shield",
                description = "Absorb damage with mana",
                path = SkillTreePath.Mage,
                requiredLevel = 20,
                skillPointCost = 2,
                prerequisiteSkills = new List<int> { 22 },
                effect = new SkillEffect { armorBonus = 25, manaBonus = 50, specialAbility = "ManaShield" }
            };

            allSkills[26] = new Skill
            {
                skillId = 26,
                skillName = "Time Warp",
                description = "Slow time for 5 seconds",
                path = SkillTreePath.Mage,
                requiredLevel = 25,
                skillPointCost = 2,
                prerequisiteSkills = new List<int> { 23 },
                effect = new SkillEffect { manaBonus = 35, specialAbility = "TimeWarp" }
            };

            allSkills[27] = new Skill
            {
                skillId = 27,
                skillName = "Teleport",
                description = "Teleport to any location",
                path = SkillTreePath.Mage,
                requiredLevel = 30,
                skillPointCost = 2,
                prerequisiteSkills = new List<int> { 25 },
                effect = new SkillEffect { speedBonus = 25, manaBonus = 30, specialAbility = "Teleport" }
            };

            allSkills[28] = new Skill
            {
                skillId = 28,
                skillName = "Inferno",
                description = "Massive fire explosion",
                path = SkillTreePath.Mage,
                requiredLevel = 35,
                skillPointCost = 2,
                prerequisiteSkills = new List<int> { 24 },
                effect = new SkillEffect { damageBonus = 60, manaBonus = 50, specialAbility = "Inferno" }
            };

            allSkills[29] = new Skill
            {
                skillId = 29,
                skillName = "Arcane Mastery",
                description = "Master all arcane magic",
                path = SkillTreePath.Mage,
                requiredLevel = 40,
                skillPointCost = 3,
                prerequisiteSkills = new List<int> { 27 },
                effect = new SkillEffect { manaBonus = 100, cooldownReduction = 0.3f, specialAbility = "ArcaneMastery" }
            };

            allSkills[30] = new Skill
            {
                skillId = 30,
                skillName = "Archmage",
                description = "Ultimate mage ability",
                path = SkillTreePath.Mage,
                requiredLevel = 50,
                skillPointCost = 3,
                prerequisiteSkills = new List<int> { 28, 29 },
                effect = new SkillEffect { damageBonus = 70, manaBonus = 80, specialAbility = "Archmage" }
            };

            Debug.Log("Skill trees initialized with 30 skills (3 trees x 10 skills)");
        }

        /// <summary>
        /// Unlock a skill.
        /// </summary>
        public bool UnlockSkill(int skillId)
        {
            if (!allSkills.ContainsKey(skillId))
            {
                Debug.LogError($"Skill {skillId} not found");
                return false;
            }

            Skill skill = allSkills[skillId];

            // Check prerequisites
            foreach (var prereqId in skill.prerequisiteSkills)
            {
                if (!allSkills[prereqId].unlocked)
                {
                    Debug.LogWarning($"Prerequisite skill {prereqId} not unlocked");
                    return false;
                }
            }

            // Check level
            if (PlayerStats.Instance.Level < skill.requiredLevel)
            {
                Debug.LogWarning($"Player level {PlayerStats.Instance.Level} is below required {skill.requiredLevel}");
                return false;
            }

            // Check skill points
            if (totalSkillPoints < skill.skillPointCost)
            {
                Debug.LogWarning($"Not enough skill points. Need {skill.skillPointCost}, have {totalSkillPoints}");
                return false;
            }

            skill.unlocked = true;
            totalSkillPoints -= skill.skillPointCost;

            // Apply skill effects
            ApplySkillEffect(skill);

            Debug.Log($"Skill unlocked: {skill.skillName}");
            OnSkillUnlocked?.Invoke(skill);

            return true;
        }

        /// <summary>
        /// Apply skill effects to player.
        /// </summary>
        private void ApplySkillEffect(Skill skill)
        {
            PlayerStats stats = PlayerStats.Instance;
            SkillEffect effect = skill.effect;

            if (effect.damageBonus > 0)
                stats.AddDamageBonus(effect.damageBonus);
            if (effect.armorBonus > 0)
                stats.AddArmorBonus(effect.armorBonus);
            if (effect.hpBonus > 0)
                stats.AddHPBonus(effect.hpBonus);
            if (effect.speedBonus > 0)
                stats.AddSpeedBonus(effect.speedBonus);
            if (effect.manaBonus > 0)
                stats.AddManaBonus(effect.manaBonus);
        }

        /// <summary>
        /// Get skill by ID.
        /// </summary>
        public Skill GetSkill(int skillId)
        {
            if (allSkills.ContainsKey(skillId))
                return allSkills[skillId];
            return null;
        }

        /// <summary>
        /// Get all skills for a tree.
        /// </summary>
        public List<Skill> GetSkillsForTree(SkillTreePath path)
        {
            List<Skill> result = new();
            foreach (var skill in allSkills.Values)
            {
                if (skill.path == path)
                    result.Add(skill);
            }
            return result;
        }

        /// <summary>
        /// Get unlocked skills.
        /// </summary>
        public List<Skill> GetUnlockedSkills()
        {
            List<Skill> result = new();
            foreach (var skill in allSkills.Values)
            {
                if (skill.unlocked)
                    result.Add(skill);
            }
            return result;
        }

        /// <summary>
        /// Add skill points.
        /// </summary>
        public void AddSkillPoints(int amount)
        {
            totalSkillPoints += amount;
            Debug.Log($"Added {amount} skill points. Total: {totalSkillPoints}");
            OnSkillPointsAdded?.Invoke(totalSkillPoints);
        }

        /// <summary>
        /// Get available skill points.
        /// </summary>
        public int GetAvailableSkillPoints()
        {
            return totalSkillPoints;
        }

        // Events
        public event Action<Skill> OnSkillUnlocked;
        public event Action<int> OnSkillPointsAdded;
    }
}
