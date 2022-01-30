using UnityEngine;

public class DeathZone : MonoBehaviour
{
    private Collider2D collider;

    private void Awake()
    {
        collider = GetComponent<Collider2D>();
    }

    private void OnDrawGizmos()
    {
        if (!Application.isPlaying) return;
        
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, ((CircleCollider2D)collider).radius);
    }
}
