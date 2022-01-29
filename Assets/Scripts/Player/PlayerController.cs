using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PlayerInputHandler))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] private bool lookAtMousePos = false;
    
    private Rigidbody2D _rigidbody = null;
    private PlayerInputHandler _inputHandler = null;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _inputHandler = GetComponent<PlayerInputHandler>();
    }

    private void FixedUpdate()
    {
        UpdatePosition();
        UpdateRotation();
    }

    private void UpdatePosition()
    {
        Vector2 movement = _inputHandler.KeyboardInput.normalized * movementSpeed;
        _rigidbody.MovePosition(_rigidbody.position + movement * Time.fixedDeltaTime);
    }

    private void UpdateRotation()
    {
        if (!lookAtMousePos) return;
        
        Vector2 targetDirection = _inputHandler.MouseWorldPos - _rigidbody.position;
        float targetRotation = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg - 90f;
        _rigidbody.rotation = targetRotation;
    }
}
