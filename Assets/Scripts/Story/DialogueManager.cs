using UnityEngine;
using System.Collections.Generic;
using System;

namespace HackSlash.Story
{
    /// <summary>
    /// Manages dialogue interactions with NPCs.
    /// Handles dialogue trees, choices, and NPC conversations.
    /// </summary>
    public class DialogueManager : MonoBehaviour
    {
        public static DialogueManager Instance { get; private set; }

        [System.Serializable]
        public class DialogueNode
        {
            public int nodeId;
            public string characterName;
            public string dialogueText;
            public List<DialogueChoice> choices = new();
            public int nextNodeId = -1; // -1 = end dialogue
        }

        [System.Serializable]
        public class DialogueChoice
        {
            public int choiceId;
            public string choiceText;
            public int nextNodeId;
            public int questRewardId;
            public int xpReward;
        }

        [System.Serializable]
        public class NPC
        {
            public int npcId;
            public string npcName;
            public int firstDialogueNodeId;
            public List<int> questIds = new();
            public string description;
        }

        private Dictionary<int, DialogueNode> dialogueNodes = new();
        private Dictionary<int, NPC> npcs = new();
        private int currentNodeId = -1;
        private bool dialogueActive = false;

        void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeDialogues();
        }

        /// <summary>
        /// Initialize all NPCs and dialogue trees.
        /// </summary>
        private void InitializeDialogues()
        {
            // NPC 1: Village Elder
            npcs[1] = new NPC
            {
                npcId = 1,
                npcName = "Village Elder",
                firstDialogueNodeId = 1,
                description = "The wise leader of the village"
            };

            // Dialogue nodes for Village Elder
            dialogueNodes[1] = new DialogueNode
            {
                nodeId = 1,
                characterName = "Village Elder",
                dialogueText = "Welcome, brave warrior. The village is under attack by dark forces.",
                choices = new List<DialogueChoice>
                {
                    new DialogueChoice { choiceId = 1, choiceText = "I'll help defend the village", nextNodeId = 2, xpReward = 100 },
                    new DialogueChoice { choiceId = 2, choiceText = "Tell me more about the threat", nextNodeId = 3, xpReward = 50 }
                }
            };

            dialogueNodes[2] = new DialogueNode
            {
                nodeId = 2,
                characterName = "Village Elder",
                dialogueText = "Thank you! Your courage gives us hope. Please defend the village from the incoming waves of enemies.",
                nextNodeId = -1
            };

            dialogueNodes[3] = new DialogueNode
            {
                nodeId = 3,
                characterName = "Village Elder",
                dialogueText = "Dark creatures have emerged from the forest. They destroy everything in their path. We need a hero to stop them.",
                choices = new List<DialogueChoice>
                {
                    new DialogueChoice { choiceId = 3, choiceText = "I will stop them", nextNodeId = 2, xpReward = 100 }
                }
            };

            // NPC 2: Blacksmith
            npcs[2] = new NPC
            {
                npcId = 2,
                npcName = "Blacksmith",
                firstDialogueNodeId = 10,
                description = "A skilled craftsman who forges weapons"
            };

            dialogueNodes[10] = new DialogueNode
            {
                nodeId = 10,
                characterName = "Blacksmith",
                dialogueText = "Greetings, warrior! I can forge powerful weapons for you.",
                choices = new List<DialogueChoice>
                {
                    new DialogueChoice { choiceId = 4, choiceText = "Show me your wares", nextNodeId = 11, xpReward = 0 },
                    new DialogueChoice { choiceId = 5, choiceText = "Maybe later", nextNodeId = -1, xpReward = 0 }
                }
            };

            dialogueNodes[11] = new DialogueNode
            {
                nodeId = 11,
                characterName = "Blacksmith",
                dialogueText = "I have the finest weapons in the realm. Visit my shop to see what I have in stock.",
                nextNodeId = -1
            };

            // NPC 3: Merchant
            npcs[3] = new NPC
            {
                npcId = 3,
                npcName = "Merchant",
                firstDialogueNodeId = 20,
                description = "A traveling merchant with rare goods"
            };

            dialogueNodes[20] = new DialogueNode
            {
                nodeId = 20,
                characterName = "Merchant",
                dialogueText = "Welcome to my humble shop! I have rare items for sale.",
                choices = new List<DialogueChoice>
                {
                    new DialogueChoice { choiceId = 6, choiceText = "Show me your inventory", nextNodeId = 21, xpReward = 0 },
                    new DialogueChoice { choiceId = 7, choiceText = "I'm just browsing", nextNodeId = -1, xpReward = 0 }
                }
            };

            dialogueNodes[21] = new DialogueNode
            {
                nodeId = 21,
                characterName = "Merchant",
                dialogueText = "I have potions, scrolls, and rare artifacts. Take a look!",
                nextNodeId = -1
            };

            // NPC 4: Healer
            npcs[4] = new NPC
            {
                npcId = 4,
                npcName = "Healer",
                firstDialogueNodeId = 30,
                description = "A skilled healer who mends wounds"
            };

            dialogueNodes[30] = new DialogueNode
            {
                nodeId = 30,
                characterName = "Healer",
                dialogueText = "I see you're injured. Let me help you recover.",
                choices = new List<DialogueChoice>
                {
                    new DialogueChoice { choiceId = 8, choiceText = "Yes, please heal me", nextNodeId = 31, xpReward = 0 },
                    new DialogueChoice { choiceId = 9, choiceText = "I'm fine, thank you", nextNodeId = -1, xpReward = 0 }
                }
            };

            dialogueNodes[31] = new DialogueNode
            {
                nodeId = 31,
                characterName = "Healer",
                dialogueText = "There you go! You're as good as new.",
                nextNodeId = -1
            };

            // NPC 5: Warrior
            npcs[5] = new NPC
            {
                npcId = 5,
                npcName = "Warrior",
                firstDialogueNodeId = 40,
                description = "A seasoned warrior with battle scars"
            };

            dialogueNodes[40] = new DialogueNode
            {
                nodeId = 40,
                characterName = "Warrior",
                dialogueText = "You look like you can handle yourself in a fight. Want some combat tips?",
                choices = new List<DialogueChoice>
                {
                    new DialogueChoice { choiceId = 10, choiceText = "Yes, teach me", nextNodeId = 41, xpReward = 200 },
                    new DialogueChoice { choiceId = 11, choiceText = "I'm experienced enough", nextNodeId = -1, xpReward = 0 }
                }
            };

            dialogueNodes[41] = new DialogueNode
            {
                nodeId = 41,
                characterName = "Warrior",
                dialogueText = "Always aim for the weak points. Use your dash to evade attacks. And never underestimate your enemies.",
                nextNodeId = -1
            };

            Debug.Log("Dialogues initialized with 5 NPCs");
        }

