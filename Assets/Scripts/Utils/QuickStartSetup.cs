using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Quick setup utility for creating game scene automatically.
/// Use this to quickly setup a test scene in the editor.
/// </summary>
public class QuickStartSetup : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private int initialEnemyCount = 5;

    /// <summary>
    /// Creates a complete game scene with all necessary components.
    /// Call this from the editor menu or a setup button.
    /// </summary>
    public void SetupGameScene()
    {
        Debug.Log("Setting up game scene...");

        // Create ground
        CreateGround();

        // Create managers
        CreateManagers();

        // Create player
        CreatePlayer();

        // Create camera
        CreateCamera();

        // Create UI
        CreateUI();

        Debug.Log("Game scene setup complete!");
    }

    private void CreateGround()
    {
        GameObject ground = GameObject.CreatePrimitive(PrimitiveType.Plane);
        ground.name = "Ground";
        ground.transform.localScale = new Vector3(50, 1, 50);
        ground.transform.position = Vector3.zero;

        // Remove collider (we'll use NavMesh)
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
        GameManager gameManager = gameManagerObj.AddComponent<GameManager>();
        gameManager.GetType().GetField("playerPrefab", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).SetValue(gameManager, playerPrefab);
        gameManager.GetType().GetField("enemyPrefab", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).SetValue(gameManager, enemyPrefab);

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

    private void CreatePlayer()
    {
        if (playerPrefab == null)
        {
            Debug.LogError("Player prefab not assigned!");
            return;
        }

        GameObject player = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
        player.name = "Player";
        player.tag = Constants.Tags.PLAYER;

        Debug.Log("Player created");
    }

    private void CreateCamera()
    {
        GameObject cameraObj = new GameObject("CameraController");
        CameraController cameraController = cameraObj.AddComponent<CameraController>();

        // Find main camera
        Camera mainCamera = Camera.main;
        if (mainCamera != null)
        {
            cameraController.GetType().GetField("mainCamera", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).SetValue(cameraController, mainCamera);
        }

        Debug.Log("Camera controller created");
    }

    private void CreateUI()
    {
        // Create Canvas
        GameObject canvasObj = new GameObject("Canvas");
        Canvas canvas = canvasObj.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;

        CanvasScaler scaler = canvasObj.AddComponent<CanvasScaler>();
        scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;

        // Create HUD Manager
        GameObject hudObj = new GameObject("HUDManager");
        hudObj.transform.SetParent(canvasObj.transform);
        hudObj.AddComponent<HUDManager>();

        Debug.Log("UI created");
    }
}
