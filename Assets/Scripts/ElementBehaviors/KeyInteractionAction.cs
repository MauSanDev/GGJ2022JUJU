using UnityEngine;

public class KeyInteractionAction : AbstractInteractionAction
{
    public override void OnPlayerCollide(Collision2D other)
    {
        ShowTextBehaviour.Instance.ShowText("You found a key");
        LevelData.CurrentLevelData.LevelGoalChecker.RegisterKey();
        AudioManager.Instance.PlaySound("ChestOpen");
    }
}
