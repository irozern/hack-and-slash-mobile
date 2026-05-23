using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Manages loot generation, drops, and collection.
/// Handles fountain physics and loot distribution.
/// </summary>
public class LootManager : MonoBehaviour
{
    public static LootManager Instance { get; private set; }

    [SerializeField] private GameObject lootItemPrefab;
    [SerializeField] private Transform lootParent;

    private List<LootItem> activeLoot = new List<LootItem>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Update()
    {
        // Remove destroyed loot from list
        activeLoot.RemoveAll(l => l == null);

        // Check for pickups
        CheckPickups();
    }

    /// <summary>
    /// Generates loot drops when an enemy dies.
    /// </summary>
    public void GenerateLoot(Vector3 dropPosition, int enemyLevel = 1)
    {
        // Determine number of drops (1-3 items)
        int dropCount = Random.Range(1, 4);

        for (int i = 0; i < dropCount; i++)
        {
            ItemData itemData = ItemDatabase.Instance.GetRandomItem();
            if (itemData == null) continue;

            // Scale item stats by enemy level
            ItemData scaledItem = ScaleItemStats(itemData, enemyLevel);

            // Calculate drop position with fountain effect
            Vector3 dropPos = dropPosition + Random.insideUnitSphere * 0.5f;
            Vector3 dropForce = GetFountainForce();

            DropLoot(scaledItem, dropPos, dropForce);
        }
    }

    /// <summary>
    /// Drops a single loot item at the specified position.
    /// </summary>
    private void DropLoot(ItemData itemData, Vector3 dropPosition, Vector3 dropForce)
    {
        GameObject lootObj = Instantiate(lootItemPrefab, dropPosition, Quaternion.identity, lootParent);
        LootItem lootItem = lootObj.GetComponent<LootItem>();

        if (lootItem != null)
        {
            lootItem.Initialize(itemData, dropPosition, dropForce);
            activeLoot.Add(lootItem);
        }
    }

    /// <summary>
    /// Calculates fountain physics for loot drops.
    /// </summary>
    private Vector3 GetFountainForce()
    {
        float angle = Random.Range(0f, 360f) * Mathf.Deg2Rad;
        float height = Constants.Loot.LOOT_FOUNTAIN_ANGLE * Mathf.Deg2Rad;

        float x = Mathf.Cos(angle) * Mathf.Cos(height);
        float z = Mathf.Sin(angle) * Mathf.Cos(height);
        float y = Mathf.Sin(height);

        return new Vector3(x, y, z).normalized * Constants.Loot.LOOT_FOUNTAIN_FORCE;
    }

    /// <summary>
    /// Scales item stats based on enemy level.
    /// </summary>
    private ItemData ScaleItemStats(ItemData baseItem, int level)
    {
        ItemData scaledItem = Instantiate(baseItem);
        float levelMultiplier = 1f + (level - 1) * 0.1f;

        scaledItem.healthBonus *= levelMultiplier;
        scaledItem.damageBonus *= levelMultiplier;
        scaledItem.armorBonus *= levelMultiplier;

        return scaledItem;
    }

    /// <summary>
    /// Checks if player is in range of any loot items and picks them up.
    /// </summary>
    private void CheckPickups()
    {
        Transform playerTransform = GameManager.Instance.GetPlayer().transform;
        if (playerTransform == null) return;

        foreach (var lootItem in activeLoot)
        {
            if (lootItem == null) continue;

            float distance = Vector3.Distance(playerTransform.position, lootItem.transform.position);

            if (distance < Constants.Loot.LOOT_PICKUP_RANGE)
            {
                if (lootItem.TryPickup())
                {
                    // TODO: Add to inventory
                    // TODO: Play pickup sound
                    // TODO: Show pickup notification
                }
            }
        }
    }

    public List<LootItem> GetActiveLoot() => activeLoot;
}
