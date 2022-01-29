
using System;
using UnityEngine;

public class LightCharger : MonoBehaviour
{
    [SerializeField] private LayerMask playerLayers;
    [SerializeField] private GameObject enabledState;
    [SerializeField] private GameObject disableState;
    private LightHandler lightHandler;
    private bool enableCharger;
    private void Start()
    {
        lightHandler = LevelData.CurrentLevelData.LightHandler;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (playerLayers == (playerLayers | (1 << other.gameObject.layer)))
        {
            enableCharger = true;
        }
    }

    private void Update()
    {
        if (!enableCharger)
        {
            return;
        }
        
        if (lightHandler.LightIsFull)
        {
            enabledState.SetActive(false);
            disableState.SetActive(true);
        }
        else
        {
            enabledState.SetActive(true);
            disableState.SetActive(false);
            lightHandler.StartChargingLight();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (playerLayers == (playerLayers | (1 << other.gameObject.layer)))
        {
            enabledState.SetActive(false);
            disableState.SetActive(true);
            enableCharger = false;
            lightHandler.StopChargingLight();
        }
    }
}
