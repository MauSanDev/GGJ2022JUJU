using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PlayerInputHandler))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] private bool lookAtMousePos = false;
    
    private Rigidbody2D _rigidbody = null;
    private PlayerInputHandler _inputHandler = null;
    private string[] _footsteps;

    public Vector3 currMovement 
    {
        get;
        private set;
    }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _inputHandler = GetComponent<PlayerInputHandler>();
        _footsteps = new[] { "Footstep_1", "Footstep_2", "Footstep_3" };
        EventsManager.SubscribeToEvent(EvenManagerConstants.ON_PLAYER_STEP, OnStep);
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

        currMovement = movement * Time.fixedDeltaTime;
    }

    private void UpdateRotation()
    {
        if (lookAtMousePos)
        {
            Vector2 targetDirection = _inputHandler.MouseWorldPos - _rigidbody.position;
            float targetRotation = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg - 90f;
            _rigidbody.rotation = targetRotation;
        }
    }

    private void OnStep(object[] parameters)
    {
        int soundIndex = Utility.Random.Next(_footsteps.Length);
        AudioManager.Instance.PlaySound(_footsteps[soundIndex]);
    }

    private void OnDestroy()
    {
        EventsManager.UnsubscribeToEvent(EvenManagerConstants.ON_PLAYER_STEP, OnStep);
    }
}
