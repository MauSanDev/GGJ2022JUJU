
using System;
using System.Collections;
using UnityEngine;

public class LightHandler : MonoBehaviour
{
    [SerializeField] private int amountOfLightPerSecondToConsume;
    [SerializeField] private iTween.EaseType drainEase = iTween.EaseType.easeInBack;
    
    private int lightAmount;
    private bool isDraining;
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
            isDraining = true;
            drainRoutine = StartCoroutine(DrainRoutineMethod());
        }
    }

    private IEnumerator DrainRoutineMethod()
    {
        if (!isDraining)
        {
            yield return null;
        }
        
        Hashtable hashtable =  iTween.Hash("from",lightAmount,"to", lightAmount - amountOfLightPerSecondToConsume,"time", 1,"onupdate",nameof(OnDrain), "easetype", drainEase);
        iTween.ValueTo(gameObject, hashtable);
        yield return new WaitForSeconds(1);
    }

    private void OnDrain(float value)
    {
        value = Mathf.Clamp(value, 0, 100);
        lightAmount = (int)value;
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
