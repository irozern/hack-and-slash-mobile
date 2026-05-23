using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Factory for creating game prefabs at runtime.
/// Used for creating Player, Enemy, Loot, and UI prefabs.
/// </summary>
public class PrefabFactory : MonoBehaviour
{
    /// <summary>
    /// Creates a Player GameObject with all required components.
    /// </summary>
    public static GameObject CreatePlayer(Vector3 position = default)
    {
        GameObject player = new GameObject("Player");
        player.tag = Constants.Tags.PLAYER;
        player.transform.position = position;

        // Add Capsule renderer
        GameObject capsule = GameObject.CreatePrimitive(PrimitiveType.Capsule);
        capsule.transform.SetParent(player.transform);
        capsule.transform.localPosition = Vector3.zero;
        capsule.name = "Model";
        Destroy(capsule.GetComponent<Collider>());

        // Add CharacterController
        CharacterController controller = player.AddComponent<CharacterController>();
        controller.height = 2f;
        controller.radius = 0.5f;
        controller.center = new Vector3(0, 1, 0);

        // Add scripts
        player.AddComponent<PlayerStats>();
        player.AddComponent<PlayerController>();
        player.AddComponent<PlayerCombat>();
        player.AddComponent<HitDetection>();
        player.AddComponent<Animator>();

        Debug.Log("Player prefab created");
        return player;
    }

    /// <summary>
    /// Creates an Enemy GameObject with all required components.
    /// </summary>
    public static GameObject CreateEnemy(Vector3 position = default, EnemyDatabaseLoader.EnemyData enemyData = null)
    {
        if (enemyData == null)
            enemyData = EnemyDatabaseLoader.GetRandomEnemy();

        if (enemyData == null)
        {
            Debug.LogError("No enemy data available!");
            return null;
        }

        GameObject enemy = new GameObject($"Enemy_{enemyData.name}");
        enemy.tag = Constants.Tags.ENEMY;
        enemy.transform.position = position;

        // Add Capsule renderer
        GameObject capsule = GameObject.CreatePrimitive(PrimitiveType.Capsule);
        capsule.transform.SetParent(enemy.transform);
        capsule.transform.localPosition = Vector3.zero;
        capsule.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
        capsule.name = "Model";
        Destroy(capsule.GetComponent<Collider>());

        // Add NavMeshAgent
        NavMeshAgent agent = enemy.AddComponent<NavMeshAgent>();
        agent.speed = enemyData.moveSpeed;
        agent.stoppingDistance = 0.5f;
        agent.autoBraking = true;

        // Add scripts
        EnemyStats stats = enemy.AddComponent<EnemyStats>();
        stats.GetType().GetField("maxHealth", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).SetValue(stats, enemyData.maxHealth);
        stats.GetType().GetField("damage", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).SetValue(stats, enemyData.damage);

        enemy.AddComponent<EnemyAI>();
        enemy.AddComponent<EnemyController>();
        enemy.AddComponent<HitDetection>();
        enemy.AddComponent<Animator>();

        Debug.Log($"Enemy prefab created: {enemyData.name}");
        return enemy;
    }

    /// <summary>
    /// Creates a Loot item GameObject.
    /// </summary>
    public static GameObject CreateLootItem(Vector3 position = default, ItemData itemData = null)
    {
        if (itemData == null)
            itemData = ItemDatabase.Instance.GetRandomItem();

        if (itemData == null)
        {
            Debug.LogError("No item data available!");
            return null;
        }

        GameObject loot = new GameObject($"Loot_{itemData.itemName}");
        loot.tag = Constants.Tags.LOOT;
        loot.transform.position = position;

        // Add Cube renderer
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.SetParent(loot.transform);
        cube.transform.localPosition = Vector3.zero;
        cube.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
        cube.name = "Model";

        // Color by rarity
        Renderer renderer = cube.GetComponent<Renderer>();
        renderer.material.color = itemData.rarityColor;

        // Add Rigidbody
        Rigidbody rb = loot.AddComponent<Rigidbody>();
        rb.mass = 0.5f;
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;

        // Add LootItem script
        LootItem lootItem = loot.AddComponent<LootItem>();

        Debug.Log($"Loot item created: {itemData.itemName}");
        return loot;
    }

