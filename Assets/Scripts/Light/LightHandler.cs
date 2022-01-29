
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class LightHandler : MonoBehaviour
{
    [FormerlySerializedAs("amountOfLightPerSecondToConsume")] [SerializeField] private float amountOfLightToConsume = 0.15f;
    [SerializeField] private float amountOfLightToRecharge = 0.20f;
    [SerializeField] private Material onInactiveMaterial = null;
    [SerializeField] private Material onActiveMaterial = null;
    [SerializeField] private SpriteRenderer[] spriteRenderers = null;
    
    private float lightAmount;
    private Coroutine drainRoutine;
    private Coroutine chargeLightRoutine;
    private PlayerInputHandler input;
    private void Start()
    {
        lightAmount = LevelData.CurrentLevelData.LanternStartAmount;
        input = LevelData.CurrentLevelData.InputHandler;
        
        AssignMaterial(onInactiveMaterial);
        input.OnMouseClickHoldStart += OnMouseClickStart;
        input.OnMouseClickHoldEnd += OnMouseClickEnd;
        EventsManager.DispatchEvent(EvenManagerConstants.ON_UPDATE_LANTERN_VALUE, new object[] { lightAmount });
    }

    private void OnMouseClickEnd()
    {
        if (drainRoutine != null)
        {
            StopCoroutine(drainRoutine);
            drainRoutine = null;
            EventsManager.DispatchEvent(EvenManagerConstants.ON_DRAIN_STOP, new object[] { lightAmount });
            AssignMaterial(onInactiveMaterial);
        }
    }

    private void AssignMaterial(Material material)
    {
        foreach (SpriteRenderer renderer in spriteRenderers)
        {
            renderer.material = material;
        }
    }

    private void OnMouseClickStart()
    {
        if (HasLight && drainRoutine == null)
        {
            drainRoutine = StartCoroutine(DrainRoutineMethod());
            AssignMaterial(onActiveMaterial);
        }
    }

    private IEnumerator DrainRoutineMethod()
    {
        while (input.IsMousePressed && HasLight)
        {
            OnDrain(lightAmount - amountOfLightToConsume * Time.deltaTime);
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
        
        if (chargeLightRoutine != null)
        {
            StopCoroutine(chargeLightRoutine);
        }
        if (drainRoutine != null)
        {
            StopCoroutine(drainRoutine);
        }
    }

    private bool HasLight => lightAmount > 0;
    public bool LightIsFull => lightAmount >= 1;

    public void StartChargingLight()
    {
        if (!LightIsFull && chargeLightRoutine == null)
        {
            chargeLightRoutine = StartCoroutine(ChargeRoutineMethod());
        }
    }
    
    private IEnumerator ChargeRoutineMethod()
    {
        while (!LightIsFull)
        {
            OnCharge(lightAmount + amountOfLightToRecharge * Time.deltaTime);
            yield return null;
        }

        chargeLightRoutine = null;
    }

    private void OnCharge(float value)
    {
        value = Mathf.Clamp(value, 0, 1);
        lightAmount = value;
        EventsManager.DispatchEvent(EvenManagerConstants.ON_RECHARGE_LANTERN, new object[] { lightAmount });
    }

    public void StopChargingLight()
    {
        if (chargeLightRoutine != null)
        {
            StopCoroutine(chargeLightRoutine);
            chargeLightRoutine = null;
        }
    }
}
