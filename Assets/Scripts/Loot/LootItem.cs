using UnityEngine;
using TMPro;

/// <summary>
/// Represents a loot item dropped in the world.
/// Handles pickup, visual representation, and despawn.
/// </summary>
public class LootItem : MonoBehaviour
{
    [SerializeField] private InventoryItem itemInstance;
    [SerializeField] private TextMeshPro rarityText;
    [SerializeField] private Renderer itemRenderer;
    [SerializeField] private Rigidbody rigidbody;

    private float despawnTimer;
    private bool isPickedUp = false;

    public void Initialize(ItemData itemData, Vector3 dropPosition, Vector3 dropForce)
    {
        itemInstance = new InventoryItem(itemData);
        transform.position = dropPosition;

        // Set visual
        if (rarityText != null)
        {
            rarityText.text = itemData.itemName;
            rarityText.color = itemData.rarityColor;
        }

        // Apply physics
        if (rigidbody != null)
        {
            rigidbody.velocity = dropForce;
            rigidbody.angularVelocity = Random.insideUnitSphere * 5f;
        }

        despawnTimer = Constants.Loot.LOOT_DESPAWN_TIME;
    }

    private void Update()
    {
        if (isPickedUp) return;

        // Despawn timer
        despawnTimer -= Time.deltaTime;
        if (despawnTimer <= 0)
        {
            Despawn();
        }

        // Fade out near end
        if (despawnTimer < 5f)
        {
            float alpha = despawnTimer / 5f;
            SetAlpha(alpha);
        }
    }

    private void SetAlpha(float alpha)
    {
        if (itemRenderer != null)
        {
            Color color = itemRenderer.material.color;
            color.a = alpha;
            itemRenderer.material.color = color;
        }
    }

    /// <summary>
    /// Picks up the loot item.
    /// </summary>
    public bool TryPickup()
    {
        if (isPickedUp) return false;

        isPickedUp = true;

        // TODO: Add pickup animation
        // TODO: Add pickup sound

        return true;
    }

    private void Despawn()
    {
        // TODO: Add despawn effect
        Destroy(gameObject);
    }

    public ItemData GetItemData() => itemInstance.itemData;
    public InventoryItem GetInventoryItem() => itemInstance;
}
