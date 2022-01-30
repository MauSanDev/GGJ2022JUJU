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
    [SerializeField] private bool multiplySpeed;
    [SerializeField] private float multiplierSpeed;

    private Rigidbody2D rb = null;
    private const string target = "Player";
    private Transform player;
    private Vector2 movement;
    private bool hasToPatrol;
    public bool isIlluminated;
    private Vector2 initialPosition;
    private bool staticEnemy;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag(target).transform;
        hasToPatrol = waypointsComponent != null;
        initialPosition = transform.position;
        staticEnemy = !hasToPatrol && !followPlayer;
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

    private void IlluminatedBehaviour()
    {
        if (runAway)
        {
            RunAway();
            Rotate(player);
        }
        else
        {
            MoveToTarget(player.position);
            Rotate(player);
        }
    }

    private void InitialBehaviour()
    {
        if (followPlayer || hasToPatrol)
        {
            MoveToTarget(Target.position);
            Rotate(Target);
        }

        if (staticEnemy)
        {
            StaticEnemyBehaviour();
        }
    }

    private void StaticEnemyBehaviour()
    { 
        float distance = Vector2.Distance(initialPosition, transform.position);
        if (distance > 0.5f)
        {
            MoveToTarget(initialPosition);
        }
    }
    
    private float Speed => isIlluminated &&  multiplySpeed ? (speed * multiplierSpeed) : speed;

    private bool ReachPlayer
    {
        get
        {
            float distance = Vector2.Distance (player.position, transform.position);
            return distance < 0.5f;
        }
    }
    
    private void MoveToTarget(Vector2 target)
    {
        if (!ReachPlayer)
        {
            Vector2 direction = (Vector2)target - rb.position;
            direction.Normalize();
            Vector2 vector = direction * Speed;
            rb.MovePosition(rb.position + vector * Time.fixedDeltaTime);
        }
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
