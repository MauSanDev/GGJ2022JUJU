using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PressurePlate : MonoBehaviour
{
    [SerializeField] private LayerMask interactionMask;
    [SerializeField] private float timeToComplete = 0f;
    [SerializeField] private bool shouldReset = false;

    public UnityEvent<GameObject> onActivate;
    public UnityEvent<GameObject> onDeactivate;

    private bool pressing = false;
    private bool activated = false;
    private float counter = 0f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (((1 << collision.gameObject.layer) & interactionMask) != 0)
        {
            pressing = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (((1 << collision.gameObject.layer) & interactionMask) != 0)
        {
            pressing = false;
        }
    }

    void Update()
    {
        if (pressing)
        {
            if (timeToComplete <= 0)
            {
                TriggerActivate();
            }
            else
            {
                counter += Time.deltaTime;
                if (counter >= timeToComplete)
                {
                    TriggerActivate();
                }
            }
        }
        else 
        {
            if (shouldReset)
            { 
                counter = 0f;
                TriggerDeactivate();
            }
        }
    }

    private void TriggerActivate()
    {
        if (!activated)
        {
            onActivate?.Invoke(this.gameObject);
            activated = true;
            print("Activated: " + this.gameObject.name);
        }
    }

    private void TriggerDeactivate()
    {
        if (activated)
        {
            onDeactivate?.Invoke(this.gameObject);
            activated = false;
            print("Deactivated: " + this.gameObject.name);
        }
    }
}
