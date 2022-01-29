using UnityEngine;

public class LightEnableDisableComponent : MonoBehaviour, ILighteable
{
    [SerializeField] private GameObject[] objectsToSwitchState;
    
    public void OnLightEnter()
    {
        SwitchState();
    }

    public void OnLightExit()
    {
        SwitchState();
    }

    private void SwitchState()
    {
        foreach (GameObject gameObject in objectsToSwitchState)
        {
            gameObject.SetActive(!gameObject.activeSelf);
        }
    }
}
