using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Enemy AI system using Finite State Machine (FSM).
/// States: Idle, Patrol, Chase, Attack, Death
/// </summary>
public class EnemyAI : MonoBehaviour
{
    [SerializeField] private NavMeshAgent navMeshAgent;
    [SerializeField] private EnemyStats enemyStats;
    [SerializeField] private Animator animator;

    [Header("AI Settings")]
    [SerializeField] private float aggroRange = Constants.Enemy.AGGRO_RANGE;
    [SerializeField] private float attackRange = Constants.Enemy.ATTACK_RANGE;
    [SerializeField] private float patrolRange = Constants.Enemy.PATROL_RANGE;
    [SerializeField] private float attackCooldown = Constants.Enemy.ATTACK_COOLDOWN;

    private Transform playerTransform;
    private EnemyState currentState = EnemyState.Idle;
    private Vector3 patrolCenter;
    private float lastAttackTime = 0f;
    private bool isDead = false;

    private enum EnemyState
    {
        Idle,
        Patrol,
        Chase,
        Attack,
        Death
    }

    private void Start()
    {
        if (navMeshAgent == null)
            navMeshAgent = GetComponent<NavMeshAgent>();

        if (enemyStats == null)
            enemyStats = GetComponent<EnemyStats>();

        if (animator == null)
            animator = GetComponent<Animator>();

        patrolCenter = transform.position;
        navMeshAgent.speed = enemyStats.GetMoveSpeed();

        // Find player
        GameObject playerObj = GameObject.FindGameObjectWithTag(Constants.Tags.PLAYER);
        if (playerObj != null)
            playerTransform = playerObj.transform;

        // Subscribe to death event
        enemyStats.OnDeath += HandleDeath;
    }

    private void Update()
    {
        if (isDead) return;

        UpdateAIState();
        ExecuteState();
    }

    /// <summary>
    /// Updates the current AI state based on conditions.
    /// </summary>
    private void UpdateAIState()
    {
        if (playerTransform == null) return;

        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

        switch (currentState)
        {
            case EnemyState.Idle:
                if (distanceToPlayer < aggroRange)
                    currentState = EnemyState.Chase;
                else if (Random.value > 0.95f)
                    currentState = EnemyState.Patrol;
                break;

            case EnemyState.Patrol:
                if (distanceToPlayer < aggroRange)
                    currentState = EnemyState.Chase;
                else if (!navMeshAgent.hasPath || navMeshAgent.remainingDistance < 0.5f)
                    currentState = EnemyState.Idle;
                break;

            case EnemyState.Chase:
                if (distanceToPlayer > aggroRange * 1.5f)
                    currentState = EnemyState.Patrol;
                else if (distanceToPlayer < attackRange)
                    currentState = EnemyState.Attack;
                break;

            case EnemyState.Attack:
                if (distanceToPlayer > attackRange * 1.5f)
                    currentState = EnemyState.Chase;
                break;
        }
    }

    /// <summary>
    /// Executes behavior for the current state.
    /// </summary>
    private void ExecuteState()
    {
        switch (currentState)
        {
            case EnemyState.Idle:
                ExecuteIdle();
                break;

            case EnemyState.Patrol:
                ExecutePatrol();
                break;

            case EnemyState.Chase:
                ExecuteChase();
                break;

            case EnemyState.Attack:
                ExecuteAttack();
                break;

            case EnemyState.Death:
                ExecuteDeath();
                break;
        }
    }

    private void ExecuteIdle()
    {
        navMeshAgent.SetDestination(transform.position);
        animator.SetFloat(Constants.Animation.PARAM_MOVE_SPEED, 0);
    }

    private void ExecutePatrol()
    {
        if (!navMeshAgent.hasPath || navMeshAgent.remainingDistance < 0.5f)
        {
            Vector3 randomDirection = Random.insideUnitSphere * patrolRange;
            randomDirection += patrolCenter;

            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomDirection, out hit, patrolRange, NavMesh.AllAreas))
            {
                navMeshAgent.SetDestination(hit.position);
            }
        }

        animator.SetFloat(Constants.Animation.PARAM_MOVE_SPEED, navMeshAgent.velocity.magnitude);
    }

    private void ExecuteChase()
    {
        if (playerTransform != null)
        {
            navMeshAgent.SetDestination(playerTransform.position);
            animator.SetFloat(Constants.Animation.PARAM_MOVE_SPEED, navMeshAgent.velocity.magnitude);
        }
    }

    private void ExecuteAttack()
    {
        navMeshAgent.SetDestination(transform.position);
        animator.SetFloat(Constants.Animation.PARAM_MOVE_SPEED, 0);

        // Attack cooldown
        if (Time.time - lastAttackTime >= attackCooldown)
        {
            PerformAttack();
            lastAttackTime = Time.time;
        }
    }

    private void ExecuteDeath()
    {
        navMeshAgent.enabled = false;
        animator.SetBool(Constants.Animation.PARAM_IS_DEAD, true);
    }

    /// <summary>
    /// Performs an attack on the player.
    /// </summary>
    private void PerformAttack()
    {
        if (playerTransform == null) return;

        // TODO: Deal damage to player
        // TODO: Play attack animation
        // TODO: Add attack VFX
    }

    private void HandleDeath()
    {
        isDead = true;
        currentState = EnemyState.Death;

        // TODO: Generate loot
        // TODO: Add death VFX
        // TODO: Destroy or pool this enemy
    }

    public float GetHealthPercent() => enemyStats.GetHealthPercent();
    public bool IsAlive() => !isDead;
}
