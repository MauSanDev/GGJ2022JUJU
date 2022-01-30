using System;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour, ILighteable
{
    [SerializeField] private EnemyWaypoints waypointsComponent;
    [SerializeField] private LayerMask playerLayer;

    [Header("Behaviors")] 
    [SerializeField] private AbstractEnemyBehavior initialBehavior;
    [SerializeField] private AbstractEnemyBehavior proximityBehavior;
    [SerializeField] private AbstractEnemyBehavior illuminatedBehavior;
    [SerializeField] private TriggerListener proximityTrigger;

    private Vector2 movement;
    public bool isIlluminated;
    private Vector2 initialPosition;

    private void BindBehaviors()
    {
        if (initialBehavior != null)
        {
            initialBehavior.OnBehaviorEnded += OnBehaviorStopped;
        }
        if (illuminatedBehavior != null)
        {
            illuminatedBehavior.OnBehaviorEnded += OnBehaviorStopped;
        }
        if (proximityBehavior != null)
        {
            proximityBehavior.OnBehaviorEnded += OnBehaviorStopped;
        }
    }
    
    private void Start()
    {
        initialPosition = transform.position;
        BindBehaviors();

        if (proximityTrigger != null && proximityBehavior != null)
        {
            proximityTrigger.onTriggerEnter += OnProximityTrigger;
        }

        ExecuteBehavior(initialBehavior);
    }

    private void OnDestroy()
    {
        if (proximityTrigger != null && proximityBehavior != null)
        {
            proximityTrigger.onTriggerEnter += OnProximityTrigger;
        }
    }

    private void OnProximityTrigger(Collider2D other)
    {
        if (playerLayer == (playerLayer | (1 << other.gameObject.layer)))
        {
            StopBehavior(initialBehavior);
            StopBehavior(illuminatedBehavior);
            ExecuteBehavior(proximityBehavior);
        }
    }

    private void OnBehaviorStopped()
    {
        if ((initialBehavior == null || !initialBehavior.IsExecuting) && 
            (proximityBehavior == null || !proximityBehavior.IsExecuting) &&
            (illuminatedBehavior == null || !illuminatedBehavior.IsExecuting))
        {
            ExecuteBehavior(initialBehavior);
        }
    }

    public void OnLightEnter()
    {
        if (illuminatedBehavior != null)
        {
            StopBehavior(initialBehavior);
            StopBehavior(proximityBehavior);
            ExecuteBehavior(illuminatedBehavior);
        }
        isIlluminated = true;
    }

    public void OnLightExit()
    {
        isIlluminated = false;
    }
    
    private void StopBehavior(AbstractEnemyBehavior behavior)
    {
        if (behavior != null)
        {
            behavior.StopBehavior();
        }
    }

    private void ExecuteBehavior(AbstractEnemyBehavior behavior)
    {
        if (behavior != null)
        {
            behavior.ExecuteBehavior();
        }
    }
}

public abstract class AbstractEnemyBehavior : MonoBehaviour
{
    private const string TARGET_LAYER = "Player";
    
    protected Rigidbody2D rigidBody = null;
    protected Transform player;
    
    
    [SerializeField] private AbstractMovementListener[] movementListeners;
    

    protected void UpdateMovementListeners(Vector3 movement)
    {
        foreach (var listener in movementListeners)
        {
            listener.OnMovementChanged(movement);
        }
    }
    
    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag(TARGET_LAYER).transform;
    }

    private bool isExecuting = false;
    public bool IsExecuting => isExecuting;
    
    public event Action OnBehaviorEnded;

    public void ExecuteBehavior()
    {
        isExecuting = true;
        OnBehaviorStart();
    }

    protected abstract void OnBehaviorStart();
    
    protected abstract void Behave();
    
    private void Update()
    {
        if (isExecuting)
        {
            Behave();
        }
    }

    public void StopBehavior()
    {
        isExecuting = false;
        OnBehaviorEnded?.Invoke();
    }

}