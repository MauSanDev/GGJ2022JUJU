using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableInteraction : MonoBehaviour
{
    public PlayerController playerController;
    private void OnTriggerStay2D(Collider2D collision)
    {
        Movable other = collision.gameObject.GetComponent<Movable>();
        if (!other) 
            return;

        other.Push(playerController.transform.position, playerController.currMovement);
    }
}
