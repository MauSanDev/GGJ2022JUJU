using UnityEngine;

public class AnimationEventHandler : MonoBehaviour
{
    public void OnStep()
    {
        EventsManager.DispatchEvent(EvenManagerConstants.ON_PLAYER_STEP);
    }
}
