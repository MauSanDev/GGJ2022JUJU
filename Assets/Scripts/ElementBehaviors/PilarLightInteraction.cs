public class PilarLightInteraction : AbstractLightInteraction
{
    public override void OnLightStart()
    {
        ShowTextBehaviour.Instance.ShowText("You activated a Pilar");
        LevelData.CurrentLevelData.LevelGoalChecker.RegisterPillar();
    }
}
