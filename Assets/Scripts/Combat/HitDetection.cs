using UnityEngine;
using System.Collections;

/// <summary>
/// Handles hit detection effects: hit flash, knockback, and damage numbers.
/// </summary>
public class HitDetection : MonoBehaviour
{
    [SerializeField] private Renderer[] renderers;
    [SerializeField] private CharacterController characterController;

    private Material[] originalMaterials;
    private Color originalColor;

    private void Start()
    {
        if (renderers.Length == 0)
            renderers = GetComponentsInChildren<Renderer>();

        if (characterController == null)
            characterController = GetComponent<CharacterController>();

        // Store original materials
        originalMaterials = new Material[renderers.Length];
        for (int i = 0; i < renderers.Length; i++)
        {
            originalMaterials[i] = renderers[i].material;
            originalColor = originalMaterials[i].color;
        }
    }

    /// <summary>
    /// Plays hit flash effect (white flash).
    /// </summary>
    public void PlayHitFlash()
    {
        StartCoroutine(HitFlashCoroutine());
    }

    private IEnumerator HitFlashCoroutine()
    {
        // Flash white
        foreach (var renderer in renderers)
        {
            renderer.material.color = Color.white;
        }

        yield return new WaitForSeconds(Constants.Combat.HIT_FLASH_DURATION);

        // Return to original color
        foreach (var renderer in renderers)
        {
            renderer.material.color = originalColor;
        }
    }

    /// <summary>
    /// Applies knockback force to the character.
    /// </summary>
    public void ApplyKnockback(Vector3 direction, float force = Constants.Combat.KNOCKBACK_FORCE)
    {
        if (characterController == null) return;

        Vector3 knockbackVelocity = direction.normalized * force;
        StartCoroutine(KnockbackCoroutine(knockbackVelocity));
    }

    private IEnumerator KnockbackCoroutine(Vector3 knockbackVelocity)
    {
        float elapsed = 0f;

        while (elapsed < Constants.Combat.KNOCKBACK_DURATION)
        {
            elapsed += Time.deltaTime;
            float progress = 1f - (elapsed / Constants.Combat.KNOCKBACK_DURATION);

            // Gradually reduce knockback
            Vector3 currentVelocity = knockbackVelocity * progress;
            currentVelocity.y -= 9.81f * Time.deltaTime;

            characterController.Move(currentVelocity * Time.deltaTime);

            yield return null;
        }
    }

    /// <summary>
    /// Shows a damage number at the hit location.
    /// </summary>
    public void ShowDamageNumber(float damage, bool isCritical = false, Vector3? customPosition = null)
    {
        Vector3 position = customPosition ?? transform.position + Vector3.up;

        // TODO: Instantiate damage number prefab
        // GameObject damageNumberObj = Instantiate(damageNumberPrefab, position, Quaternion.identity);
        // DamageNumber damageNumber = damageNumberObj.GetComponent<DamageNumber>();
        // damageNumber.Initialize(position, damage, isCritical);
    }
}
