using UnityEngine;
using System;

/// <summary>
/// Manages enemy statistics and health.
/// Handles damage, death, and loot generation.
/// </summary>
public class EnemyStats : MonoBehaviour
{
    [SerializeField] private float currentHealth;
    [SerializeField] private bool isElite = false;

    private float baseMaxHealth;
    private float baseDamage;
    private float baseArmor;
    private float baseMoveSpeed;

    // Events
    public event Action<float, float> OnHealthChanged;
    public event Action OnDeath;

    private void Awake()
    {
        InitializeStats();
    }

    private void InitializeStats()
    {
        baseMaxHealth = Constants.Enemy.BASE_HP;
        baseDamage = Constants.Enemy.BASE_DAMAGE;
        baseArmor = 0;
        baseMoveSpeed = Constants.Enemy.BASE_MOVE_SPEED;

        // Apply elite modifiers
        if (isElite)
        {
            baseMaxHealth *= Constants.Enemy.ELITE_HP_MULTIPLIER;
            baseDamage *= Constants.Enemy.ELITE_DAMAGE_MULTIPLIER;
        }

        currentHealth = baseMaxHealth;
    }

    public void TakeDamage(float damage)
    {
        // Calculate actual damage with armor reduction
        float actualDamage = CalculateDamageReduction(damage);
        currentHealth -= actualDamage;

        OnHealthChanged?.Invoke(currentHealth, baseMaxHealth);

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            OnDeath?.Invoke();
        }
    }

    private float CalculateDamageReduction(float incomingDamage)
    {
        float damageReduction = baseArmor / (baseArmor + 100f);
        return incomingDamage * (1f - damageReduction);
    }

    public void Heal(float amount)
    {
        currentHealth = Mathf.Min(currentHealth + amount, baseMaxHealth);
        OnHealthChanged?.Invoke(currentHealth, baseMaxHealth);
    }

    // ==================== STAT GETTERS ====================

    public float GetMaxHealth() => baseMaxHealth;
    public float GetCurrentHealth() => currentHealth;
    public float GetHealthPercent() => currentHealth / baseMaxHealth;

    public float GetDamage() => baseDamage;
    public float GetArmor() => baseArmor;
    public float GetMoveSpeed() => baseMoveSpeed;

    public bool IsElite() => isElite;
    public bool IsAlive() => currentHealth > 0;

    // ==================== STAT SETTERS ====================

    public void SetElite(bool elite)
    {
        isElite = elite;
        if (isElite)
        {
            baseMaxHealth *= Constants.Enemy.ELITE_HP_MULTIPLIER;
            baseDamage *= Constants.Enemy.ELITE_DAMAGE_MULTIPLIER;
            currentHealth = baseMaxHealth;
        }
    }

    public void SetStats(float maxHealth, float damage, float moveSpeed)
    {
        baseMaxHealth = maxHealth;
        baseDamage = damage;
        baseMoveSpeed = moveSpeed;
        currentHealth = baseMaxHealth;
    }
}
