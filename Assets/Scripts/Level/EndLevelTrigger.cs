
using UnityEngine;

public class EndLevelTrigger : MonoBehaviour
{
   [SerializeField] private LayerMask playerLayers;
   private void OnTriggerEnter2D(Collider2D other)
   {
      if ( playerLayers == (playerLayers | (1 << other.gameObject.layer)))
      {
         EventsManager.DispatchEvent(EvenManagerConstants.ON_LEVEL_COMPLETE);
      }
   }
}
