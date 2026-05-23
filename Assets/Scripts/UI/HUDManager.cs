using UnityEngine;
using TMPro;
using UnityEngine.UI;

/// <summary>
/// Manages the main HUD display including health, mana, level, and action bar.
/// </summary>
public class HUDManager : MonoBehaviour
{
    public static HUDManager Instance { get; private set; }

    [Header("Health & Mana")]
    [SerializeField] private Image healthBar;
    [SerializeField] private Image manaBar;
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private TextMeshProUGUI manaText;

    [Header("Experience")]
    [SerializeField] private Image experienceBar;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI experienceText;

    [Header("Action Bar")]
    [SerializeField] private Image[] actionBarIcons;
    [SerializeField] private Image[] cooldownOverlays;

    [Header("Stats Display")]
    [SerializeField] private TextMeshProUGUI damageText;
    [SerializeField] private TextMeshProUGUI armorText;
    [SerializeField] private TextMeshProUGUI speedText;

    private PlayerStats playerStats;
    private PlayerCombat playerCombat;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        GameObject playerObj = GameManager.Instance.GetPlayer();
        if (playerObj != null)
        {
            playerStats = playerObj.GetComponent<PlayerStats>();
            playerCombat = playerObj.GetComponent<PlayerCombat>();

            // Subscribe to stat changes
            if (playerStats != null)
            {
                playerStats.OnHealthChanged += UpdateHealthBar;
                playerStats.OnManaChanged += UpdateManaBar;
                playerStats.OnLevelUp += UpdateLevelDisplay;
            }
        }

        UpdateAllStats();
    }

    private void Update()
    {
        UpdateCooldowns();
    }

    /// <summary>
    /// Updates health bar and text.
    /// </summary>
    private void UpdateHealthBar(float current, float max)
    {
        if (healthBar != null)
            healthBar.fillAmount = current / max;

        if (healthText != null)
            healthText.text = $"{Mathf.RoundToInt(current)} / {Mathf.RoundToInt(max)}";
    }

    /// <summary>
    /// Updates mana bar and text.
    /// </summary>
    private void UpdateManaBar(float current, float max)
    {
        if (manaBar != null)
            manaBar.fillAmount = current / max;

        if (manaText != null)
            manaText.text = $"{Mathf.RoundToInt(current)} / {Mathf.RoundToInt(max)}";
    }

    /// <summary>
    /// Updates experience bar and level display.
    /// </summary>
    private void UpdateLevelDisplay(int level)
    {
        if (levelText != null)
            levelText.text = $"LVL {level}";

        if (experienceBar != null && playerStats != null)
            experienceBar.fillAmount = playerStats.GetExperienceProgress();
    }

    /// <summary>
    /// Updates cooldown overlays for action bar.
    /// </summary>
    private void UpdateCooldowns()
    {
        if (playerCombat == null) return;

        float attackCooldown = playerCombat.GetAttackCooldownProgress();

        if (cooldownOverlays.Length > 0 && cooldownOverlays[0] != null)
        {
            cooldownOverlays[0].fillAmount = 1f - attackCooldown;
        }
    }

    /// <summary>
    /// Updates all stat displays.
    /// </summary>
    private void UpdateAllStats()
    {
        if (playerStats == null) return;

        if (damageText != null)
            damageText.text = $"DMG: {playerStats.GetDamage():F1}";

        if (armorText != null)
            armorText.text = $"ARM: {playerStats.GetArmor():F1}";

        if (speedText != null)
            speedText.text = $"SPD: {playerStats.GetMoveSpeed():F1}";

        UpdateHealthBar(playerStats.GetCurrentHealth(), playerStats.GetMaxHealth());
        UpdateManaBar(playerStats.GetCurrentMana(), playerStats.GetMaxMana());
        UpdateLevelDisplay(playerStats.GetLevel());
    }

    /// <summary>
    /// Shows a temporary notification on screen.
    /// </summary>
    public void ShowNotification(string message, float duration = 2f)
    {
        // TODO: Implement notification system
        Debug.Log($"[Notification] {message}");
    }

    /// <summary>
    /// Shows damage popup at world position.
    /// </summary>
    public void ShowDamagePopup(Vector3 worldPosition, float damage, bool isCritical = false)
    {
        // TODO: Instantiate damage number prefab
    }
}
