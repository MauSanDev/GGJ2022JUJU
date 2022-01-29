using UnityEngine;

public class EndLevelInteraction : AbstractInteractionAction
{
   [SerializeField] private LayerMask playerLayers;
   
   public override void OnPlayerTrigger(Collider2D collision)
   {
      if ( playerLayers == (playerLayers | (1 << collision.gameObject.layer)))
      {
         EventsManager.DispatchEvent(EvenManagerConstants.ON_LEVEL_COMPLETE);
      }
   }
}
