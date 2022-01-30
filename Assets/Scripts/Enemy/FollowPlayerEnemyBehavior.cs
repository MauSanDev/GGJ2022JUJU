using UnityEngine;

public class FollowPlayerEnemyBehavior : AbstractEnemyBehavior
{
    [SerializeField] private float movementSpeed = 2f;

    protected override void OnBehaviorStart()
    {
    }

    private bool ReachPlayer
    {
        get
        {
            float distance = Vector2.Distance (player.position, transform.position);
            return distance < 0.5f;
        }
    }

    protected override void Behave()
    {
        if (!ReachPlayer)
        {
            Vector2 direction = (Vector2)player.transform.position - rigidBody.position;
            direction.Normalize();
            Vector2 vector = direction * movementSpeed;
            Vector3 finalPos = rigidBody.position + vector * Time.deltaTime;
            rigidBody.MovePosition(finalPos);
        }
    }
}