        /// <summary>
        /// Start dialogue with an NPC.
        /// </summary>
        public void StartDialogue(int npcId)
        {
            if (!npcs.ContainsKey(npcId))
            {
                Debug.LogError($"NPC {npcId} not found");
                return;
            }

            NPC npc = npcs[npcId];
            currentNodeId = npc.firstDialogueNodeId;
            dialogueActive = true;

            Debug.Log($"Starting dialogue with {npc.npcName}");
            OnDialogueStarted?.Invoke(npc);
            ShowCurrentNode();
        }

        /// <summary>
        /// Show current dialogue node.
        /// </summary>
        private void ShowCurrentNode()
        {
            if (!dialogueNodes.ContainsKey(currentNodeId))
            {
                EndDialogue();
                return;
            }

            DialogueNode node = dialogueNodes[currentNodeId];
            OnDialogueNodeShown?.Invoke(node);
        }

        /// <summary>
        /// Select a dialogue choice.
        /// </summary>
        public void SelectChoice(int choiceId)
        {
            if (!dialogueActive || !dialogueNodes.ContainsKey(currentNodeId))
                return;

            DialogueNode node = dialogueNodes[currentNodeId];
            DialogueChoice choice = node.choices.Find(c => c.choiceId == choiceId);

            if (choice == null)
            {
                Debug.LogWarning($"Choice {choiceId} not found");
                return;
            }

            // Award XP if applicable
            if (choice.xpReward > 0)
                PlayerStats.Instance.AddExperience(choice.xpReward);

            // Move to next node
            if (choice.nextNodeId == -1)
            {
                EndDialogue();
            }
            else
            {
                currentNodeId = choice.nextNodeId;
                ShowCurrentNode();
            }

            OnChoiceSelected?.Invoke(choice);
        }

        /// <summary>
        /// End current dialogue.
        /// </summary>
        public void EndDialogue()
        {
            dialogueActive = false;
            currentNodeId = -1;
            Debug.Log("Dialogue ended");
            OnDialogueEnded?.Invoke();
        }

        /// <summary>
        /// Get NPC by ID.
        /// </summary>
        public NPC GetNPC(int npcId)
        {
            if (npcs.ContainsKey(npcId))
                return npcs[npcId];
            return null;
        }

        /// <summary>
        /// Get all NPCs.
        /// </summary>
        public List<NPC> GetAllNPCs()
        {
            return new List<NPC>(npcs.Values);
        }

        /// <summary>
        /// Check if dialogue is active.
        /// </summary>
        public bool IsDialogueActive()
        {
            return dialogueActive;
        }

        /// <summary>
        /// Get current dialogue node.
        /// </summary>
        public DialogueNode GetCurrentNode()
        {
            if (dialogueNodes.ContainsKey(currentNodeId))
                return dialogueNodes[currentNodeId];
            return null;
        }

        // Events
        public event Action<NPC> OnDialogueStarted;
        public event Action OnDialogueEnded;
        public event Action<DialogueNode> OnDialogueNodeShown;
        public event Action<DialogueChoice> OnChoiceSelected;
    }
}
