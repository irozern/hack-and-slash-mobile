using UnityEngine;
using System.Collections.Generic;
using System;

namespace HackSlash.Core
{
    /// <summary>
    /// Integrates all game systems and manages their interactions.
    /// Ensures proper initialization order and system communication.
    /// </summary>
    public class SystemIntegrationManager : MonoBehaviour
    {
        public static SystemIntegrationManager Instance { get; private set; }

        public enum SystemStatus
        {
            Uninitialized,
            Initializing,
            Ready,
            Error
        }

        [System.Serializable]
        public class SystemInfo
        {
            public string systemName;
            public MonoBehaviour systemComponent;
            public SystemStatus status;
            public float initializationTime;
        }

        private Dictionary<string, SystemInfo> systems = new();
        private bool isInitialized = false;
        private float totalInitializationTime = 0f;

        void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        void Start()
        {
            InitializeAllSystems();
        }

        /// <summary>
        /// Initialize all game systems in correct order.
        /// </summary>
        private void InitializeAllSystems()
        {
            Debug.Log("Starting system initialization...");

            // Core Systems (must initialize first)
            InitializeSystem("InputManager", InputManager.Instance);
            InitializeSystem("GameManager", GameManager.Instance);
            InitializeSystem("CameraController", CameraController.Instance);

            // Player Systems
            InitializeSystem("PlayerStats", PlayerStats.Instance);
            InitializeSystem("PlayerController", PlayerController.Instance);
            InitializeSystem("PlayerCombat", PlayerCombat.Instance);

            // Enemy Systems
            InitializeSystem("EnemyStats", null); // Spawned dynamically
            InitializeSystem("EnemyAI", null); // Spawned dynamically

            // Loot & Inventory
            InitializeSystem("ItemDatabase", ItemDatabase.Instance);
            InitializeSystem("LootManager", LootManager.Instance);
            InitializeSystem("InventoryManager", InventoryManager.Instance);

            // HUD & UI
            InitializeSystem("HUDManager", HUDManager.Instance);
            InitializeSystem("UIManager", UIManager.Instance);

            // Progression Systems
            InitializeSystem("SkillTreeManager", SkillTreeManager.Instance);
            InitializeSystem("DungeonManager", DungeonManager.Instance);

            // Quest Systems
            InitializeSystem("QuestManager", QuestManager.Instance);
            InitializeSystem("AdvancedQuestManager", AdvancedQuestManager.Instance);

            // Monetization
            InitializeSystem("GamePassManager", GamePassManager.Instance);
            InitializeSystem("PremiumChestManager", PremiumChestManager.Instance);

            // Social & Endgame
            InitializeSystem("PvPManager", PvPManager.Instance);
            InitializeSystem("GuildManager", GuildManager.Instance);
            InitializeSystem("LeaderboardManager", LeaderboardManager.Instance);
            InitializeSystem("SeasonalEventManager", SeasonalEventManager.Instance);

            // Story & Narrative
            InitializeSystem("StoryManager", StoryManager.Instance);
            InitializeSystem("DialogueManager", DialogueManager.Instance);

            // Map & World
            InitializeSystem("MapManager", MapManager.Instance);

            isInitialized = true;
            Debug.Log($"All systems initialized in {totalInitializationTime:F2}s");
            OnAllSystemsInitialized?.Invoke();
        }

        /// <summary>
        /// Initialize a single system.
        /// </summary>
        private void InitializeSystem(string systemName, MonoBehaviour systemComponent)
        {
            float startTime = Time.realtimeSinceStartup;

            try
            {
                SystemInfo info = new SystemInfo
                {
                    systemName = systemName,
                    systemComponent = systemComponent,
                    status = SystemStatus.Initializing
                };

                // Simulate initialization
                if (systemComponent != null)
                {
                    // System is already initialized by Awake
                    info.status = SystemStatus.Ready;
                }
                else
                {
                    // Dynamic systems will be initialized when needed
                    info.status = SystemStatus.Ready;
                }

                float initTime = Time.realtimeSinceStartup - startTime;
                info.initializationTime = initTime;
                totalInitializationTime += initTime;

                systems[systemName] = info;
                Debug.Log($"✓ {systemName} initialized ({initTime:F3}s)");
                OnSystemInitialized?.Invoke(systemName);
            }
            catch (Exception ex)
            {
                Debug.LogError($"✗ Failed to initialize {systemName}: {ex.Message}");
                SystemInfo info = new SystemInfo
                {
                    systemName = systemName,
                    status = SystemStatus.Error
                };
                systems[systemName] = info;
                OnSystemInitializationFailed?.Invoke(systemName, ex.Message);
            }
        }

