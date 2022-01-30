using UnityEngine;

public class PressurePlateInteraction : AbstractInteractionAction
{
    public override void OnPlayerTrigger(Collider2D other)
    {
        LevelData.CurrentLevelData.LevelGoalChecker.EnablePressurePlate(true);
    }

    public override void OnPlayerTriggerExit(Collider2D other)
    {
        LevelData.CurrentLevelData.LevelGoalChecker.EnablePressurePlate(false);
    }
}
