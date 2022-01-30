using System;
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

    [SerializeField] private AbstractMovementListener[] movementListeners;

    private Rigidbody2D rb = null;
    private const string target = "Player";
    private Transform player;
    private Vector2 movement;
    private bool hasToPatrol;
    public bool isIlluminated;
    private Vector2 initialPosition;
    private bool staticEnemy;
    
    //runaway
    private bool isRunningAway = false;
    [SerializeField] private float runawayTime = 3f;
    private float runawayTimer = 0f;

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
        if (isIlluminated && !isRunningAway)
        {
            IlluminatedBehaviour();
        }
        else
        {
            InitialBehaviour();
        }
    }

    private Transform Target => hasToPatrol ? waypointsComponent.GetWaypointPosition(transform) : player;

    private void UpdateMovementListeners(Vector3 movement)
    {
        foreach (var listener in movementListeners)
        {
            listener.OnMovementChanged(movement);
        }
    }

    private void Update()
    {
        if (isRunningAway)
        {
            runawayTimer += Time.deltaTime;

            if (runawayTimer > runawayTime)
            {
                runawayTimer = 0f;
                isRunningAway = false;
            }
        }
    }

    private void IlluminatedBehaviour()
    {
        if (runAway)
        {
            isRunningAway = true;
        }
        else
        {
            MoveToTarget(player.position);
            Rotate(player);
        }
    }

    private void InitialBehaviour()
    {
        if (isRunningAway)
        {
            RunAway();
            Rotate(player);
        }
        else if (followPlayer || hasToPatrol)
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
    
    private float Speed => isIlluminated &&  multiplySpeed ? (speed * multiplierSpeed * Time.deltaTime) : speed;

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
            Vector3 finalPos = rb.position + vector * Time.deltaTime;
            rb.MovePosition(finalPos);
            UpdateMovementListeners(finalPos);
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
        Vector3 finalPos = rb.position + vector ;
        rb.MovePosition(finalPos);
        UpdateMovementListeners(finalPos);
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
