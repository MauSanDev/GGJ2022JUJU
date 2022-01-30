using UnityEngine;

public class PressurePlateInteraction : AbstractInteractionAction
{
    public override void OnPlayerTrigger(Collider2D other)
    {
        LevelData.CurrentLevelData.CurrentGoals.presurePlates++;
    }

    public override void OnPlayerTriggerExit(Collider2D other)
    {
        LevelData.CurrentLevelData.CurrentGoals.presurePlates--;
    }
}