    /// <summary>
    /// Creates a UI Canvas with all HUD elements.
    /// </summary>
    public static GameObject CreateHUDCanvas()
    {
        GameObject canvasObj = new GameObject("HUDCanvas");
        Canvas canvas = canvasObj.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;

        CanvasScaler scaler = canvasObj.AddComponent<CanvasScaler>();
        scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;

        // Add HUDManager
        HUDManager hudManager = canvasObj.AddComponent<HUDManager>();

        Debug.Log("HUD Canvas created");
        return canvasObj;
    }

    /// <summary>
    /// Creates a virtual joystick UI.
    /// </summary>
    public static GameObject CreateJoystick(Transform parent = null)
    {
        GameObject joystickObj = new GameObject("Joystick");
        if (parent != null)
            joystickObj.transform.SetParent(parent);

        RectTransform rectTransform = joystickObj.AddComponent<RectTransform>();
        rectTransform.anchorMin = Vector2.zero;
        rectTransform.anchorMax = Vector2.zero;
        rectTransform.offsetMin = new Vector2(20, 20);
        rectTransform.offsetMax = new Vector2(220, 220);

        Image background = joystickObj.AddComponent<Image>();
        background.color = new Color(0.5f, 0.5f, 0.5f, 0.5f);

        // Create handle
        GameObject handleObj = new GameObject("Handle");
        handleObj.transform.SetParent(joystickObj.transform);

        RectTransform handleRect = handleObj.AddComponent<RectTransform>();
        handleRect.anchoredPosition = Vector2.zero;
        handleRect.sizeDelta = new Vector2(80, 80);

        Image handleImage = handleObj.AddComponent<Image>();
        handleImage.color = Color.white;

        Debug.Log("Joystick created");
        return joystickObj;
    }

    /// <summary>
    /// Creates an attack button UI.
    /// </summary>
    public static GameObject CreateAttackButton(Transform parent = null)
    {
        GameObject buttonObj = new GameObject("AttackButton");
        if (parent != null)
            buttonObj.transform.SetParent(parent);

        RectTransform rectTransform = buttonObj.AddComponent<RectTransform>();
        rectTransform.anchorMin = Vector2.one;
        rectTransform.anchorMax = Vector2.one;
        rectTransform.offsetMin = new Vector2(-120, 20);
        rectTransform.offsetMax = new Vector2(-20, 120);

        Image image = buttonObj.AddComponent<Image>();
        image.color = Color.red;

        Button button = buttonObj.AddComponent<Button>();

        // Add text
        GameObject textObj = new GameObject("Text");
        textObj.transform.SetParent(buttonObj.transform);

        RectTransform textRect = textObj.AddComponent<RectTransform>();
        textRect.anchoredPosition = Vector2.zero;
        textRect.sizeDelta = Vector2.zero;

        TextMeshProUGUI text = textObj.AddComponent<TextMeshProUGUI>();
        text.text = "ATTACK";
        text.alignment = TextAlignmentOptions.Center;
        text.fontSize = 36;

        Debug.Log("Attack button created");
        return buttonObj;
    }

    /// <summary>
    /// Creates a dash button UI.
    /// </summary>
    public static GameObject CreateDashButton(Transform parent = null)
    {
        GameObject buttonObj = new GameObject("DashButton");
        if (parent != null)
            buttonObj.transform.SetParent(parent);

        RectTransform rectTransform = buttonObj.AddComponent<RectTransform>();
        rectTransform.anchorMin = Vector2.one;
        rectTransform.anchorMax = Vector2.one;
        rectTransform.offsetMin = new Vector2(-120, 130);
        rectTransform.offsetMax = new Vector2(-20, 230);

        Image image = buttonObj.AddComponent<Image>();
        image.color = Color.blue;

        Button button = buttonObj.AddComponent<Button>();

        // Add text
        GameObject textObj = new GameObject("Text");
        textObj.transform.SetParent(buttonObj.transform);

        RectTransform textRect = textObj.AddComponent<RectTransform>();
        textRect.anchoredPosition = Vector2.zero;
        textRect.sizeDelta = Vector2.zero;

        TextMeshProUGUI text = textObj.AddComponent<TextMeshProUGUI>();
        text.text = "DASH";
        text.alignment = TextAlignmentOptions.Center;
        text.fontSize = 36;

        Debug.Log("Dash button created");
        return buttonObj;
    }
}
