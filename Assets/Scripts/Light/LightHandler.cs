
using System;
using System.Collections;
using UnityEngine;

public class LightHandler : MonoBehaviour
{
    [SerializeField] private float amountOfLightPerSecondToConsume;
    
    private float lightAmount;
    private Coroutine drainRoutine;
    private PlayerInputHandler input;
    private void Start()
    {
        lightAmount = LevelData.CurrentLevelData.LanternStartAmount;
        input = LevelData.CurrentLevelData.InputHandler;
        
        input.OnMouseClickHoldStart += OnMouseClickStart;
        input.OnMouseClickHoldEnd += OnMouseClickEnd;
        EventsManager.DispatchEvent(EvenManagerConstants.ON_UPDATE_LANTERN_VALUE, new object[] { lightAmount });
    }

    private void OnMouseClickEnd()
    {
        if (drainRoutine != null)
        {
            StopCoroutine(drainRoutine);
        }
    }

    private void OnMouseClickStart()
    {
        if (HasLight)
        {
            drainRoutine = StartCoroutine(DrainRoutineMethod());
        }
    }

    private IEnumerator DrainRoutineMethod()
    {
        while (input.IsMousePressed && HasLight)
        {
            OnDrain(lightAmount - amountOfLightPerSecondToConsume * Time.deltaTime);
            yield return null;
        }
    }

    private void OnDrain(float value)
    {
        value = Mathf.Clamp(value, 0, 1);
        lightAmount = value;
        EventsManager.DispatchEvent(EvenManagerConstants.ON_DRAIN_LANTERN, new object[] { lightAmount });
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        ILighteable lighteable = other.GetComponent<ILighteable>();
        
        lighteable?.OnLightEnter();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        ILighteable lighteable = other.GetComponent<ILighteable>();
        
        lighteable?.OnLightExit();
    }

    private void OnDestroy()
    {
        input.OnMouseClickHoldStart -= OnMouseClickStart;
        input.OnMouseClickHoldEnd -= OnMouseClickEnd;
    }

    private bool HasLight => lightAmount > 0;
}
