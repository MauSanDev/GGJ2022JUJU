using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{
    [SerializeField] private Camera mainCamera = null;

    private Plane _rayPlane;
    private Ray _camRay;
    
    public Vector2 KeyboardInput { get; private set; }
    public Vector2 MouseWorldPos { get; private set; }

    public bool IsWalking => KeyboardInput != Vector2.zero;

    private void Awake()
    {
        _rayPlane = new Plane(Vector3.forward, Vector3.zero);
    }

    private void Update()
    {
        GetKeyboardInput();
        GetMouseInput();
    }

    private void GetKeyboardInput()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        KeyboardInput = new Vector2(horizontalInput, verticalInput);
    }

    private void GetMouseInput()
    {
        _camRay = mainCamera.ScreenPointToRay(Input.mousePosition);

        if (_rayPlane.Raycast(_camRay, out float rayLength))
        {
            MouseWorldPos = _camRay.GetPoint(rayLength);
        }
    }
}
