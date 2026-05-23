using UnityEngine;

/// <summary>
/// Manages the isometric camera system.
/// Follows the player with smooth interpolation and supports zoom.
/// </summary>
public class CameraController : MonoBehaviour
{
    public static CameraController Instance { get; private set; }

    [SerializeField] private Transform playerTarget;
    [SerializeField] private Camera mainCamera;
    
    private Vector3 offset;
    private float currentZoom;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        if (mainCamera == null)
            mainCamera = Camera.main;

        currentZoom = Constants.Camera.CAMERA_DISTANCE;
        CalculateIsometricOffset();
    }

    private void LateUpdate()
    {
        if (playerTarget == null) return;

        FollowPlayer();
        HandleZoom();
    }

    /// <summary>
    /// Calculates the isometric camera offset based on the configured angle.
    /// </summary>
    private void CalculateIsometricOffset()
    {
        float angleRad = Constants.Camera.ISOMETRIC_ANGLE * Mathf.Deg2Rad;
        float horizontalDist = currentZoom * Mathf.Cos(angleRad);
        float verticalDist = Constants.Camera.CAMERA_HEIGHT;

        offset = new Vector3(horizontalDist, verticalDist, horizontalDist);
    }

    /// <summary>
    /// Smoothly follows the player target.
    /// </summary>
    private void FollowPlayer()
    {
        Vector3 targetPosition = playerTarget.position + offset;
        transform.position = Vector3.Lerp(
            transform.position,
            targetPosition,
            Constants.Camera.FOLLOW_SPEED * Time.deltaTime
        );

        // Look at player with slight upward bias
        Vector3 lookTarget = playerTarget.position + Vector3.up * 0.5f;
        transform.LookAt(lookTarget);
    }

    /// <summary>
    /// Handles camera zoom with pinch gesture or mouse wheel.
    /// </summary>
    private void HandleZoom()
    {
        #if UNITY_EDITOR || UNITY_STANDALONE
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0)
        {
            currentZoom -= scroll * Constants.Camera.ZOOM_SPEED;
            currentZoom = Mathf.Clamp(
                currentZoom,
                Constants.Camera.MIN_ZOOM,
                Constants.Camera.MAX_ZOOM
            );
            CalculateIsometricOffset();
        }
        #endif

        // TODO: Implement pinch zoom for mobile
    }

    /// <summary>
    /// Converts world position to screen position for UI elements.
    /// </summary>
    public Vector3 WorldToScreenPoint(Vector3 worldPos)
    {
        return mainCamera.WorldToScreenPoint(worldPos);
    }

    /// <summary>
    /// Converts screen position to world position.
    /// </summary>
    public Vector3 ScreenToWorldPoint(Vector3 screenPos)
    {
        return mainCamera.ScreenToWorldPoint(screenPos);
    }

    public void SetTarget(Transform target)
    {
        playerTarget = target;
    }
}
