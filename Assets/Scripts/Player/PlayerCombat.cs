using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Manages player combat, attacks, and hit detection.
/// Handles melee attacks with cone-based AOE detection.
/// </summary>
public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private Animator animator;
    
    [Header("Attack Settings")]
    [SerializeField] private Transform attackOrigin;
    [SerializeField] private float attackCooldown = Constants.Player.ATTACK_COOLDOWN;

    private float lastAttackTime = 0f;
    private List<EnemyController> hitEnemies = new List<EnemyController>();

    private void Start()
    {
        if (playerController == null)
            playerController = GetComponent<PlayerController>();

        if (playerStats == null)
            playerStats = GetComponent<PlayerStats>();

        if (animator == null)
            animator = GetComponent<Animator>();

        if (attackOrigin == null)
            attackOrigin = transform;
    }

    private void Update()
    {
        HandleAttackInput();
    }

    /// <summary>
    /// Handles attack input from the player.
    /// </summary>
    private void HandleAttackInput()
    {
        if (InputManager.Instance.IsAttackPressed())
        {
            if (Time.time - lastAttackTime >= attackCooldown)
            {
                PerformAttack();
                lastAttackTime = Time.time;
            }
        }
    }

    /// <summary>
    /// Performs a melee attack with cone-based AOE detection.
    /// </summary>
    private void PerformAttack()
    {
        // Play attack animation
        animator.SetTrigger(Constants.Animation.PARAM_IS_ATTACKING);

        // Clear previous hit list
        hitEnemies.Clear();

        // Get all enemies in range
        Collider[] hitColliders = Physics.OverlapSphere(
            attackOrigin.position,
            Constants.Player.ATTACK_RANGE,
            LayerMask.GetMask(Constants.Layers.ENEMY)
        );

        Vector3 attackDirection = playerController.GetFacingDirection();
        float attackAngle = Constants.Player.ATTACK_ANGLE;

        foreach (Collider collider in hitColliders)
        {
            EnemyController enemy = collider.GetComponent<EnemyController>();
            if (enemy == null) continue;

            // Check if enemy is within attack cone
            Vector3 toEnemy = (enemy.transform.position - attackOrigin.position).normalized;
            float angleToEnemy = Vector3.Angle(attackDirection, toEnemy);

            if (angleToEnemy <= attackAngle / 2f)
            {
                DealDamageToEnemy(enemy);
                hitEnemies.Add(enemy);
            }
        }

        // TODO: Add attack VFX (slash effect)
        // TODO: Add attack sound
    }

    /// <summary>
    /// Deals damage to an enemy and applies effects.
    /// </summary>
    private void DealDamageToEnemy(EnemyController enemy)
    {
        float damage = playerStats.CalculateDamage();
        enemy.TakeDamage(damage);

        // TODO: Apply knockback
        // TODO: Show damage number
        // TODO: Add hit flash effect
    }

    /// <summary>
    /// Gets the cooldown progress (0-1) for the attack.
    /// </summary>
    public float GetAttackCooldownProgress()
    {
        float timeSinceLastAttack = Time.time - lastAttackTime;
        return Mathf.Clamp01(timeSinceLastAttack / attackCooldown);
    }

    public bool IsAttackReady() => Time.time - lastAttackTime >= attackCooldown;
}
