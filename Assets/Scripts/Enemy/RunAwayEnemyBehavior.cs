using UnityEngine;

public class RunAwayEnemyBehavior : AbstractEnemyBehavior
{
    [SerializeField] private float movementSpeed = 2f;
    [SerializeField] private float runawayTime = 3f;
    private float runawayTimer = 0f;

    protected override void OnBehaviorStart()
    {
        runawayTimer = 0f;
    }

    protected override void Behave()
    {
        if (runawayTimer > runawayTime)
        {
            StopBehavior();
            return;
        }

        runawayTimer += Time.deltaTime;
        Vector2 direction = (Vector2)player.position - rigidBody.position;
        direction.Normalize();
        Vector2 vector = direction * -movementSpeed;
        Vector3 finalPos = rigidBody.position + vector * Time.deltaTime ;
        rigidBody.MovePosition(finalPos);
    }
}
