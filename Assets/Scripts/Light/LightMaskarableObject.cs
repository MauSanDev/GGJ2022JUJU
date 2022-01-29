using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class LightMaskarableObject : MonoBehaviour
{
    [SerializeField] private SpriteMaskInteraction maskInteraction = SpriteMaskInteraction.VisibleOutsideMask;
    private SpriteRenderer spriteRenderer = null;
    
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
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
        spriteRenderer.maskInteraction = set ? maskInteraction : SpriteMaskInteraction.None;
    }

    private void OnDestroy()
    {
        EventsManager.UnsubscribeToEvent(EvenManagerConstants.ON_DRAIN_LANTERN, OnDrainStart);
        EventsManager.UnsubscribeToEvent(EvenManagerConstants.ON_DRAIN_STOP, OnDrainStop);
    }
}
