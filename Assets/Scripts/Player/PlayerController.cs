using UnityEngine;

/// <summary>
/// Manages player movement, rotation, and basic interactions.
/// Handles joystick input and converts it to world movement.
/// </summary>
public class PlayerController : MonoBehaviour
{
    [SerializeField] private CharacterController characterController;
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private Animator animator;

    private Vector3 currentVelocity = Vector3.zero;
    private Vector3 facingDirection = Vector3.forward;
    private bool isDashing = false;
    private float dashTimer = 0f;

    private void Start()
    {
        if (characterController == null)
            characterController = GetComponent<CharacterController>();

        if (playerStats == null)
            playerStats = GetComponent<PlayerStats>();

        if (animator == null)
            animator = GetComponent<Animator>();

        // Set camera target
        if (CameraController.Instance != null)
            CameraController.Instance.SetTarget(transform);
    }

    private void Update()
    {
        HandleMovement();
        HandleDash();
        UpdateAnimations();
    }

    /// <summary>
    /// Handles player movement based on joystick input.
    /// </summary>
    private void HandleMovement()
    {
        if (isDashing) return;

        Vector3 moveDirection = InputManager.Instance.GetMovementDirection();

        if (moveDirection.magnitude > 0.1f)
        {
            // Update facing direction
            facingDirection = moveDirection;

            // Rotate player to face movement direction
            RotateTowardDirection(moveDirection);
        }

        // Calculate movement velocity
        float moveSpeed = playerStats.GetMoveSpeed();
        currentVelocity = moveDirection * moveSpeed;

        // Apply gravity
        currentVelocity.y -= 9.81f * Time.deltaTime;

        // Move character
        characterController.Move(currentVelocity * Time.deltaTime);
    }

    /// <summary>
    /// Rotates the player to face the movement direction smoothly.
    /// </summary>
    private void RotateTowardDirection(Vector3 direction)
    {
        if (direction.magnitude < 0.1f) return;

        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Lerp(
            transform.rotation,
            targetRotation,
            Constants.Player.ROTATION_SPEED * Time.deltaTime
        );
    }

    /// <summary>
    /// Handles dash/roll ability with invincibility frames.
    /// </summary>
    private void HandleDash()
    {
        if (InputManager.Instance.IsDashPressed() && !isDashing)
        {
            StartDash();
        }

        if (isDashing)
        {
            dashTimer -= Time.deltaTime;
            if (dashTimer <= 0)
            {
                isDashing = false;
            }
        }
    }

    private void StartDash()
    {
        isDashing = true;
        dashTimer = Constants.Player.DASH_DURATION;

        // Apply dash velocity in facing direction
        currentVelocity = facingDirection * Constants.Player.DASH_SPEED;

        // TODO: Add invincibility frames
        // TODO: Add dash animation
    }

    /// <summary>
    /// Updates animator parameters based on current state.
    /// </summary>
    private void UpdateAnimations()
    {
        float moveSpeed = currentVelocity.magnitude;
        animator.SetFloat(Constants.Animation.PARAM_MOVE_SPEED, moveSpeed);
    }

    // ==================== PUBLIC METHODS ====================

    public Vector3 GetFacingDirection() => facingDirection;
    public Vector3 GetPosition() => transform.position;
    public bool IsDashing() => isDashing;

    public void SetPosition(Vector3 position)
    {
        characterController.enabled = false;
        transform.position = position;
        characterController.enabled = true;
    }

    public void TakeDamage(float damage)
    {
        playerStats.TakeDamage(damage);
        
        // TODO: Add knockback
        // TODO: Add damage animation
    }
}
