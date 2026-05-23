using UnityEngine;
using System;

/// <summary>
/// Manages player statistics and character progression.
/// Handles HP, Mana, Damage, Armor, and other character attributes.
/// </summary>
public class PlayerStats : MonoBehaviour
{
    [SerializeField] private float currentHealth;
    [SerializeField] private float currentMana;
    [SerializeField] private int level = 1;
    [SerializeField] private long experience = 0;
    [SerializeField] private long gold = 0;

    // Base stats
    private float baseMaxHealth;
    private float baseMaxMana;
    private float baseDamage;
    private float baseArmor;
    private float baseMoveSpeed;
    private float baseAttackSpeed;

    // Bonus stats from equipment
    private float bonusHealth = 0;
    private float bonusDamage = 0;
    private float bonusArmor = 0;
    private float bonusMoveSpeed = 0;
    private float bonusAttackSpeed = 0;

    // Events
    public event Action<float, float> OnHealthChanged;
    public event Action<float, float> OnManaChanged;
    public event Action<int> OnLevelUp;
    public event Action OnDeath;

    private void Awake()
    {
        InitializeStats();
    }

    private void InitializeStats()
    {
        baseMaxHealth = Constants.Player.BASE_HP;
        baseMaxMana = Constants.Player.BASE_MANA;
        baseDamage = Constants.Player.BASE_DAMAGE;
        baseArmor = 0;
        baseMoveSpeed = Constants.Player.MOVE_SPEED;
        baseAttackSpeed = 1f / Constants.Player.ATTACK_COOLDOWN;

        currentHealth = baseMaxHealth;
        currentMana = baseMaxMana;
    }

    // ==================== HEALTH ====================

    public void TakeDamage(float damage)
    {
        // Calculate actual damage with armor reduction
        float actualDamage = CalculateDamageReduction(damage);
        currentHealth -= actualDamage;

        OnHealthChanged?.Invoke(currentHealth, GetMaxHealth());

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            OnDeath?.Invoke();
        }
    }

    public void Heal(float amount)
    {
        float maxHealth = GetMaxHealth();
        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
        OnHealthChanged?.Invoke(currentHealth, maxHealth);
    }

    private float CalculateDamageReduction(float incomingDamage)
    {
        float totalArmor = baseArmor + bonusArmor;
        float damageReduction = totalArmor / (totalArmor + 100f); // Armor formula
        return incomingDamage * (1f - damageReduction);
    }

    // ==================== MANA ====================

    public void ConsumeMana(float amount)
    {
        currentMana = Mathf.Max(currentMana - amount, 0);
        OnManaChanged?.Invoke(currentMana, GetMaxMana());
    }

    public void RestoreMana(float amount)
    {
        float maxMana = GetMaxMana();
        currentMana = Mathf.Min(currentMana + amount, maxMana);
        OnManaChanged?.Invoke(currentMana, maxMana);
    }

    public bool HasEnoughMana(float requiredMana)
    {
        return currentMana >= requiredMana;
    }

    // ==================== EXPERIENCE & LEVELING ====================

    public void GainExperience(float amount)
    {
        experience += (long)amount;

        long expToLevel = GetExperienceToLevel();
        while (experience >= expToLevel && level < Constants.Experience.MAX_LEVEL)
        {
            experience -= expToLevel;
            LevelUp();
        }
    }

    private void LevelUp()
    {
        level++;
        
        // Increase base stats
        baseMaxHealth *= 1.1f;
        baseMaxMana *= 1.1f;
        baseDamage *= 1.05f;

        currentHealth = baseMaxHealth;
        currentMana = baseMaxMana;

        OnHealthChanged?.Invoke(currentHealth, baseMaxHealth);
        OnManaChanged?.Invoke(currentMana, baseMaxMana);
        OnLevelUp?.Invoke(level);
    }

    private long GetExperienceToLevel()
    {
        return (long)(Constants.Experience.BASE_EXP_TO_LEVEL * 
                      Mathf.Pow(Constants.Experience.EXP_MULTIPLIER_PER_LEVEL, level - 1));
    }

    public float GetExperienceProgress()
    {
        long expToLevel = GetExperienceToLevel();
        return Mathf.Clamp01((float)experience / expToLevel);
    }

    // ==================== GOLD ====================

    public void AddGold(long amount)
    {
        gold += amount;
    }

    public bool SpendGold(long amount)
    {
        if (gold >= amount)
        {
            gold -= amount;
            return true;
        }
        return false;
    }

    // ==================== DAMAGE CALCULATION ====================

    public float CalculateDamage()
    {
        float totalDamage = baseDamage + bonusDamage;
        
        // Apply crit chance
        if (Random.value < Constants.Player.CRIT_CHANCE)
        {
            totalDamage *= Constants.Player.CRIT_MULTIPLIER;
        }

        return totalDamage;
    }

    // ==================== STAT GETTERS ====================

    public float GetMaxHealth() => baseMaxHealth + bonusHealth;
    public float GetCurrentHealth() => currentHealth;
    public float GetHealthPercent() => currentHealth / GetMaxHealth();

    public float GetMaxMana() => baseMaxMana + bonusMana;
    public float GetCurrentMana() => currentMana;
    public float GetManaPercent() => currentMana / GetMaxMana();

    public float GetDamage() => baseDamage + bonusDamage;
    public float GetArmor() => baseArmor + bonusArmor;
    public float GetMoveSpeed() => baseMoveSpeed + bonusMoveSpeed;
    public float GetAttackSpeed() => baseAttackSpeed + bonusAttackSpeed;

    public int GetLevel() => level;
    public long GetExperience() => experience;
    public long GetGold() => gold;

    // ==================== STAT MODIFICATION ====================

    public void AddBonusHealth(float amount) => bonusHealth += amount;
    public void AddBonusDamage(float amount) => bonusDamage += amount;
    public void AddBonusArmor(float amount) => bonusArmor += amount;
    public void AddBonusMoveSpeed(float amount) => bonusMoveSpeed += amount;
    public void AddBonusAttackSpeed(float amount) => bonusAttackSpeed += amount;

    public void RemoveBonusHealth(float amount) => bonusHealth -= amount;
    public void RemoveBonusDamage(float amount) => bonusDamage -= amount;
    public void RemoveBonusArmor(float amount) => bonusArmor -= amount;
    public void RemoveBonusMoveSpeed(float amount) => bonusMoveSpeed -= amount;
    public void RemoveBonusAttackSpeed(float amount) => bonusAttackSpeed -= amount;

    private float bonusMana = 0;
}
