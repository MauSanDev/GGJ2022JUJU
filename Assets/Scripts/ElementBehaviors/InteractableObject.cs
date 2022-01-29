using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Collider2D))]
public class InteractableObject : MonoBehaviour
{
    [SerializeField] private AbstractInteractionAction interactionAction;
    [SerializeField] private bool singleInteraction = true;
    
    [SerializeField] private bool isEnabledToInteract = false;

    [SerializeField] private InteractionType interactionType;

    [Header("Sprites")] 
    [SerializeField] private SpriteRenderer enabledSprite;
    [SerializeField] private SpriteRenderer disabledSprite;

    private bool wasInteracted = false;

    private enum InteractionType
    {
        Collision,
        Trigger
    }

    private void Awake()
    {
        CheckInteractionSprites();
    }

    private void CheckInteractionSprites()
    {
        bool canInteract = CanInteract;
        if (enabledSprite != null)
        {
            enabledSprite.gameObject.SetActive(canInteract);
        }
        if (disabledSprite != null)
        {
            disabledSprite.gameObject.SetActive(!canInteract);
        }
    }

    public bool CanInteract => isEnabledToInteract && (!singleInteraction || !wasInteracted);

    public void AllowInteraction()
    {
        isEnabledToInteract = true;
        CheckInteractionSprites();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (interactionType == InteractionType.Collision && CanInteract)
        {
            interactionAction.OnPlayerCollide(other);
            wasInteracted = true;
            CheckInteractionSprites();
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (interactionType == InteractionType.Trigger && CanInteract)
        {
            interactionAction.OnPlayerTrigger(other);
            wasInteracted = true;
            CheckInteractionSprites();
        }
    }
}
