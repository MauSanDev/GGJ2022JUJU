using UnityEngine;

public class EnemyBehaviour : MonoBehaviour, ILighteable
{
    [SerializeField] private EnemyWaypoints waypointsComponent;
    [SerializeField] private bool hasToRotate;
    
    [Header("Initial movement")]
    [SerializeField] private bool followPlayer;
    [Header("Speed")]
    [SerializeField] private float speed;
    [Header("When is illuminated")]
    [SerializeField] private bool runAway;
    [SerializeField] private bool increaseSpeedWhenFollowPlayer;
    [SerializeField] private float multiplierSpeed;
    
    
    
    private Rigidbody2D rb = null;
    private const string target = "Player";
    private Transform player;
    private Vector2 movement;
    private bool hasToPatrol;
    public bool isIlluminated;
    
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag(target).transform;
        hasToPatrol = waypointsComponent != null;
    }
    private void FixedUpdate()
    {
        Behave();
    }

    private void Behave()
    {
        if (isIlluminated)
        {
            IlluminatedBehaviour();
        }
        else
        {
            InitialBehaviour();
        }
    }

    private Transform Target => hasToPatrol ? waypointsComponent.GetWaypointPosition(transform) : player;
    private float Speed => isIlluminated &&  increaseSpeedWhenFollowPlayer ? (speed * multiplierSpeed) : speed;

    private void IlluminatedBehaviour()
    {
        if (runAway)
        {
            RunAway();
            Rotate(player);
        }
        else
        {
            MoveToTarget(player);
            Rotate(player);
        }
    }

    private void InitialBehaviour()
    {
        if (followPlayer || hasToPatrol)
        {
            MoveToTarget(Target);
            Rotate(Target);
        }
    }

    private void MoveToTarget(Transform target)
    {
        Vector2 direction = (Vector2)target.position - rb.position;
        direction.Normalize();
        Vector2 vector = direction * Speed;
        rb.MovePosition(rb.position + vector * Time.fixedDeltaTime);
    }

    private void Rotate(Transform target)
    {
        if (hasToRotate)
        {
            Vector2 targetDirection = target.position - transform.position;
            targetDirection.Normalize();
            float targetRotation = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg - 90f;
            rb.rotation = targetRotation;    
        }
    }
    
    private void RunAway()
    {
        Vector2 direction = (Vector2)player.position - rb.position;
        direction.Normalize();
        Vector2 vector = direction * -Speed;
        rb.MovePosition(rb.position + vector * Time.deltaTime);
    }

    public void OnLightEnter()
    {
        isIlluminated = true;
    }

    public void OnLightExit()
    {
        isIlluminated = false;
    }
}
