using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target = null;
    [SerializeField] private float smoothness = 3f;
    
    private Vector3 _offset = Vector3.zero;

    private void Start()
    {
        _offset = transform.position - target.position;
    }

    private void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, target.position + _offset, smoothness * Time.deltaTime);
    }
}
