
using UnityEngine;

public class EndLevelTrigger : MonoBehaviour
{
   [SerializeField] private LayerMask playerLayers;
   private void OnTriggerEnter2D(Collider2D other)
   {
      if (other.gameObject.layer == playerLayers)
      {
         EventsManager.DispatchEvent(EvenManagerConstants.ON_LEVEL_COMPLETE);
      }
   }
}
