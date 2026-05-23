using UnityEngine;

/// <summary>
/// Manages all input handling for mobile (Android/iOS) and desktop platforms.
/// Supports virtual joystick, attack button, and dash button.
/// </summary>
public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }

    [Header("Joystick Settings")]
    [SerializeField] private RectTransform joystickBase;
    [SerializeField] private RectTransform joystickHandle;
    [SerializeField] private float joystickRadius = 80f;

    [Header("Button Settings")]
    [SerializeField] private RectTransform attackButton;
    [SerializeField] private RectTransform dashButton;

    private Vector2 joystickInput = Vector2.zero;
    private bool isAttackPressed = false;
    private bool isDashPressed = false;
    private bool isJoystickActive = false;
    private Vector2 joystickStartPos;

    // Touch tracking
    private int joystickTouchID = -1;
    private int attackTouchID = -1;
    private int dashTouchID = -1;

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
        HandleTouchInput();
        HandleMouseInput();
    }

    /// <summary>
    /// Handles touch input for mobile platforms.
    /// </summary>
    private void HandleTouchInput()
    {
        // Reset button presses each frame
        isAttackPressed = false;
        isDashPressed = false;

        for (int i = 0; i < Input.touchCount; i++)
        {
            Touch touch = Input.GetTouch(i);
            Vector2 touchPos = touch.position;

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    HandleTouchBegan(touchPos, touch.fingerId);
                    break;

                case TouchPhase.Moved:
                    HandleTouchMoved(touchPos, touch.fingerId);
                    break;

                case TouchPhase.Ended:
                case TouchPhase.Canceled:
                    HandleTouchEnded(touch.fingerId);
                    break;
            }
        }
    }

    /// <summary>
    /// Handles mouse input for desktop testing.
    /// </summary>
    private void HandleMouseInput()
    {
        #if UNITY_EDITOR || UNITY_STANDALONE
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = Input.mousePosition;
            
            if (IsPointOverUI(mousePos, joystickBase))
            {
                joystickTouchID = 0;
                joystickStartPos = mousePos;
                isJoystickActive = true;
            }
            else if (IsPointOverUI(mousePos, attackButton))
            {
                isAttackPressed = true;
            }
            else if (IsPointOverUI(mousePos, dashButton))
            {
                isDashPressed = true;
            }
        }

        if (Input.GetMouseButton(0) && joystickTouchID == 0)
        {
            Vector2 mousePos = Input.mousePosition;
            UpdateJoystickPosition(mousePos);
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (joystickTouchID == 0)
            {
                ResetJoystick();
            }
            joystickTouchID = -1;
        }
        #endif
    }

    private void HandleTouchBegan(Vector2 touchPos, int fingerId)
    {
        // Check joystick
        if (IsPointOverUI(touchPos, joystickBase) && joystickTouchID == -1)
        {
            joystickTouchID = fingerId;
            joystickStartPos = touchPos;
            isJoystickActive = true;
        }
        // Check attack button
        else if (IsPointOverUI(touchPos, attackButton))
        {
            attackTouchID = fingerId;
            isAttackPressed = true;
        }
        // Check dash button
        else if (IsPointOverUI(touchPos, dashButton))
        {
            dashTouchID = fingerId;
            isDashPressed = true;
        }
    }

    private void HandleTouchMoved(Vector2 touchPos, int fingerId)
    {
        if (fingerId == joystickTouchID)
        {
            UpdateJoystickPosition(touchPos);
        }
    }

    private void HandleTouchEnded(int fingerId)
    {
        if (fingerId == joystickTouchID)
        {
            ResetJoystick();
            joystickTouchID = -1;
        }
        else if (fingerId == attackTouchID)
        {
            attackTouchID = -1;
        }
        else if (fingerId == dashTouchID)
        {
            dashTouchID = -1;
        }
    }

    private void UpdateJoystickPosition(Vector2 touchPos)
    {
        Vector2 direction = touchPos - joystickStartPos;
        float distance = direction.magnitude;

        if (distance > joystickRadius)
        {
            direction = direction.normalized * joystickRadius;
        }

        joystickHandle.anchoredPosition = direction;
        joystickInput = (direction / joystickRadius).normalized;

        // Clamp to unit circle
        if (joystickInput.magnitude > 1f)
        {
            joystickInput = joystickInput.normalized;
        }
    }

    private void ResetJoystick()
    {
        joystickHandle.anchoredPosition = Vector2.zero;
        joystickInput = Vector2.zero;
        isJoystickActive = false;
    }

    private bool IsPointOverUI(Vector2 screenPoint, RectTransform uiElement)
    {
        if (uiElement == null) return false;
        return RectTransformUtility.RectangleContainsScreenPoint(uiElement, screenPoint);
    }

    // ==================== PUBLIC ACCESSORS ====================

    public Vector2 GetJoystickInput() => joystickInput;
    public bool IsJoystickActive() => isJoystickActive;
    public bool IsAttackPressed() => isAttackPressed;
    public bool IsDashPressed() => isDashPressed;

    public Vector3 GetMovementDirection()
    {
        // Convert 2D input to 3D isometric direction
        Vector3 direction = new Vector3(joystickInput.x, 0, joystickInput.y);
        return direction.normalized;
    }
}
