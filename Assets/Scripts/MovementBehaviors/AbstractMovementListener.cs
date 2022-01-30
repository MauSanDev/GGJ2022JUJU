using UnityEngine;

public abstract class AbstractMovementListener : MonoBehaviour
{
    public abstract void OnMovementChanged(Vector3 movementAxis);
}
