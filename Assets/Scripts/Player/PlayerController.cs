using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PlayerInputHandler))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] private bool lookAtMousePos = false;
    [SerializeField] private float timeBeforeDeathOnZone = 3f;
    
    private Rigidbody2D _rigidbody = null;
    private PlayerInputHandler _inputHandler = null;
    private string[] _footsteps;
    private bool _onDeathZone = false;
    private float _currentTime = 0f;
    private LightHandler _lightHandler = null;
    
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
        _lightHandler = FindObjectOfType<LightHandler>();
    }

    private void Update()
    {
        CheckForDeathZone();
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

    private void CheckForDeathZone()
    {
        if (_lightHandler == null) return;
        
        _currentTime = _onDeathZone && !_lightHandler.HasLight ? _currentTime + Time.deltaTime : 0f;

        if (_currentTime >= timeBeforeDeathOnZone && !_lightHandler.HasLight)
        {
            Debug.Log("DIE");
            _currentTime = 0f;
        }
    }
    
    private void OnStep(object[] parameters)
    {
        int soundIndex = Utility.Random.Next(_footsteps.Length);
        AudioManager.Instance.PlaySound(_footsteps[soundIndex]);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 7)
        {
            _onDeathZone = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == 7)
        {
            _onDeathZone = false;
        }
    }

    private void OnDestroy()
    {
        EventsManager.UnsubscribeToEvent(EvenManagerConstants.ON_PLAYER_STEP, OnStep);
    }
}
