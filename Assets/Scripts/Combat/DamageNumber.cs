using UnityEngine;
using TMPro;
using System.Collections;

/// <summary>
/// Floating damage number that appears above enemies when hit.
/// Shows normal damage in white and critical damage in yellow.
/// </summary>
public class DamageNumber : MonoBehaviour
{
    private TextMeshProUGUI damageText;
    private CanvasGroup canvasGroup;
    private Vector3 startPosition;
    private float lifetime;
    private bool isCritical;

    public void Initialize(Vector3 worldPosition, float damage, bool isCrit = false)
    {
        damageText = GetComponent<TextMeshProUGUI>();
        canvasGroup = GetComponent<CanvasGroup>();

        isCritical = isCrit;
        lifetime = Constants.Combat.DAMAGE_NUMBER_LIFETIME;

        // Set damage text
        damageText.text = Mathf.RoundToInt(damage).ToString();
        damageText.color = isCrit ? Color.yellow : Color.white;

        // Convert world position to screen position
        Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPosition);
        GetComponent<RectTransform>().position = screenPos;
        startPosition = screenPos;

        // Start animation
        StartCoroutine(AnimateDamage());
    }

    private IEnumerator AnimateDamage()
    {
        float elapsed = 0f;

        while (elapsed < lifetime)
        {
            elapsed += Time.deltaTime;
            float progress = elapsed / lifetime;

            // Rise up
            Vector3 newPos = startPosition + Vector3.up * Constants.Combat.DAMAGE_NUMBER_RISE_SPEED * elapsed;
            GetComponent<RectTransform>().position = newPos;

            // Fade out
            canvasGroup.alpha = Mathf.Lerp(1f, 0f, progress);

            // Scale down slightly
            float scale = Mathf.Lerp(1.2f, 0.8f, progress);
            transform.localScale = Vector3.one * scale;

            yield return null;
        }

        Destroy(gameObject);
    }
}
