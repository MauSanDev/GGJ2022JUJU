using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractInteractionAction : MonoBehaviour
{
    public virtual void OnPlayerCollide(Collision2D other) {}
    public virtual void OnPlayerTrigger(Collider2D other) {}
}
