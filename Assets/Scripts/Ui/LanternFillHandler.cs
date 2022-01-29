using UnityEngine;
using UnityEngine.UI;

public class LanternFillHandler : MonoBehaviour
{
    [SerializeField] private Slider fillImage = null;
    private void Awake()
    {
        EventsManager.SubscribeToEvent(EvenManagerConstants.ON_RECHARGE_LANTERN, OnRechargeLantern);
        EventsManager.SubscribeToEvent(EvenManagerConstants.ON_DRAIN_LANTERN, OnDrainLantern);
        EventsManager.SubscribeToEvent(EvenManagerConstants.ON_UPDATE_LANTERN_VALUE, OnUpdateLantern);
    }

    private void OnUpdateLantern(object[] parametercontainer)
    {
        fillImage.value = Mathf.Clamp( (float)parametercontainer[0] , 0f, 1f);
    }
    
    private void OnDrainLantern(object[] parametercontainer)
    {
        OnUpdateLantern(parametercontainer);
    }

    private void OnRechargeLantern(object[] parametercontainer)
    {
        OnUpdateLantern(parametercontainer);
    }

    private void OnDestroy()
    {
        EventsManager.UnsubscribeToEvent(EvenManagerConstants.ON_RECHARGE_LANTERN, OnRechargeLantern);
        EventsManager.UnsubscribeToEvent(EvenManagerConstants.ON_DRAIN_LANTERN, OnDrainLantern);
        EventsManager.UnsubscribeToEvent(EvenManagerConstants.ON_UPDATE_LANTERN_VALUE, OnUpdateLantern);
    }
}
