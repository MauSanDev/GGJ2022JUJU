using UnityEngine;

public class PressurePlateInteraction : AbstractInteractionAction
{
    public override void OnPlayerTrigger(Collider2D other)
    {
        LevelData.CurrentLevelData.LevelGoalChecker.EnablePressurePlate(true);
        AudioManager.Instance.PlaySound("SwitchOn");
    }

    public override void OnPlayerTriggerExit(Collider2D other)
    {
        LevelData.CurrentLevelData.LevelGoalChecker.EnablePressurePlate(false);
        AudioManager.Instance.PlaySound("SwitchOff");
    }
}
