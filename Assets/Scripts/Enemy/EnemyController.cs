using UnityEngine;

/// <summary>
/// Main controller for enemy entities.
/// Manages interaction between AI, stats, and combat.
/// </summary>
public class EnemyController : MonoBehaviour
{
    [SerializeField] private EnemyStats enemyStats;
    [SerializeField] private EnemyAI enemyAI;
    [SerializeField] private Canvas healthBarCanvas;
    [SerializeField] private Image healthBarFill;

    private bool isDead = false;

    private void Start()
    {
        if (enemyStats == null)
            enemyStats = GetComponent<EnemyStats>();

        if (enemyAI == null)
            enemyAI = GetComponent<EnemyAI>();

        // Subscribe to stat events
        enemyStats.OnHealthChanged += UpdateHealthBar;
        enemyStats.OnDeath += HandleDeath;

        // Setup health bar
        if (healthBarCanvas != null)
            healthBarCanvas.worldCamera = Camera.main;
    }

    private void OnDestroy()
    {
        if (enemyStats != null)
        {
            enemyStats.OnHealthChanged -= UpdateHealthBar;
            enemyStats.OnDeath -= HandleDeath;
        }
    }

    /// <summary>
    /// Applies damage to the enemy.
    /// </summary>
    public void TakeDamage(float damage)
    {
        if (isDead) return;
        enemyStats.TakeDamage(damage);
    }

    /// <summary>
    /// Updates the health bar UI.
    /// </summary>
    private void UpdateHealthBar(float currentHealth, float maxHealth)
    {
        if (healthBarFill != null)
        {
            healthBarFill.fillAmount = currentHealth / maxHealth;
        }
    }

    /// <summary>
    /// Handles enemy death.
    /// </summary>
    private void HandleDeath()
    {
        isDead = true;

        // TODO: Generate loot
        // TODO: Add death animation
        // TODO: Disable colliders
        // TODO: Pool or destroy

        // Destroy after delay
        Destroy(gameObject, 2f);
    }

    public bool IsAlive() => !isDead;
    public EnemyStats GetStats() => enemyStats;
}
