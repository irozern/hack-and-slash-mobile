using UnityEngine;
using System;

/// <summary>
/// Manages Game Pass subscription and premium features.
/// Handles XP boost, premium currency, and seasonal content.
/// </summary>
public class GamePassManager : MonoBehaviour
{
    public static GamePassManager Instance { get; private set; }

    [SerializeField] private bool hasActivePass = false;
    [SerializeField] private float passExpirationTime = 0f;
    [SerializeField] private int premiumCurrency = 0;

    // Events
    public event Action<bool> OnGamePassStatusChanged;
    public event Action<float> OnPassTimeRemaining;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        LoadGamePassData();
    }

    private void Update()
    {
        if (!hasActivePass) return;

        // Check if pass expired
        if (Time.time > passExpirationTime)
        {
            ExpireGamePass();
        }
        else
        {
            // Update remaining time
            float timeRemaining = passExpirationTime - Time.time;
            OnPassTimeRemaining?.Invoke(timeRemaining);
        }
    }

    /// <summary>
    /// Purchases a Game Pass.
    /// </summary>
    public void PurchaseGamePass()
    {
        // TODO: Integrate with Google Play Billing / Apple IAP
        ActivateGamePass();
    }

    /// <summary>
    /// Activates Game Pass benefits.
    /// </summary>
    private void ActivateGamePass()
    {
        hasActivePass = true;
        passExpirationTime = Time.time + (Constants.Monetization.GAME_PASS_DURATION_DAYS * 86400f);

        OnGamePassStatusChanged?.Invoke(true);
        SaveGamePassData();

        Debug.Log($"Game Pass activated! Expires in {Constants.Monetization.GAME_PASS_DURATION_DAYS} days");
    }

    /// <summary>
    /// Expires the current Game Pass.
    /// </summary>
    private void ExpireGamePass()
    {
        hasActivePass = false;
        OnGamePassStatusChanged?.Invoke(false);
        SaveGamePassData();

        Debug.Log("Game Pass expired!");
    }

    /// <summary>
    /// Gets XP multiplier based on Game Pass status.
    /// </summary>
    public float GetXPMultiplier()
    {
        if (hasActivePass)
            return Constants.Monetization.XP_BOOST_MULTIPLIER;
        return 1f;
    }

    /// <summary>
    /// Gets currency multiplier based on Game Pass status.
    /// </summary>
    public float GetCurrencyMultiplier()
    {
        if (hasActivePass)
            return Constants.Monetization.PREMIUM_CURRENCY_MULTIPLIER;
        return 1f;
    }

    /// <summary>
    /// Adds premium currency.
    /// </summary>
    public void AddPremiumCurrency(int amount)
    {
        premiumCurrency += amount;
        SaveGamePassData();
    }

    /// <summary>
    /// Spends premium currency.
    /// </summary>
    public bool SpendPremiumCurrency(int amount)
    {
        if (premiumCurrency >= amount)
        {
            premiumCurrency -= amount;
            SaveGamePassData();
            return true;
        }
        return false;
    }

    // ==================== PERSISTENCE ====================

    private void SaveGamePassData()
    {
        PlayerPrefs.SetInt("GamePass_Active", hasActivePass ? 1 : 0);
        PlayerPrefs.SetFloat("GamePass_Expiration", passExpirationTime);
        PlayerPrefs.SetInt("PremiumCurrency", premiumCurrency);
        PlayerPrefs.Save();
    }

    private void LoadGamePassData()
    {
        hasActivePass = PlayerPrefs.GetInt("GamePass_Active", 0) == 1;
        passExpirationTime = PlayerPrefs.GetFloat("GamePass_Expiration", 0f);
        premiumCurrency = PlayerPrefs.GetInt("PremiumCurrency", 0);
    }

    // ==================== GETTERS ====================

    public bool HasActivePass() => hasActivePass;
    public float GetTimeRemaining() => Mathf.Max(0, passExpirationTime - Time.time);
    public int GetPremiumCurrency() => premiumCurrency;
}
