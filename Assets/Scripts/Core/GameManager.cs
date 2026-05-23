using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Main game manager handling game state, spawning, and overall control.
/// </summary>
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private Transform playerSpawnPoint;
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject enemyPrefab;

    [Header("Spawning")]
    [SerializeField] private int initialEnemyCount = 5;
    [SerializeField] private float spawnRadius = 20f;

    private GameObject playerInstance;
    private List<EnemyController> activeEnemies = new List<EnemyController>();
    private bool isGameRunning = true;

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
        SpawnPlayer();
        SpawnInitialEnemies();
    }

    private void Update()
    {
        if (!isGameRunning) return;

        // Remove dead enemies from list
        activeEnemies.RemoveAll(e => e == null || !e.IsAlive());

        // Spawn new enemies if needed
        if (activeEnemies.Count < initialEnemyCount)
        {
            SpawnEnemy();
        }
    }

    /// <summary>
    /// Spawns the player at the designated spawn point.
    /// </summary>
    private void SpawnPlayer()
    {
        Vector3 spawnPos = playerSpawnPoint != null ? playerSpawnPoint.position : Vector3.zero;
        playerInstance = Instantiate(playerPrefab, spawnPos, Quaternion.identity);
        playerInstance.name = "Player";
    }

    /// <summary>
    /// Spawns initial wave of enemies.
    /// </summary>
    private void SpawnInitialEnemies()
    {
        for (int i = 0; i < initialEnemyCount; i++)
        {
            SpawnEnemy();
        }
    }

    /// <summary>
    /// Spawns a single enemy at a random location around the player.
    /// </summary>
    private void SpawnEnemy()
    {
        if (playerInstance == null) return;

        Vector3 playerPos = playerInstance.transform.position;
        Vector3 randomDirection = Random.insideUnitSphere * spawnRadius;
        randomDirection.y = 0; // Keep on ground
        Vector3 spawnPos = playerPos + randomDirection;

        // Ensure spawn position is on NavMesh
        NavMeshHit hit;
        if (!NavMesh.SamplePosition(spawnPos, out hit, spawnRadius, NavMesh.AllAreas))
        {
            return;
        }

        GameObject enemyInstance = Instantiate(enemyPrefab, hit.position, Quaternion.identity);
        EnemyController enemyController = enemyInstance.GetComponent<EnemyController>();

        if (enemyController != null)
        {
            activeEnemies.Add(enemyController);

            // Randomly make some enemies elite
            if (Random.value < 0.1f) // 10% chance
            {
                enemyController.GetStats().SetElite(true);
            }
        }
    }

    /// <summary>
    /// Pauses or resumes the game.
    /// </summary>
    public void SetGamePaused(bool paused)
    {
        isGameRunning = !paused;
        Time.timeScale = paused ? 0f : 1f;
    }

    public bool IsGameRunning() => isGameRunning;
    public GameObject GetPlayer() => playerInstance;
    public List<EnemyController> GetActiveEnemies() => activeEnemies;
}
