using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Generates a complete game scene with all necessary components.
/// Use this to quickly create a playable scene in the editor.
/// </summary>
public class SceneGenerator : MonoBehaviour
{
    [SerializeField] private int initialEnemyCount = 5;
    [SerializeField] private float spawnRadius = 20f;

    /// <summary>
    /// Generates a complete game scene.
    /// Call this from the editor or a setup button.
    /// </summary>
    public void GenerateScene()
    {
        Debug.Log("Generating game scene...");

        // Create ground with NavMesh
        CreateGround();

        // Create managers
        CreateManagers();

        // Create player
        GameObject player = PrefabFactory.CreatePlayer(Vector3.zero);

        // Create camera
        CreateCamera(player.transform);

        // Create UI
        GameObject canvas = PrefabFactory.CreateHUDCanvas();

        // Create joystick
        GameObject joystick = PrefabFactory.CreateJoystick(canvas.transform);

        // Create buttons
        GameObject attackButton = PrefabFactory.CreateAttackButton(canvas.transform);
        GameObject dashButton = PrefabFactory.CreateDashButton(canvas.transform);

        // Spawn initial enemies
        SpawnEnemies(initialEnemyCount, spawnRadius);

        Debug.Log("Scene generation complete!");
    }

    private void CreateGround()
    {
        GameObject ground = GameObject.CreatePrimitive(PrimitiveType.Plane);
        ground.name = "Ground";
        ground.transform.localScale = new Vector3(50, 1, 50);
        ground.transform.position = Vector3.zero;

        // Remove collider
        Collider collider = ground.GetComponent<Collider>();
        if (collider != null)
            DestroyImmediate(collider);

        // Add NavMesh surface
        NavMeshSurface navMeshSurface = ground.AddComponent<NavMeshSurface>();
        navMeshSurface.Bake();

        Debug.Log("Ground created with NavMesh");
    }

    private void CreateManagers()
    {
        // GameManager
        GameObject gameManagerObj = new GameObject("GameManager");
        gameManagerObj.AddComponent<GameManager>();

        // InputManager
        GameObject inputManagerObj = new GameObject("InputManager");
        inputManagerObj.AddComponent<InputManager>();

        // ItemDatabase
        GameObject itemDatabaseObj = new GameObject("ItemDatabase");
        itemDatabaseObj.AddComponent<ItemDatabase>();

        // LootManager
        GameObject lootManagerObj = new GameObject("LootManager");
        lootManagerObj.AddComponent<LootManager>();

        // InventoryManager
        GameObject inventoryManagerObj = new GameObject("InventoryManager");
        inventoryManagerObj.AddComponent<InventoryManager>();

        // GamePassManager
        GameObject gamePassManagerObj = new GameObject("GamePassManager");
        gamePassManagerObj.AddComponent<GamePassManager>();

        // PremiumChestManager
        GameObject premiumChestManagerObj = new GameObject("PremiumChestManager");
        premiumChestManagerObj.AddComponent<PremiumChestManager>();

        // QuestManager
        GameObject questManagerObj = new GameObject("QuestManager");
        questManagerObj.AddComponent<QuestManager>();

        Debug.Log("Managers created");
    }

    private void CreateCamera(Transform playerTransform)
    {
        GameObject cameraObj = new GameObject("CameraController");
        CameraController cameraController = cameraObj.AddComponent<CameraController>();

        // Position camera
        Camera mainCamera = Camera.main;
        if (mainCamera != null)
        {
            mainCamera.transform.position = new Vector3(0, 8, -10);
            mainCamera.transform.LookAt(playerTransform);
        }

        Debug.Log("Camera created");
    }

    private void SpawnEnemies(int count, float radius)
    {
        GameObject enemiesParent = new GameObject("Enemies");

        for (int i = 0; i < count; i++)
        {
            Vector3 spawnPos = Random.insideUnitCircle * radius;
            spawnPos.y = 0;

            GameObject enemy = PrefabFactory.CreateEnemy(spawnPos);
            if (enemy != null)
                enemy.transform.SetParent(enemiesParent.transform);
        }

        Debug.Log($"Spawned {count} enemies");
    }
}
