using UnityEngine;

public class LightInteractableObject : MonoBehaviour, ILighteable
{
    [SerializeField] private AbstractLightInteraction interactionAction;
    [SerializeField] private bool singleInteraction = true;
    
    [SerializeField] private bool isEnabledToInteract = false;

    [Header("Sprites")] 
    [SerializeField] private SpriteRenderer enabledSprite;
    [SerializeField] private SpriteRenderer disabledSprite;

    private bool wasInteracted = false;
    
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


    public void OnLightEnter()
    {
        if (CanInteract)
        {
            interactionAction.OnLightStart();
            wasInteracted = true;
            CheckInteractionSprites();
        }
        
    }

    public void OnLightExit()
    {
        interactionAction.OnLightEnd();
        CheckInteractionSprites();
    }
}
