using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(TilemapRenderer))]
public class LightMaskarableTileset : MonoBehaviour
{
    [SerializeField] private SpriteMaskInteraction maskInteraction = SpriteMaskInteraction.VisibleOutsideMask;
    private TilemapRenderer tileRenderer = null;
    
    private void Awake()
    {
        tileRenderer = GetComponent<TilemapRenderer>();
        EventsManager.SubscribeToEvent(EvenManagerConstants.ON_DRAIN_LANTERN, OnDrainStart);
        EventsManager.SubscribeToEvent(EvenManagerConstants.ON_DRAIN_STOP, OnDrainStop);
    }
    
    private void OnDrainStart(object[] parametercontainer)
    {
        SetAsMaskarable(true);   
    }
    private void OnDrainStop(object[] parametercontainer)
    {
        SetAsMaskarable(false);   
    }

    private void SetAsMaskarable(bool set)
    {
        tileRenderer.maskInteraction = set ? maskInteraction : SpriteMaskInteraction.None;
    }

    private void OnDestroy()
    {
        EventsManager.UnsubscribeToEvent(EvenManagerConstants.ON_DRAIN_LANTERN, OnDrainStart);
        EventsManager.UnsubscribeToEvent(EvenManagerConstants.ON_DRAIN_STOP, OnDrainStop);
    }
}
