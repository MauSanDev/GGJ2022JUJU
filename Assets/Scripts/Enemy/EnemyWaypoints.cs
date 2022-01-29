using UnityEngine;

public class EnemyWaypoints : MonoBehaviour
{
    [SerializeField] private Transform[] waypoints;
    [SerializeField] private bool randomIndex;
    private int index;
    private Transform currentWaypoint;
    
    private void Start()
    {
        currentWaypoint = waypoints[GetNextWaypointIndex()];
    }
    
    public Transform GetWaypointPosition(Transform currentEnemyPosition)
    {
        if (HasReachedToTheCurrentWaypoint(currentEnemyPosition))
        {
            index = GetNextWaypointIndex();
            currentWaypoint = waypoints[index];
        }
        return currentWaypoint;
    }
    
    private bool HasReachedToTheCurrentWaypoint(Transform currentEnemyPosition)
    {
        float distance = Vector2.Distance (currentWaypoint.position, currentEnemyPosition.position);
        return distance < 0.5f;
    }
    
    private int GetNextWaypointIndex()
    {
        return randomIndex ? Random.Range(0, waypoints.Length) : (int) Mathf.Repeat(index + 1, waypoints.Length);
    }
}
