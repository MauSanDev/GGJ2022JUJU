using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class TriggerListener : MonoBehaviour
{
    public event Action<Collider2D> onTriggerEnter;
    public event Action<Collider2D> onTriggerExit;
    public event Action<Collider2D> onTriggerStay;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        onTriggerEnter?.Invoke(other);
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        onTriggerExit?.Invoke(other);
    }
    
    private void OnTriggerStay2D(Collider2D other)
    {
        onTriggerStay?.Invoke(other);
    }
}
