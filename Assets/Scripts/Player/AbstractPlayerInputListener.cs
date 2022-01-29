using UnityEngine;

public abstract class AbstractPlayerInputListener : MonoBehaviour
{
    public abstract void OnInputChanged(PlayerInputData inputData);
}
