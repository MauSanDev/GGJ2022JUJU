using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{
    [SerializeField] private Camera mainCamera = null;
    [SerializeField] private List<AbstractPlayerInputListener> inputListeners = new List<AbstractPlayerInputListener>();

    public event Action<PlayerInputData> OnInputChanged;

    private Plane _rayPlane;
    private Ray _camRay;
    
    public Vector2 KeyboardInput { get; private set; }
    public Vector2 MouseWorldPos { get; private set; }
    public bool IsTurnLightPressed { get; set; }

    public Action OnMouseClickHoldStart;
    public Action OnMouseClickHoldEnd;

    public bool IsWalking => KeyboardInput != Vector2.zero;

    private void Awake()
    {
        _rayPlane = new Plane(Vector3.forward, Vector3.zero);
        BindListeners();
    }

    private void OnDestroy()
    {
        UnbindListeners();
    }

    private void BindListeners()
    {
        foreach (AbstractPlayerInputListener listener in inputListeners)
        {
            OnInputChanged += listener.OnInputChanged;
        }
    }

    private void UnbindListeners()
    {
        foreach (AbstractPlayerInputListener listener in inputListeners)
        {
            OnInputChanged -= listener.OnInputChanged;
        }
    }

    public bool EnableInputs { get; set; } = true;

    private void Update()
    {
        if (!EnableInputs) return;
        
        GetKeyboardInput();
        GetMouseInput();
        GetMouseClick();
    }

    private void GetMouseClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            OnMouseClickHoldStart?.Invoke();
        }
        
        if (Input.GetMouseButtonUp(0))
        {
            OnMouseClickHoldEnd?.Invoke();
        }
    }

    private void GetKeyboardInput()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        KeyboardInput = new Vector2(horizontalInput, verticalInput);
        OnInputChanged?.Invoke(GetPlayerInputData());
    }

    private void GetMouseInput()
    {
        _camRay = mainCamera.ScreenPointToRay(Input.mousePosition);

        if (_rayPlane.Raycast(_camRay, out float rayLength))
        {
            MouseWorldPos = _camRay.GetPoint(rayLength);
        }
    }

    private PlayerInputData GetPlayerInputData()
    {
        return new PlayerInputData()
        {
            movementAxis = KeyboardInput,
            mouseClickPressed = IsMousePressed,
            mouseWorldPosition = MouseWorldPos,
            mouseScreenPosition = MousePosition,
            isMoving = IsWalking
        };
    }

    public bool IsMousePressed => Input.GetMouseButton(0);
    public Vector3 MousePosition => Input.mousePosition;

}

public struct PlayerInputData
{
    public Vector3 movementAxis;
    public Vector3 mouseWorldPosition;
    public Vector3 mouseScreenPosition;
    public bool mouseClickPressed;
    public bool isMoving;
}