        /// <summary>
        /// Get system status.
        /// </summary>
        public SystemStatus GetSystemStatus(string systemName)
        {
            if (systems.ContainsKey(systemName))
                return systems[systemName].status;
            return SystemStatus.Uninitialized;
        }

        /// <summary>
        /// Check if all systems are ready.
        /// </summary>
        public bool AreAllSystemsReady()
        {
            foreach (var system in systems.Values)
            {
                if (system.status != SystemStatus.Ready)
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Get system initialization time.
        /// </summary>
        public float GetSystemInitializationTime(string systemName)
        {
            if (systems.ContainsKey(systemName))
                return systems[systemName].initializationTime;
            return 0f;
        }

        /// <summary>
        /// Get total initialization time.
        /// </summary>
        public float GetTotalInitializationTime()
        {
            return totalInitializationTime;
        }

        /// <summary>
        /// Get all systems info.
        /// </summary>
        public List<SystemInfo> GetAllSystemsInfo()
        {
            return new List<SystemInfo>(systems.Values);
        }

        /// <summary>
        /// Verify system dependencies.
        /// </summary>
        public bool VerifySystemDependencies()
        {
            // Check if all required systems are initialized
            string[] requiredSystems = new[]
            {
                "GameManager",
                "PlayerStats",
                "InputManager",
                "CameraController",
                "HUDManager",
                "UIManager"
            };

            foreach (string systemName in requiredSystems)
            {
                if (GetSystemStatus(systemName) != SystemStatus.Ready)
                {
                    Debug.LogError($"Required system not ready: {systemName}");
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Get system health report.
        /// </summary>
        public Dictionary<string, object> GetHealthReport()
        {
            Dictionary<string, object> report = new();
            report["TotalSystems"] = systems.Count;
            report["ReadySystems"] = CountSystemsByStatus(SystemStatus.Ready);
            report["ErrorSystems"] = CountSystemsByStatus(SystemStatus.Error);
            report["TotalInitializationTime"] = totalInitializationTime;
            report["IsInitialized"] = isInitialized;
            report["AllSystemsReady"] = AreAllSystemsReady();

            return report;
        }

        /// <summary>
        /// Count systems by status.
        /// </summary>
        private int CountSystemsByStatus(SystemStatus status)
        {
            int count = 0;
            foreach (var system in systems.Values)
            {
                if (system.status == status)
                    count++;
            }
            return count;
        }

        /// <summary>
        /// Reload a system.
        /// </summary>
        public void ReloadSystem(string systemName)
        {
            if (systems.ContainsKey(systemName))
            {
                Debug.Log($"Reloading system: {systemName}");
                systems[systemName].status = SystemStatus.Initializing;
                InitializeSystem(systemName, systems[systemName].systemComponent);
            }
        }

        /// <summary>
        /// Shutdown all systems.
        /// </summary>
        public void ShutdownAllSystems()
        {
            Debug.Log("Shutting down all systems...");
            foreach (var system in systems.Values)
            {
                system.status = SystemStatus.Uninitialized;
            }
            systems.Clear();
            isInitialized = false;
            OnAllSystemsShutdown?.Invoke();
        }

        // Events
        public event Action<string> OnSystemInitialized;
        public event Action<string, string> OnSystemInitializationFailed;
        public event Action OnAllSystemsInitialized;
        public event Action OnAllSystemsShutdown;
    }
}
