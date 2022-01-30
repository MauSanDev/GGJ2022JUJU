using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class InteractableObject : MonoBehaviour
{
    [SerializeField] private AbstractInteractionAction interactionAction;
    [SerializeField] private bool singleInteraction = true;
    [SerializeField] private bool isEnabledToInteract = false;
    [SerializeField] private InteractionType interactionType;
    [SerializeField] private LayerMask allowedLayer;

    [Header("Sprites")] 
    [SerializeField] private SpriteRenderer enabledSprite;
    [SerializeField] private SpriteRenderer disabledSprite;

    
    [Header("GameObjects")] 
    [SerializeField] private GameObject enabledGO;
    
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

    public void BanInteraction()
    {
        isEnabledToInteract = false;
        CheckInteractionSprites();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(IsAllowedLayer(other.gameObject))
        {
            if (interactionType == InteractionType.Collision && CanInteract)
            {
                interactionAction.OnPlayerCollide(other);
                wasInteracted = true;
                CheckInteractionSprites();
            }
        }
    }

    private bool IsAllowedLayer(GameObject obj) => allowedLayer == (allowedLayer | (1 << obj.layer));

    private void OnCollisionExit2D(Collision2D other)
    {
        if(IsAllowedLayer(other.gameObject))
        {
            if (interactionType == InteractionType.Collision && CanInteract)
            {
                interactionAction.OnPlayerCollideExit(other);
                wasInteracted = true;
                CheckInteractionSprites();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(IsAllowedLayer(other.gameObject))
        {
            if (interactionType == InteractionType.Trigger && CanInteract)
            {
                interactionAction.OnPlayerTrigger(other);
                wasInteracted = true;
                CheckInteractionSprites();

                if (enabledGO != null)
                {
                    enabledGO.SetActive(true);
                }
            }
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if(IsAllowedLayer(other.gameObject))
        {
            if (interactionType == InteractionType.Trigger && CanInteract)
            {
                interactionAction.OnPlayerTriggerExit(other);
                wasInteracted = true;
                CheckInteractionSprites();
                 
                if (enabledGO != null)
                {
                    enabledGO.SetActive(false);
                }
            }
        }
    }
}
