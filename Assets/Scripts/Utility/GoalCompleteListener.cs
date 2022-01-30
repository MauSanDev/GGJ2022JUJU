using UnityEngine;

public class GoalCompleteListener : MonoBehaviour
{
    [SerializeField] private InteractableObject objToInteract = null;
    
    private void Awake()
    {
        EventsManager.SubscribeToEvent(EvenManagerConstants.ON_GOALS_ACHIEVED, OnGoalsAchieved);
        EventsManager.SubscribeToEvent(EvenManagerConstants.ON_GOALS_DISMISS, OnGoalsDismiss);
    }

    private void OnGoalsAchieved(params object[] param)
    {
        objToInteract.AllowInteraction();
        
        //Play a noise?
    }

    private void OnGoalsDismiss(params object[] param)
    {
        objToInteract.AllowInteraction();

        //Play a noise?
    }
}
