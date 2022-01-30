using UnityEngine;

public abstract class AbstractInteractionAction : MonoBehaviour
{
    public virtual void OnPlayerCollide(Collision2D other) {}
    public virtual void OnPlayerCollideExit(Collision2D other) {}
    public virtual void OnPlayerTrigger(Collider2D other) {}
    public virtual void OnPlayerTriggerExit(Collider2D other) {}
}


public abstract class AbstractLightInteraction : MonoBehaviour
{
    public virtual void OnLightStart() {}
    public virtual void OnLightEnd() {}
}
