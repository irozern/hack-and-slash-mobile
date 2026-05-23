using UnityEngine;
using System.Collections.Generic;
using System;

namespace HackSlash.UI
{
    /// <summary>
    /// Centralized UI management system for all game screens and panels.
    /// Handles UI state, transitions, and event management.
    /// </summary>
    public class UIManager : MonoBehaviour
    {
        public static UIManager Instance { get; private set; }

        public enum UIScreen
        {
            MainMenu,
            HUD,
            Inventory,
            Character,
            Skills,
            Quests,
            Map,
            PvP,
            Guild,
            Leaderboard,
            Events,
            Shop,
            Settings,
            Pause
        }

        [System.Serializable]
        public class UIPanel
        {
            public UIScreen screenType;
            public GameObject panelObject;
            public CanvasGroup canvasGroup;
            public bool isActive;
        }

        private Dictionary<UIScreen, UIPanel> uiPanels = new();
        private UIScreen currentScreen = UIScreen.MainMenu;
        private Stack<UIScreen> screenHistory = new();

        void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeUI();
        }

        /// <summary>
        /// Initialize UI system.
        /// </summary>
        private void InitializeUI()
        {
            Debug.Log("UI system initialized");
        }

        /// <summary>
        /// Register a UI panel.
        /// </summary>
        public void RegisterPanel(UIScreen screenType, GameObject panelObject)
        {
            if (uiPanels.ContainsKey(screenType))
            {
                Debug.LogWarning($"Panel {screenType} already registered");
                return;
            }

            CanvasGroup canvasGroup = panelObject.GetComponent<CanvasGroup>();
            if (canvasGroup == null)
            {
                canvasGroup = panelObject.AddComponent<CanvasGroup>();
            }

            UIPanel panel = new UIPanel
            {
                screenType = screenType,
                panelObject = panelObject,
                canvasGroup = canvasGroup,
                isActive = false
            };

            uiPanels[screenType] = panel;
            panelObject.SetActive(false);

            Debug.Log($"Panel registered: {screenType}");
        }

        /// <summary>
        /// Show a UI screen.
        /// </summary>
        public void ShowScreen(UIScreen screenType, bool addToHistory = true)
        {
            if (!uiPanels.ContainsKey(screenType))
            {
                Debug.LogWarning($"Panel {screenType} not registered");
                return;
            }

            // Hide current screen
            if (currentScreen != screenType)
            {
                HideScreen(currentScreen);
                if (addToHistory)
                {
                    screenHistory.Push(currentScreen);
                }
            }

            UIPanel panel = uiPanels[screenType];
            panel.panelObject.SetActive(true);
            panel.canvasGroup.alpha = 1f;
            panel.isActive = true;
            currentScreen = screenType;

            Debug.Log($"Screen shown: {screenType}");
            OnScreenChanged?.Invoke(screenType);
        }

        /// <summary>
        /// Hide a UI screen.
        /// </summary>
        public void HideScreen(UIScreen screenType)
        {
            if (!uiPanels.ContainsKey(screenType))
                return;

            UIPanel panel = uiPanels[screenType];
            panel.panelObject.SetActive(false);
            panel.canvasGroup.alpha = 0f;
            panel.isActive = false;

            Debug.Log($"Screen hidden: {screenType}");
        }

        /// <summary>
        /// Go back to previous screen.
        /// </summary>
        public void GoBack()
        {
            if (screenHistory.Count == 0)
                return;

            UIScreen previousScreen = screenHistory.Pop();
            ShowScreen(previousScreen, false);
        }

        /// <summary>
        /// Toggle a UI screen.
        /// </summary>
        public void ToggleScreen(UIScreen screenType)
        {
            if (currentScreen == screenType)
            {
                GoBack();
            }
            else
            {
                ShowScreen(screenType);
            }
        }

        /// <summary>
        /// Get current screen.
        /// </summary>
        public UIScreen GetCurrentScreen()
        {
            return currentScreen;
        }

        /// <summary>
        /// Check if screen is active.
        /// </summary>
        public bool IsScreenActive(UIScreen screenType)
        {
            if (uiPanels.ContainsKey(screenType))
                return uiPanels[screenType].isActive;
            return false;
        }

        /// <summary>
        /// Fade screen in.
        /// </summary>
        public void FadeScreenIn(UIScreen screenType, float duration = 0.5f)
        {
            if (!uiPanels.ContainsKey(screenType))
                return;

            UIPanel panel = uiPanels[screenType];
            StartCoroutine(FadeCoroutine(panel.canvasGroup, 0f, 1f, duration));
        }

        /// <summary>
        /// Fade screen out.
        /// </summary>
        public void FadeScreenOut(UIScreen screenType, float duration = 0.5f)
        {
            if (!uiPanels.ContainsKey(screenType))
                return;

            UIPanel panel = uiPanels[screenType];
            StartCoroutine(FadeCoroutine(panel.canvasGroup, 1f, 0f, duration));
        }

        /// <summary>
        /// Fade coroutine.
        /// </summary>
        private System.Collections.IEnumerator FadeCoroutine(CanvasGroup canvasGroup, float startAlpha, float endAlpha, float duration)
        {
            float elapsed = 0f;
            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;
                canvasGroup.alpha = Mathf.Lerp(startAlpha, endAlpha, elapsed / duration);
                yield return null;
            }
            canvasGroup.alpha = endAlpha;
        }

        /// <summary>
        /// Show notification.
        /// </summary>
        public void ShowNotification(string title, string message, float duration = 3f)
        {
            Debug.Log($"Notification: {title} - {message}");
            OnNotificationShown?.Invoke(title, message);
            // TODO: Implement notification UI
        }

        /// <summary>
        /// Show dialog.
        /// </summary>
        public void ShowDialog(string title, string message, string confirmText = "OK", string cancelText = "Cancel", Action onConfirm = null, Action onCancel = null)
        {
            Debug.Log($"Dialog: {title} - {message}");
            OnDialogShown?.Invoke(title, message);
            // TODO: Implement dialog UI
        }

        /// <summary>
        /// Show loading screen.
        /// </summary>
        public void ShowLoadingScreen(string message = "Loading...")
        {
            Debug.Log($"Loading: {message}");
            ShowScreen(UIScreen.MainMenu);
            OnLoadingStarted?.Invoke(message);
        }

        /// <summary>
        /// Hide loading screen.
        /// </summary>
        public void HideLoadingScreen()
        {
            Debug.Log("Loading complete");
            OnLoadingCompleted?.Invoke();
        }

        // Events
        public event Action<UIScreen> OnScreenChanged;
        public event Action<string, string> OnNotificationShown;
        public event Action<string, string> OnDialogShown;
        public event Action<string> OnLoadingStarted;
        public event Action OnLoadingCompleted;
    }
}
