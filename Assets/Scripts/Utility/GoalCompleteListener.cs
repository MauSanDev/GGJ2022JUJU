using UnityEngine;

public class GoalCompleteListener : MonoBehaviour
{
    [SerializeField] private InteractableObject objToInteract = null;
    
    private void Awake()
    {
        EventsManager.SubscribeToEvent(EvenManagerConstants.ON_GOALS_ACHIEVED, OnGoalsAchieved);
    }

    private void OnGoalsAchieved(params object[] param)
    {
        objToInteract.AllowInteraction();
        
        //Play a noise?
    }
}
